using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eUseControl.Domain.Enums;
using eUseControl.Web.Filters;
using eUseControl.Web.Models;
namespace eUseControl.Web.Controllers
{
    [RoleAuthorization(URole.Chef)]

    public class ChefController : Controller
    {
        // Временное хранилище заказов (заглушка)
        

        //[Authorize(Roles = "Chef")] // Защита по роли
        public ActionResult Dashboard()
        {
            return View();
        }

        public ActionResult Menu()
        {
            return View();
        }

        [HttpPost]
        //[Authorize(Roles = "Chef")]
        public ActionResult MarkAsDone(int id)
        {
            //var order = _orders.FirstOrDefault(o => o.Id == id);
            //if (order != null)
            //{
            //    order.Status = "Готов";
            //    // Здесь должна быть логика сохранения в БД

            //}
            return RedirectToAction("Dashboard");
        }
    }
}