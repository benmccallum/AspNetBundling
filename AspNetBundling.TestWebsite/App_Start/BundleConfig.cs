using System.Web.Optimization;

namespace AspNetBundling.TestWebsite
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            // Default (
            bundles.Add(new ScriptWithSourceMapBundle("~/bundles/jquery")
                .Include("~/Scripts/jquery-{version}.js"));

            // Without providing a sourceMapExtension, appends "map" to end of bundleVirtualPath and works out-of-the-box
            bundles.Add(new ScriptWithSourceMapBundle("~/bundles/jquery", null, true, true) 
                .Include("~/Scripts/jquery-{version}.js"));

            // Providing a sourceMapExtension requires additional config of a handler for the extension (see web.config > handlers > *.map)
            bundles.Add(new ScriptWithSourceMapBundle("~/bundles/jquery-with-dotmap-ext", null, true, true, ".map")
                .Include("~/Scripts/jquery-{version}.js"));

            // Providing a sourceMapExtension requires additional config of a handler for the extension (see web.config > handlers > *.bundle)
            bundles.Add(new ScriptWithSourceMapBundle("~/bundles/jquery-with-dotbundle-ext", null, true, true, ".bundle")
                .Include("~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptWithSourceMapBundle("~/bundles/jquery-no-important-comments", null, true, false)
                .Include("~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptWithSourceMapBundle("~/bundles/jqueryval", null, true, true)
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
