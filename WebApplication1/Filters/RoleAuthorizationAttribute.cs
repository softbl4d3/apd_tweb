using eUseControl.BusinessLogic;
using eUseControl.BusinessLogic.Interfaces;
using eUseControl.Domain.Enums;
using eUseControl.Web.Extension;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace eUseControl.Web.Filters
{
    public class RoleAuthorizationAttribute : ActionFilterAttribute
    {
        private readonly IUser _session;
        private readonly URole _requiredRole;

        public RoleAuthorizationAttribute(URole requiredRole)
        {
            var bl = new BusinessLogic.BusinessLogic();
            _session = bl.GetSessionBL();
            _requiredRole = requiredRole;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var apiCookie = HttpContext.Current.Request.Cookies["X-KEY"];

            if (apiCookie != null)
            {
                var profile = _session.GetUserByCookie(apiCookie.Value);

                if (profile != null)
                {
                    if (profile.Role == _requiredRole)
                    {
                        HttpContext.Current.SetMySessionObject(profile);
                        HttpContext.Current.Session["LoginStatus"] = "login";
                    }
                    else
                    {

                        filterContext.Result = new RedirectToRouteResult(new
                            RouteValueDictionary(new { controller = "Account", action = "ErrorAuth" }));
                    }
                }
                else
                {

                    HttpContext.Current.Session.Clear();
                    if (filterContext.HttpContext.Request.Cookies.AllKeys.Contains("X-KEY"))
                    {
                        var cookie = filterContext.HttpContext.Request.Cookies["X-KEY"];
                        if (cookie != null)
                        {
                            cookie.Expires = DateTime.Now.AddDays(-1);
                            filterContext.HttpContext.Response.Cookies.Add(cookie);
                        }
                    }
                    HttpContext.Current.Session["LoginStatus"] = "logout";
                    filterContext.Result = new RedirectToRouteResult(new
                        RouteValueDictionary(new { controller = "Account", action = "Login" }));
                }
            }
            else
            {

                filterContext.Result = new RedirectToRouteResult(new
                    RouteValueDictionary(new { controller = "Account", action = "Login" }));

                HttpContext.Current.Session["LoginStatus"] = "logout";
            }
        }
    }
}