using System.Web.Mvc;

namespace eUseControl.Web.Areas
{
    using System.Web.Mvc;

    namespace YourProjectNamespace.Areas
    {
        public class ChefAreaRegistration : AreaRegistration
        {
            public override string AreaName
            {
                get
                {
                    return "Chef";
                }
            }

            public override void RegisterArea(AreaRegistrationContext context)
            {
                context.MapRoute(
                    name: "Chef_default",
                    url: "Chef/{controller}/{action}/{id}",
                    defaults: new { action = "Index", id = UrlParameter.Optional },
                    namespaces: new[] { "eUseControl.Web.Controllers" }
                );

                context.MapRoute(
                    name: "Chef_root",
                    url: "Chef",
                    defaults: new { controller = "Home", action = "Index", area = "Chef" },
                    namespaces: new[] { "eUseControl.Web.Controllers" }
                );
            }
        }
    }
}