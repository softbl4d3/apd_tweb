using eUseControl.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eUseControl.Web.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        // POST: Account/Login

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User model)
        {
            if (ModelState.IsValid)
            {
                switch (model.Role)
                {
                    case "Admin":
                        return RedirectToAction("Admin", "Dashboard");
                    case "Waiter":
                        return RedirectToAction("Waiter", "Menu");
                    case "Chef":
                        return RedirectToAction("Chef", "Dashboard");
                    default:
                        ModelState.AddModelError("", "Неверная роль или пароль");
                        return View(model);
                }
            }
            return View(model);
        }
    }
}
