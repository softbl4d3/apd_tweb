using eUseControl.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eUseControl.Domain.Entities.DTO;
using eUseControl.Domain.Enums;
using eUseControl.Web.Filters;

namespace eUseControl.Web.Controllers
{
    [RoleAuthorization(URole.Waiter)]

    public class WaiterController : Controller
    {
        public ActionResult Menu()
        {
            return View();
        }

        //    return View(tables.Where(t => t.Status == TStatus.Free).ToList());
        //}

        //// GET: /Waiter/TakeOrder/5
        //public ActionResult TakeOrder(int tableId)
        //{
        //    // Заглушка данных меню
        //    var menu = new List<DishViewModel>
        //{
        //    new DishViewModel {  Name = "Карбонара", Price = 12.99m, IsAvailable = true },
        //    new DishViewModel {  Name = "Белое вино", Price = 8.99m, IsAvailable = true },
        //    new DishViewModel {  Name = "Стейк", Price = 24.99m, IsAvailable = true }
        //};

        //    ViewBag.TableId = tableId;
        //    return View(menu);
        //}

        //// POST: /Waiter/SubmitOrder
        //[HttpPost]
        //public ActionResult SubmitOrder(CreateOrderDTO order)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        // Заглушка для сохранения заказа
        //        // Здесь бы сохраняли в БД и отправляли повару

        //        // Пока просто вернем JSON ответ
        //        return Json(new { success = true });
        //    }
        //    return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors) });
        //}
    }

}