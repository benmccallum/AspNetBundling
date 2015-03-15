AspNetBundling
==============

[![Build status](https://ci.appveyor.com/api/projects/status/suqmxmi7f9l6cv1e/branch/master)](https://ci.appveyor.com/project/benmccallum/aspnetbundling/branch/master)

An assortment of bundling utility classes like a ScriptWithSourceMapBundle and fixes for the ASP.NET Web Optimization bundling framework.

## Get it on NuGet!

    Install-Package AspNetBundling


Bundling with SourceMap generation
---------------------------------------------------

    BundleTable.Bundles.Add(new ScriptWithSourceMapBundle("MyBundleVirtualPath")
      .Include("MyJsFileOne.js", "MyJsFileTwo.js")
    );


Bundling with Css Rewrite Url Tranformer fix
---------------------------------------------------------

    BundleTable.Bundles.Add(new StyleBundle("MyBundleVirtualPath")
      .Include("MyCssFileOne.css, new CssRewriteUrlTransformFixed())
      .Include("MyCssFileTwo.css, new CssRewriteUrlTransformFixed())
    );
