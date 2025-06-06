﻿using eUseControl.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eUseControl.Domain.Entities.DTO;
using eUseControl.Domain.Enums;
using eUseControl.Web.Filters;
using eUseControl.BusinessLogic.Interfaces;
using System.Web.Security;

namespace eUseControl.Web.Controllers
{
    [RoleAuthorization(URole.Waiter)]
    [Route("Waiter")]
    public class TableController : Controller
    {
        private readonly ITable _tableLogic;
        private readonly IDish _dishLogic;
        private readonly IUser _sessionLogic;
        private readonly IOrder _orderLogic;
        public TableController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _tableLogic = bl.GetTableBL();
            _dishLogic = bl.GetDishBL();
            _sessionLogic = bl.GetSessionBL();
            _orderLogic = bl.GetOrderBL();
        }
        public ActionResult Select()
        {
            var tablesDto = _tableLogic.GetAllTables();
            var tables = tablesDto.Select(t => new TableViewModel
            {
                TableNumber = t.TableNumber,
                Capacity = t.Capacity,
                Status = t.Status,
                Zone = t.Zone,
            })
                .OrderBy(t => t.TableNumber)
                .ToList();

            return View("~/Views/Waiter/SelectTable.cshtml", tables);
        }
        [HttpGet]
        public ActionResult Orders()
        {
            var apiCookie = HttpContext.Request.Cookies["X-KEY"];
            var profile = _sessionLogic.GetUserByCookie(apiCookie.Value);
            var allOrders = _orderLogic.GetAllOrders();

            var todayStart = DateTime.Today;
            var todayEnd = DateTime.Today.AddDays(1).AddTicks(-1);

            var myOrders = allOrders
                .Where(o => o.WaiterName == profile.UserName
                && o.CreatedAt >= todayStart
                && o.CreatedAt <= todayEnd)
                .Select(o => new WaiterOrderViewModel
                {
                    TableNumber = o.TableNumber,
                    Status = o.Status,
                    TotalAmount = o.TotalAmount,
                    OrderComment = o.Note,
                    Items = o.OrderItems.Select(i =>
                    {
                        
                        return new OrderItemViewModel
                        {
                            DishName = i.DishName,
                            Amount = i.Amount,
                            Note = i.Note,
                            Status = i.Status,
                        };
                    }).ToList() 
                })
                .ToList();

            return View("~/Views/Waiter/Orders.cshtml", myOrders);
        }

        public ActionResult OrderItems()
        {

            var apiCookie = HttpContext.Request.Cookies["X-KEY"];
            var profile = _sessionLogic.GetUserByCookie(apiCookie.Value);
            List<OrderItemDTO> allOrderItems = _orderLogic.GetAllOrderItems();

            var myOrderItems = allOrderItems
                .Where(oi => oi.WaiterName == profile.UserName)
                .Where(oi => oi.Status != DStatus.Completed)
                .ToList();

            var ordItemVM = myOrderItems.Select(oi => new OrderItemViewModel
            {
                Id = oi.Id,
                DishName = oi.DishName,
                Amount = oi.Amount,
                Note = oi.Note,
                Status = oi.Status,
                TableNumber = oi.TableNumber,
          
            }).ToList();


            return View("~/Views/Waiter/OrderItems.cshtml", ordItemVM);
        }

        public ActionResult TakeOrder(int tableNumber)
        {
            var DishesDto = _dishLogic.GetAllDishes();
            var dishes = DishesDto.Select(d => new DishSelectViewModel
            {
                Id = d.Id,
                Name = d.Name,
                Description = d.Description,
                Category = d.Category,
                Price = d.Price,
                IsAvailable = d.IsAvailable
            })
                .Where(d => d.IsAvailable)
                .ToList();
            var model = new OrderViewModel
            {
                TableNumber = tableNumber,
                Dishes = dishes
            };
            return View("~/Views/Waiter/TakeOrder.cshtml", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TakeOrder(OrderViewModel model)
        {
            if (ModelState.IsValid)
            {
                var apiCookie = HttpContext.Request.Cookies["X-KEY"];
                var profile = _sessionLogic.GetUserByCookie(apiCookie.Value);
                var order = new OrderDTO
                {
                    WaiterName = profile.UserName,
                    TableNumber = model.TableNumber,
                    Note = model.Note,
                    OrderItems = model.Dishes
                        .Where(item => item.Quantity > 0) // Учитываем только блюда с количеством > 0
                        .Select(item => new OrderItemDTO
                        {
                            DishName = item.Name,
                            Amount = item.Quantity,
                            Note = item.Note
                        }).ToList()
                };

                var response = _orderLogic.CreateOrder(order);

                if (response.Status)
                {
                    return RedirectToAction("Select");
                }
                else
                {
                    ModelState.AddModelError("", response.Message);
                }
            }

            return RedirectToAction("Select");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MarkConfirmed(int id)
        {
            var status = DStatus.Completed;
            var resp = _orderLogic.ChangeOrderItemStatus(id, status);
            if (resp.Status)
            {
                return RedirectToAction("OrderItems");
            }
            return RedirectToAction("OrderItems");
        }
    }
}