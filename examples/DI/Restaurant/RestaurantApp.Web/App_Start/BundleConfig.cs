﻿using System.Web;
using System.Web.Optimization;

namespace RestaurantApp.Web
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

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
            bundles.Add(new StyleBundle("~/Admin/css").Include(
                     "~/dataTables.bootstrap4.min.css",
                     "~/Content/styles.css"));
            bundles.Add(new ScriptBundle("~/bundles/all").Include(
                      "~/Scripts/admin/all.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/admin").Include(
                      "~/Scripts/admin/jquery-3.4.1.min.js",
                      "~/Scripts/admin/bootstrap.bundle.min.js",
                      "~/Scripts/admin/theme.js",
                      "~/Scripts/admin/jquery.dataTables.min.js",
                      "~/Scripts/admin/dataTables.bootstrap4.min.js",
                      "~/Scripts/admin/datatables-demo.js"
                      ));
        }
    }
}
