using eUseControl.BusinessLogic.Interfaces;
using eUseControl.Domain.Entities.DTO;
using eUseControl.Domain.Enums;
using eUseControl.Web.Filters;
using eUseControl.Web.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace eUseControl.Web.Controllers.Admin
{
    [RoleAuthorization(URole.Admin)]
    [Route("Admin")]

    public class OrdersController : Controller
    {
        private readonly IOrder _orderLogic;

        public OrdersController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _orderLogic = bl.GetOrderBL();
        }

        public ActionResult Index()
        {
            var allOrders = _orderLogic.GetAllOrders();

            var myOrders = allOrders
                .Select(o => new WaiterOrderViewModel
                {
                    OrderId = o.Id,
                    TableNumber = o.TableNumber,
                    WaiterName = o.WaiterName,
                    Status = o.Status,
                    TotalAmount = o.TotalAmount,
                    OrderComment = o.Note,
                    CompletedTime = o.CompletedTime,
                    CreatedAt = o.CreatedAt,
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

            return View("~/Views/Admin/Orders/Index.cshtml", myOrders);
        }
        [HttpGet]
        public ActionResult Edit(int OrderId)
        {
            var order = _orderLogic.GetOrderById(OrderId);
            if (order == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                var model = new WaiterOrderViewModel
                {
                    OrderId = order.Id,
                    TableNumber = order.TableNumber,
                    WaiterName = order.WaiterName,
                    Status = order.Status,
                    TotalAmount = order.TotalAmount,
                    OrderComment = order.Note,
                    CompletedTime = order.CompletedTime,
                    CreatedAt = order.CreatedAt,
                    Items = order.OrderItems.Select(i => new OrderItemViewModel
                    {
                        DishName = i.DishName,
                        Amount = i.Amount,
                        Note = i.Note,
                        Status = i.Status
                    }).ToList()
                };
                return View("~/Views/Admin/Orders/Edit.cshtml", model);
            }

        }

        [HttpPost]
        public ActionResult Edit(WaiterOrderViewModel order)
        {
            var orderDto = new OrderDTO
            {
                Id = order.OrderId,
                TableNumber = order.TableNumber,
                WaiterName = order.WaiterName,
                Status = order.Status,
                TotalAmount = order.TotalAmount,
                Note = order.OrderComment,
                CompletedTime = order.CompletedTime,
                CreatedAt = order.CreatedAt,
                OrderItems = order.Items.Select(i => new OrderItemDTO
                {
                    DishName = i.DishName,
                    Amount = i.Amount,
                    Note = i.Note,
                    Status = i.Status
                }).ToList()
            };
            var response = _orderLogic.ChangeOrder(orderDto);
            if (response.Status)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View("~/Views/Admin/Orders/Edit.cshtml", order);
            }
        }

        public ActionResult Cancel(int OrderId) 
        {
            var order = _orderLogic.GetOrderById(OrderId);
            if (order == null)
            {
                return RedirectToAction("Index");
            }
            var orderDto = new OrderDTO
            {
                Id = order.Id,
                TableNumber = order.TableNumber,
                WaiterName = order.WaiterName,
                Status = DStatus.Cancelled,
                TotalAmount = order.TotalAmount,
                Note = order.Note,
                CompletedTime = order.CompletedTime,
                CreatedAt = order.CreatedAt,
                OrderItems = order.OrderItems.Select(i => new OrderItemDTO
                {
                    DishName = i.DishName,
                    Amount = i.Amount,
                    Note = i.Note,
                    Status = i.Status
                }).ToList()
            };
            var response = _orderLogic.ChangeOrder(orderDto);
            if (response.Status)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index");
            }

        }
    }
}