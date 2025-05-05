using eUseControl.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eUseControl.Domain.Entities.DTO;
using eUseControl.BusinessLogic;

namespace eUseControl.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly SessionBL _session;
        public AccountController()
        {
            var bl = new eUseControl.BusinessLogic.BusinessLogic();
            _session = bl.GetSessionBL();
        }
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        // POST: Account/Login

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserViewModel login)
        {
            UserDTO userDto = new UserDTO { 
                UserName = login.UserName,
                Password = login.Password,
                Role = 0,
            };

            var userLogin = _session.User.Login(userDto);

            if (userLogin.Status)
            {
               
            }
            //if (userLogin.Status)
            //{
            //    switch (model.UserName)
            //    {
            //        case "Admin":
            //            return RedirectToAction("Admin", "Dashboard");
            //        case "Waiter":
            //            return RedirectToAction("Waiter", "Menu");
            //        case "Chef":
            //            return RedirectToAction("Chef", "Dashboard");
            //        default:
            //            ModelState.AddModelError("", "Неверная роль или пароль");
            //            return View(model);
            //    }
            //} 
            return View(login);
        }
    }
}
