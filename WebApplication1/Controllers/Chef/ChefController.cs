using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Web;
using System.Web.Mvc;
using eUseControl.BusinessLogic.Interfaces;
using eUseControl.Domain.Entities.DTO;
using eUseControl.Domain.Enums;
using eUseControl.Web.Filters;
using eUseControl.Web.Models;
namespace eUseControl.Web.Controllers
{
    [RoleAuthorization(URole.Chef)]
    [Route("Chef")]

    public class ChefController : Controller
    {
        private readonly IOrder _orderLogic;
        private readonly IIngredient _ingLogic; 
        public ChefController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _orderLogic = bl.GetOrderBL();
            _ingLogic = bl.GetIngredientBL();
        }

        public ActionResult Menu()
        {
            var ingrDTO = _ingLogic.GetAllIngredients();

            var igredients = ingrDTO.Select(ing => new IngridientViewModel
            {
                Id = ing.Id,
                Name = ing.Name,
                Amount = ing.Amount,
                Status = ing.Status,

            }).ToList();
            return View("~/Views/Chef/Menu.cshtml", igredients);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateIngStatus(IngridientViewModel ing)
        {
            IngridientDTO ingDto = new IngridientDTO
            {
                Id = ing.Id,
                Name = ing.Name,
                Amount = ing.Amount,
                Status = ing.Status
            };
            var resp = _ingLogic.EditIngredient(ingDto);

            if (resp.Status == true)
            {
                return RedirectToAction("Menu");
            }
            else
            {
                return RedirectToAction("Menu");
            }

        }

        public ActionResult OrderItems()
        {
            List<OrderItemDTO> allOrderItems = _orderLogic.GetAllOrderItems();


            var ordItemVM = allOrderItems.Select(oi => new OrderItemViewModel
            {
                Id = oi.Id,
                DishName = oi.DishName,
                Amount = oi.Amount,
                Note = oi.Note,
                Status = oi.Status,
                TableNumber = oi.TableNumber,

            }).ToList();


            return View("~/Views/Chef/Dashboard.cshtml", ordItemVM);
        }

        public ActionResult MarkReady(int Id)
        {
            var status = DStatus.Ready;
            var response = _orderLogic.ChangeOrderItemStatus(Id, status);
            if (response.Status)
            {
                return RedirectToAction("OrderItems");
            }
            else
            {
                return RedirectToAction("OrderItems"); //error
            }

        }
    }
}