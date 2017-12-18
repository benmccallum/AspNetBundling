using System.Web.Optimization;

namespace AspNetBundling
{
    /// <summary>
    /// Represents a list of file references to be bundled together as a single resource
    /// using AjaxMin as the minifier and a custom bundle builder (<see cref="AspNetBundling.ScriptWithSourceMapBundleBuilder"/>).
    /// </summary>
    public class ScriptWithSourceMapBundle : Bundle
    {
        public ScriptWithSourceMapBundle(string virtualPath)
            : this(virtualPath, null)
        {

        }

        public ScriptWithSourceMapBundle(string virtualPath, string cdnPath)
            : this(virtualPath, cdnPath, false)
        {

        }
        
        /// <param name="preserveVariablesName">Preserve variables names in scripts </param>
        public ScriptWithSourceMapBundle(string virtualPath, bool preserveVariablesName)
            : this(virtualPath, null, preserveVariablesName)
        {

        }

        /// <param name="preserveVariablesName">Preserve variables names in scripts </param>
        public ScriptWithSourceMapBundle(string virtualPath, string cdnPath, bool preserveVariablesName)
            : base(virtualPath, cdnPath, new IBundleTransform[] { })
        {
            this.Builder = new ScriptWithSourceMapBundleBuilder(){preserveVariablesNames = preserveVariablesName};
        }

        // Don't allow the transforms constructor as we wouldn't be able to generated source mapping if it gets transformed
    }
}