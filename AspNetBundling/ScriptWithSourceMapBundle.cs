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
            : this(virtualPath, null, false)
        {

        }

        /// <summary>
        /// Initializes a new instance of the ScriptWithSourceMapBundle class that takes a virtual path and a cdnPath for the bundle.
        /// </summary>
        public ScriptWithSourceMapBundle(string virtualPath, string cdnPath)
            : this(virtualPath, cdnPath, false)
        {

        }
        
        /// <summary>
        /// Initializes a new instance of the ScriptWithSourceMapBundle class that takes a virtual path 
        /// and allows code minification to be toggled.
        /// </summary>
        /// <param name="minifyCode">Preserve variables names in scripts </param>
        public ScriptWithSourceMapBundle(string virtualPath, bool minifyCode)
            : this(virtualPath, null, minifyCode)
        {

        }

        /// <summary>
        /// Initializes a new instance of the ScriptWithSourceMapBundle that takes a virtual path, a cdnPath
        /// and allows code minifcation to be toggled.
        /// </summary>
        /// <param name="virtualPath"></param>
        /// <param name="minifyCode">
        /// Minify code or not. Set to false to preserve variable names when needed (e.g. Angular),
        /// however the source code won't be minified to the full extent, only whitespace will be removed.
        /// </param>
        public ScriptWithSourceMapBundle(string virtualPath, string cdnPath, bool minifyCode)
            : base(virtualPath, cdnPath, new IBundleTransform[] { })
        {
            this.Builder = new ScriptWithSourceMapBundleBuilder() { minifyCode = minifyCode };
        }

        // Don't allow the transforms constructor as we wouldn't be able to generated source mapping if it gets transformed
    }
}
