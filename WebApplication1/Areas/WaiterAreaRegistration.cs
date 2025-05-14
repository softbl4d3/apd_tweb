using System.Web.Mvc;

namespace eUseControl.Web.Areas
{
    using System.Web.Mvc;

    namespace YourProjectNamespace.Areas
    {
        public class WaiterAreaRegistration : AreaRegistration
        {
            public override string AreaName
            {
                get
                {
                    return "Waiter";
                }
            }

            public override void RegisterArea(AreaRegistrationContext context)
            {
                context.MapRoute(
                    name: "Waiter_default",
                    url: "Waiter/{controller}/{action}/{id}",
                    defaults: new { action = "Index", id = UrlParameter.Optional },
                    namespaces: new[] { "eUseControl.Web.Controllers" }
                );

                context.MapRoute(
                    name: "Waiter_root",
                    url: "Waiter",
                    defaults: new { controller = "Home", action = "Index", area = "Waiter" },
                    namespaces: new[] { "eUseControl.Web.Controllers" }
                );
            }
        }
    }
}