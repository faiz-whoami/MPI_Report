using System.Web;
using System.Web.Optimization;

namespace MPI_Report
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                      "~/Scripts/vendor/angular-1.8.2.min.js",
                      "~/Scripts/vendor/angular-route-1.8.2.min.js",
                      "~/Scripts/app/app.js",
                      "~/Scripts/app/api-service.js",
                      "~/Scripts/app/app-controller.js",
                      "~/Scripts/app/dashboard-controller.js",
                      "~/Scripts/app/operations-controller.js",
                      "~/Scripts/app/customer-controller.js",
                      "~/Scripts/app/report-controller.js",
                      "~/Scripts/app/login-controller.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/app.css"));
        }
    }
}
