using System.Web.Mvc;

namespace eUseControl.Web.Areas
{
    using System.Web.Mvc;

namespace YourProjectNamespace.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                name: "Admin_default",
                url: "Admin/{controller}/{action}/{id}",
                defaults: new { action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "eUseControl.Web.Controllers.Admin" }
            );

            context.MapRoute(
                name: "Admin_root",
                url: "Admin",
                defaults: new { controller = "Table", action = "Dashboard", area = "Admin" },
                namespaces: new[] { "eUseControl.Web.Controllers.Admin" }
            );
        }
    }
}
}