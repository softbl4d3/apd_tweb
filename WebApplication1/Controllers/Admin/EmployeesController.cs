using eUseControl.BusinessLogic.Interfaces;
using eUseControl.Domain.Entities.DTO;
using eUseControl.Domain.Enums;
using eUseControl.Web.Filters;
using eUseControl.Web.Models;
using System.Linq;
using System.Web.Mvc;

namespace eUseControl.Web.Controllers.Admin
{
    [RoleAuthorization(URole.Admin)]
    [Route("Admin")]
    public class EmployeesController : Controller
    {
        private readonly IUser _session;
        public EmployeesController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _session = bl.GetSessionBL();
        }
        public ActionResult Register()
        {
            return View("~/Views/Admin/Employees/Register.cshtml");
        }

        // POST: /Admin/RegisterEmployee
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Register(EmpRegViewModel model)
        {

            if (ModelState.IsValid)
            {
                UserDTO user = new UserDTO
                {
                    UserName = model.UserName,
                    Password = model.Password,
                    Role = (URole)model.Role
                };
                var responce = _session.RegisterEmployee(user);
                if (responce.Status == true)
                {
                    return RedirectToAction("List");
                }
                else
                {
                    return RedirectToAction("Register");
                }
            }

            return View("~/Views/Admin/Employees/Register.cshtml", model);
        }

        // Пример метода для вывода списка сотрудников


        public ActionResult List()
        {
            var employeesDTO = _session.GetAllEmployee();

            var employees = employeesDTO.Select(user => new EmployeeViewModel
            {
                UserName = user.UserName,
                Role = user.Role
            }).ToList();
            return View("~/Views/Admin/Employees/List.cshtml", employees);
        }

        public ActionResult Delete(string username)
        {
            var resp = _session.DeleteUser(username);
            if (resp.Status == true)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("List");
            }
        }
    }
}