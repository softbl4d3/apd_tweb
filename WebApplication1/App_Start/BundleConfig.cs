using System.Web.Optimization;

namespace eUseControl.Web
{
    public static class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            // Bootstrap CSS
            bundles.Add(new StyleBundle("~/bundles/bootstrap/css").Include(
                "~/Content/bootstrap.min.css",
                new CssRewriteUrlTransform()));

            //// Bootstrap JS
           // bundles.Add(new ScriptBundle("~/bundles/bootstrap/js").Include(
             //   "~/Scripts/bootstrap.min.js"));

            // Font Awesome
            bundles.Add(new StyleBundle("~/bundles/font-awesome/css").Include(
                "~/Content/font-awesome.min.css",
                new CssRewriteUrlTransform()));

            // Toastr (уведомления)
            bundles.Add(new StyleBundle("~/bundles/toaster/css").Include(
                "~/Vendors/toastr/toastr.min.css",
                new CssRewriteUrlTransform()));

            // DataTables (таблицы)
            bundles.Add(new StyleBundle("~/bundles/datatables/css").Include(
                "~/Vendors/datatables/datatables.min.css",
                new CssRewriteUrlTransform()));

            // Другие бандлы (jQuery, ваши скрипты и стили) можно добавить здесь
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js"));
        }
    }
}