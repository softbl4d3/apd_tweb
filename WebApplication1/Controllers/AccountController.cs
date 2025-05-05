using eUseControl.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eUseControl.Domain.Entities.DTO;
using eUseControl.BusinessLogic;
using eUseControl.Domain.Enums;
using eUseControl.Web.Filters;

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

        public ActionResult ErrorAuth()
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
            };

            var userLogin = _session.Login(userDto);

            if (userLogin.Status)
            {
                HttpCookie cookie = _session.GenCookie(login.UserName);
                ControllerContext.HttpContext.Response.Cookies.Add(cookie);

                switch (userLogin.Role)
                {
                    case URole.Admin:
                        return RedirectToAction("Dashboard", "Admin");
                        
                    case URole.Chef:
                        return RedirectToAction("Menu", "Chef");
                        
                    case URole.Waiter:
                        return RedirectToAction("Menu", "Waiter");

                    case URole.None:
                        return View();

                    default:
                        
                        return View();
                }
            }
            else
            {
                ModelState.AddModelError("", userLogin.Message);
                return View();
            }
        }
    }
}
