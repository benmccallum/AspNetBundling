AspNetBundling
==============

![Nuget](https://img.shields.io/nuget/dt/AspNetBundling.svg) [![Build status](https://ci.appveyor.com/api/projects/status/suqmxmi7f9l6cv1e/branch/master)](https://ci.appveyor.com/project/benmccallum/aspnetbundling/branch/master) 

An assortment of bundling utility classes like a ScriptWithSourceMapBundle and fixes for the ASP.NET Web Optimization bundling framework.

## Get it on NuGet!

    Install-Package AspNetBundling


Bundling with SourceMap generation
---------------------------------------------------

    BundleTable.Bundles.Add(new ScriptWithSourceMapBundle("MyBundleVirtualPath")
      .Include("MyJsFileOne.js", "MyJsFileTwo.js")
    );

By default, this will create a bundle with no cdnPath, codeMinify on, preserveImportantComments on, and a source map delivered at `bundleVirtualPath + "map"`.
	
For other variations of this, you can use the another `ScriptWithSourceMapBundle` constructor, or set the properties on it after instantiation. A more complete list of some examples is in the TestWebsite project > App_Start > `BundleConfig.cs`.

### Known issues:
1. A current bug in AjaxMin reported by @LodewijkSioen here https://ajaxmin.codeplex.com/workitem/21834,
causes the minification to hang when the debugger is attached and we're trying to do proper sourcemapping. As such,
to avoid more harm than good, we don't support proper source mapping in this scenario until AjaxMin fixes it's bug.


Bundling with Css Rewrite Url Tranformer fix
---------------------------------------------------------

    BundleTable.Bundles.Add(new StyleBundle("MyBundleVirtualPath")
      .Include("MyCssFileOne.css, new CssRewriteUrlTransformFixed())
      .Include("MyCssFileTwo.css, new CssRewriteUrlTransformFixed())
    );

Upgrade guide
---------------------------------------------------------

### v2 to v3
I've changed the default behaviour of bundles to minify code (rather than just remove whitespace) as the default should be the best for the end user. 
In doing this, some people using Angular that need DI variable names to be maintained will need 
to ensure they're now opting in to no code minification with the appropriate `ScriptWithSourceMapBundle` constructor overload.
