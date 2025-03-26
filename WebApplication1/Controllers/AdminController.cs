using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
    }
}