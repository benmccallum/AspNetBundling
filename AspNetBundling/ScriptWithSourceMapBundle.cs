using System.Web.Optimization;

namespace AspNetBundling
{
    /// <summary>
    /// Represents a list of file references to be bundled together as a single resource
    /// using AjaxMin as the minifier and a custom bundle builder (<see cref="AspNetBundling.ScriptWithSourceMapBundleBuilder"/>).
    /// </summary>
    public class ScriptWithSourceMapBundle : Bundle
    {
        /// <summary>
        /// Initializes a new instance of the ScriptWithSourceMapBundle class that takes a virtual path for the bundle.
        /// </summary>
        public ScriptWithSourceMapBundle(string virtualPath)
            : this(virtualPath, null, false, true, DefaultSourceMapExtension)
        {

        }

        /// <summary>
        /// Initializes a new instance of the ScriptWithSourceMapBundle class that takes a virtual path and a cdnPath for the bundle.
        /// </summary>
        public ScriptWithSourceMapBundle(string virtualPath, string cdnPath)
            : this(virtualPath, cdnPath, true, true, DefaultSourceMapExtension)
        {

        }

        /// <summary>
        /// Initializes a new instance of the ScriptWithSourceMapBundle class that takes a virtual path 
        /// and allows code minification to be toggled.
        /// </summary>
        /// <param name="minifyCode">Preserve variables names in scripts </param>
        public ScriptWithSourceMapBundle(string virtualPath, bool minifyCode)
            : this(virtualPath, null, minifyCode, true, DefaultSourceMapExtension)
        {

        }

        /// <summary>
        /// Initializes a new instance of the ScriptWithSourceMapBundle that takes a virtual path, a cdnPath,
        /// allows code minification to be toggled and preservation of important comments.
        /// </summary>
        /// <param name="preserveImportantComments">Toggle to preserve important comments when needed (e.g. legal requirement for a library)</param>
        public ScriptWithSourceMapBundle(string virtualPath, string cdnPath, bool minifyCode, bool preserveImportantComments)
          : this(virtualPath, null, minifyCode, preserveImportantComments, DefaultSourceMapExtension)
        {

        }

        /// <summary>
        /// Initializes a new instance of the ScriptWithSourceMapBundle that takes a virtual path, a cdnPath,
        /// allows code minification to be toggled, preservation of important comments, and custom bundle extension.
        /// </summary>
        /// <param name="virtualPath">Virtual path for the bundle to be accessible on.</param>
        /// <param name="cdnPath">CDN path for the bundle if available on a CDN too.</param>
        /// <param name="minifyCode">
        /// Minify code or not. Set to false to preserve variable names when needed (e.g. Angular),
        /// however the source code won't be minified to the full extent, only whitespace will be removed.
        /// </param>
        /// <param name="preserveImportantComments">Toggle to preserve important comments when needed (e.g. legal requirement for a library)</param>
        /// <param name="sourceMapExtension">
        /// Adds the desired extension to the source map, in case this isn't set, "map" will be appended at the end of the current bundle name.
        /// In case you want to use custom extension you need to add a custom handler in your web.config file for the source map to work.
        /// Please check https://stackoverflow.com/a/12931522
        /// </param>
        public ScriptWithSourceMapBundle(string virtualPath, string cdnPath, bool minifyCode, bool preserveImportantComments, string sourceMapExtension)
            : base(virtualPath, cdnPath, new IBundleTransform[] { })
        {
            this.Builder = new ScriptWithSourceMapBundleBuilder()
            {
                minifyCode = minifyCode,
                preserveImportantComments = preserveImportantComments,
                sourceMapExtension = sourceMapExtension
            };
        }

        internal const string DefaultSourceMapExtension = "map";

        // Don't allow the transforms constructor as we wouldn't be able to generated source mapping if it gets transformed
    }
}
