using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eUseControl.Web.Models;
namespace eUseControl.Web.Controllers
{
    public class ChefController : Controller
    {
        // Временное хранилище заказов (заглушка)
        private static List<OrderItem> _orders = new List<OrderItem>
    {
        new OrderItem
        {
            Id = 1,
            DishId = 1,
            Amount = 2,
            Price = 15.99m,
            Note = "Средняя прожарка",
            Status = "В работе"
        },
        new OrderItem
        {
            Id = 2,
            DishId = 3,
            Amount = 1,
            Price = 10.50m,
            Note = "Без лука",
            Status = "В работе"
        },
        new OrderItem
        {
            Id =3,
            DishId = 1,
            Amount = 2,
            Price = 15.99m,
            Note = "Средняя прожарка",
            Status = "В работе"
        },
        new OrderItem
        {
            Id = 24,
            DishId = 3,
            Amount = 1,
            Price = 10.50m,
            Note = "Без лука",
            Status = "В работе"
        },
        new OrderItem
        {
            Id = 21,
            DishId = 1,
            Amount = 2,
            Price = 15.99m,
            Note = "Средняя прожарка",
            Status = "В работе"
        },
        new OrderItem
        {
            Id = 442,
            DishId = 3,
            Amount = 1,
            Price = 10.50m,
            Note = "Без лука",
            Status = "В работе"
        },
        new OrderItem
        {
            Id = 221,
            DishId = 1,
            Amount = 2,
            Price = 15.99m,
            Note = "Средняя прожарка",
            Status = "В работе"
        },
        new OrderItem
        {
            Id = 44,
            DishId = 3,
            Amount = 1,
            Price = 10.50m,
            Note = "Без лука",
            Status = "В работе"
        },
    };

        //[Authorize(Roles = "Chef")] // Защита по роли
        public ActionResult Dashboard()
        {
            return View(_orders.Where(o => o.Status == "В работе").ToList());
        }

        [HttpPost]
        //[Authorize(Roles = "Chef")]
        public ActionResult MarkAsDone(int id)
        {
            var order = _orders.FirstOrDefault(o => o.Id == id);
            if (order != null)
            {
                order.Status = "Готов";
                // Здесь должна быть логика сохранения в БД

            }
            return RedirectToAction("Dashboard");
        }
    }
}