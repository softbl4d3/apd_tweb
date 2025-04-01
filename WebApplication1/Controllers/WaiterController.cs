using eUseControl.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eUseControl.Domain.Entities.DTO;

namespace eUseControl.Web.Controllers
{
    public class WaiterController : Controller
    {
        // GET: /Waiter/SelectTable
        public ActionResult SelectTable()
        {
            // Заглушка данных столиков
            var tables = new List<Table>
        {
            new Table { Id = 1, TableNumber = 1, Capacity = 2, Status = "Free" },
            new Table { Id = 2, TableNumber = 2, Capacity = 4, Status = "Free" },
            new Table { Id = 3, TableNumber = 3, Capacity = 6, Status = "Occupied" }
        };

            return View(tables.Where(t => t.Status == "Free").ToList());
        }

        // GET: /Waiter/TakeOrder/5
        public ActionResult TakeOrder(int tableId)
        {
            // Заглушка данных меню
            var menu = new List<Dish>
        {
            new Dish { Id = 1, Name = "Карбонара", Price = 12.99m, IsAvailable = true },
            new Dish { Id = 2, Name = "Белое вино", Price = 8.99m, IsAvailable = true },
            new Dish { Id = 3, Name = "Стейк", Price = 24.99m, IsAvailable = true }
        };

            ViewBag.TableId = tableId;
            return View(menu);
        }

        // POST: /Waiter/SubmitOrder
        [HttpPost]
        public ActionResult SubmitOrder(CreateOrderDTO order)
        {
            if (ModelState.IsValid)
            {
                // Заглушка для сохранения заказа
                // Здесь бы сохраняли в БД и отправляли повару

                // Пока просто вернем JSON ответ
                return Json(new { success = true });
            }
            return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors) });
        }
    }

}