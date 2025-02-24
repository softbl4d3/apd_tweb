using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eUseControl.RestaurantStaffProject.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        // POST: Login/Authenticate
        [HttpPost]
        public ActionResult Authenticate(string username, string password)
        {
            if (username == "admin" && password == "admin")
            {
                Session["UserRole"] = "Admin";
                return RedirectToAction("Index", "Admin");
            }
            else if (username == "chef" && password == "chefpass")
            {
                Session["UserRole"] = "Chef";
                return RedirectToAction("Index", "Chef");
            }
            else if (username == "waiter" && password == "waiterpass")
            {
                Session["UserRole"] = "Waiter";
                return RedirectToAction("Index", "Waiter");
            }
            else
            {
                ViewBag.Error = "Invalid username or password";
                return View("Index");
            }
        }
    }
}
