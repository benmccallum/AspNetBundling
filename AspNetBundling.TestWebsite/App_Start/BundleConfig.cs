using System.Web;
using System.Web.Optimization;

namespace AspNetBundling.TestWebsite
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new AjaxMinScriptBundle("~/bundles/jquery")
                .Include("~/Scripts/jquery-{version}.js"));

            bundles.Add(new AjaxMinScriptBundle("~/bundles/jqueryval")
                .Include("~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new AjaxMinScriptBundle("~/bundles/modernizr")
                .Include("~/Scripts/modernizr-*"));

            bundles.Add(new AjaxMinScriptBundle("~/bundles/bootstrap")
                .Include("~/Scripts/bootstrap.js", "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css")
                .Include("~/Content/bootstrap.css", new CssRewriteUrlTransformFixed())
                .Include("~/Content/site.css", new CssRewriteUrlTransformFixed())
            );

            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            BundleTable.EnableOptimizations = true;
        }
    }
}
