using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eUseControl.Web.DTO;
using eUseControl.Web.Models;

namespace eUseControl.Web.Controllers
{
    //[Authorize(Roles= "Admin")]
    public class AdminController : Controller
    {
        // GET: /Admin/RegisterEmployee
        public ActionResult RegisterEmployee()
        {
            return View();
        }

        // POST: /Admin/RegisterEmployee
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterEmployee(EmployeeRegistrationViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Здесь нужно реализовать сохранение сотрудника в базу данных.
                // Например, с использованием Entity Framework:
                // var employee = new Employee {
                //     UserName = model.UserName,
                //     Password = HashPassword(model.Password), // Не забудьте хэшировать пароль!
                //     Role = model.Role
                // };
                // db.Employees.Add(employee);
                // db.SaveChanges();

                // В зависимости от архитектуры можно перенаправить на список сотрудников или другую страницу.
                return RedirectToAction("EmployeeList");
            }

            // Если есть ошибки валидации – вернуть представление с сообщениями
            return View(model);
        }

        // Пример метода для вывода списка сотрудников
        public ActionResult EmployeeList()
        {
            // Допустим, вы используете Entity Framework и у вас есть DbSet<Employee> Employees
            // в вашем контексте данных. Примерно так:
            // var employees = db.Employees.ToList();

            // Для наглядности пока сделаем заглушку (Fake):
            var employees = new List<User>
    {
        new User { UserName = "admin", Role = "Admin" },
        new User { UserName = "ivan", Role = "Waiter" },
        new User { UserName = "olga", Role = "Chef" }
    };

            return View(employees);
        }

        public ActionResult Dashboard()
        {
            
            var model = new AdminDashboard
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


    }
}