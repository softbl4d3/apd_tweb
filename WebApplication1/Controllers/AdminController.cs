using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eUseControl.Domain.Entities.DTO;
using eUseControl.Web.Models;
using eUseControl.Domain.Enums;
using eUseControl.BusinessLogic.Interfaces;
using eUseControl.BusinessLogic;

namespace eUseControl.Web.Controllers
{
    public class AdminController : Controller
    {
        private readonly ISession _session;
        public AdminController()
        {
            var bl = new eUseControl.BusinessLogic.BusinessLogic();
            _session = bl.GetSessionBL();
        }
        // GET: /Admin/RegisterEmployee
        public ActionResult RegisterEmployee()
        {
            return View();
        }

        // POST: /Admin/RegisterEmployee
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterEmployee(EmployeeRegistrationDTO model)
        {
            if (ModelState.IsValid)
            {
                UserDTO user = new UserDTO
                {
                    UserName = model.UserName,
                    Password = model.Password,
                    Role = (URole)model.Role
                };
                _session.RegisterEmpoyee(user);
                return RedirectToAction("EmployeeList");
            }

            // Если есть ошибки валидации – вернуть представление с сообщениями
            return View(model);
        }

        // Пример метода для вывода списка сотрудников
        public ActionResult EmployeeList()
        {
            // Допустим, в Entity Framework есть DbSet<Employee> Employees
            // var employees = db.Employees.ToList();

            // Для наглядности пока сделаем заглушку:
            var employees = new List<UserDTO>
    {
        new UserDTO { UserName = "admin", Role = URole.Admin },
        new UserDTO { UserName = "ivan", Role = URole.Waiter},
        new UserDTO { UserName = "olga", Role = URole.Chef }
    };

            return View(employees);
        }

        public ActionResult Dashboard()
        {

            var model = new AdminDTO
            {
                Tables = GetMockTables(),
                CurrentOrders = GetMockOrders()
            };

            return View(model);
        }
        private List<Table> GetMockTables() // Заглушка для списка столов
        {
            return new List<Table>
        {
            new Table { Id = 1, TableNumber = 42, Capacity = 4, Status = "APD", Zone = "Main Hall" },
            new Table { Id = 2, TableNumber = 233, Capacity = 2, Status = "Free", Zone = "Terrace" },
            new Table { Id = 3, TableNumber = 3, Capacity = 6, Status = "Reserved", Zone = "VIP" }
        };
        }

        private List<Order> GetMockOrders() // Заглушка для списка заказов
        {
            return new List<Order>
        {
            new Order
            {
                Id = 1,
                OrderNumber = 1001,
                TableId = 1,
                CreatedTime = DateTime.Now.AddMinutes(-30),
                Status = "InProgress",
                TotalAmount = 1500m
            },
            new Order
            {
                Id = 2,
                OrderNumber = 1002,
                TableId = 3,
                CreatedTime = DateTime.Now.AddMinutes(-15),
                Status = "New",
                TotalAmount = 2300m
            }
        };
        }

        private static List<Dish> _dishes = new List<Dish> {
            new Dish{
                Id = 52,
                Name = "карбонарик",
                Description = "вкуснейшее блюдо",
                Category= "Основное",
                Price= 4200,
                IsAvailable= true,
                 Ingredients = "макарошки и еще чета",
    },
            new Dish{
                Id = 12,
                Name = "pizza",
                Description = "итальянское блюдо",
                Category= "Основное",
                Price= 5200,
                IsAvailable= true,
                 Ingredients = "тесто и еще чета",
            },



        }; // Заглушка

        // GET: Admin/Menu
        public ActionResult Menu()
        {
            return View(_dishes);
        }

        // GET: Admin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Dish dish)
        {
            if (ModelState.IsValid)
            {
                _dishes.Add(dish);
                return RedirectToAction("Menu");
            }
            return View(dish);
        }

        // GET: Admin/Edit/5
        public ActionResult Edit(int id)
        {
            var dish = _dishes.FirstOrDefault(d => d.Id == id);
            return View(dish);
        }

        // POST: Admin/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Dish updatedDish)
        {
            if (ModelState.IsValid)
            {
                var existing = _dishes.First(d => d.Id == updatedDish.Id);
                _dishes.Remove(existing);
                _dishes.Add(updatedDish);
                return RedirectToAction("Menu");
            }
            return View(updatedDish);
        }

        // POST: Admin/ToggleStatus/5
        [HttpPost]
        public ActionResult ToggleStatus(int id)
        {
            var dish = _dishes.First(d => d.Id == id);
            dish.IsAvailable = !dish.IsAvailable;
            return RedirectToAction("Menu");
        }

        // GET: Admin/Delete/5
        public ActionResult Delete(int id)
        {
            var dish = _dishes.First(d => d.Id == id);
            _dishes.Remove(dish);
            return RedirectToAction("Menu");
        }

        // POST: Admin/Delete/id
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var dish = _dishes.First(d => d.Id == id);
            _dishes.Remove(dish);
            return RedirectToAction("Menu");
        }


    }
}