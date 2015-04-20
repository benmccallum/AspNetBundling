using System;
using System.Collections.Generic;
using System.Web.Optimization;

namespace AspNetBundling
{
    internal class SourceMapBundleBuilder : IBundleBuilder
    {
        SourceMapBundle _sourceMapBundle;

        public SourceMapBundleBuilder(SourceMapBundle bundle)
        {
            _sourceMapBundle = bundle;
        }

        public string BuildBundleContent(Bundle bundle, BundleContext context, IEnumerable<BundleFile> files)
        {
            return _sourceMapBundle.Content;
        }
    }
}
