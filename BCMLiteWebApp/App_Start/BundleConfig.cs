using System.Web;
using System.Web.Optimization;

namespace BCMLiteWebApp
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

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                        "~/Scripts/jquery-3.3.1.slim.js",
                        "~/Scripts/popper.js",
                        "~/Scripts/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/sb-admin").Include(
                      "~/Scripts/sb-admin.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/font-awesome.css",
                      "~/Content/sb-admin.css",
                      "~/Content/Site.css"));


            bundles.Add(new ScriptBundle("~/bundles/angularjs").Include(
                      "~/Scripts/angular.js",
                      "~/Scripts/angular-route.js"));

            //Custom Bundles
            bundles.Add(new ScriptBundle("~/bundles/angularApp").Include(
                      "~/Scripts/app/app.module.test.js",
                      "~/Scripts/app/app.routes.js",
                      "~/Scripts/app/shared/sharedService.js",
                      "~/Scripts/app/shared/navigation/navigationController.js",
                      "~/Scripts/app/components/organogram/organogramService.js",
                      "~/Scripts/app/components/organogram/organogramController.js"));

        }
    }
}
