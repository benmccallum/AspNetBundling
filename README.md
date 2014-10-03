AspNetBundling
==============

An assortment of bundling utility classes like custom transformers that can generate source maps and fixes for the ASP.NET Web Optimization bundling framework.

## Get it on NuGet!

    Install-Package AspNetBundling


Bundling with SourceMap generation
---------------------------------------------------
(note: has a dependency on AjaxMin which can be grabbed from NuGet. Currently it seems it only supports .js.map generation, but my builder should support CSS once AjaxMin does.)

    BundleTable.Bundles.Add(new AjaxMinBundle("MyBundleVirtualPath", BundleFileTypes.JavaScript)
      .Include("MyJsFileOne.js", "MyJsFileTwo.js")
    );


Bundling with Css Rewrite Url Tranformer fix
---------------------------------------------------------

    BundleTable.Bundles.Add(new AjaxMinBundle("MyBundleVirtualPath", BundleFileTypes.StyleSheet)
      .Include("MyCssFileOne.css, new CssRewriteUrlTransformFixed())
      .Include("MyCssFileTwo.css, new CssRewriteUrlTransformFixed())
    );
