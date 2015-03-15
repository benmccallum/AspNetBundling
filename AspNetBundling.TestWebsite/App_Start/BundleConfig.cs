using System.Web;
using System.Web.Optimization;

namespace AspNetBundling.TestWebsite
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptWithSourceMapBundle("~/bundles/jquery")
                .Include("~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptWithSourceMapBundle("~/bundles/jqueryval")
                .Include("~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptWithSourceMapBundle("~/bundles/modernizr")
                .Include("~/Scripts/modernizr-*"));

            bundles.Add(new ScriptWithSourceMapBundle("~/bundles/bootstrap")
                .Include("~/Scripts/bootstrap.js")
                .Include("~/Scripts/respond.js", new TestJsTransformer()));

            bundles.Add(new StyleBundle("~/Content/css")
                .Include("~/Content/bootstrap.css", new CssRewriteUrlTransformFixed())
                .Include("~/Content/site.css", new CssRewriteUrlTransformFixed())
            );

            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            BundleTable.EnableOptimizations = true;
        }
    }

    public class TestJsTransformer : IItemTransform
    {
        public string Process(string includedVirtualPath, string input)
        {
            return input.Replace("if( href.length ){ href += \"/\"; }", "if( href && href.length && href.length > 0 ){ href += \"/\"; }");
        }
    }
}
