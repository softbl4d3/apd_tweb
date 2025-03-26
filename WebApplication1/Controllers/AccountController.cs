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
                        return RedirectToAction("Admin", "Admin");
                    case "Waiter":
                        return RedirectToAction("Waiter", "Waiter");
                    case "Chef":
                        return RedirectToAction("Chef", "Chef");
                    default:
                        ModelState.AddModelError("", "Неверная роль или пароль");
                        return View(model);
                }
            }
            // Add a return statement for when ModelState is not valid
            return View(model);
        }
    }
}
