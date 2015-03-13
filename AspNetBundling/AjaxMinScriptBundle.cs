using System.Web.Optimization;

namespace AspNetBundling
{
    /// <summary>
    /// Represents a list of file references to be bundled together as a single resource
    /// using AjaxMin as the minifier and a custom bundle builder (<see cref="AspNetBundling.AjaxMinScriptBundleBuilder"/>).
    /// </summary>
    public class AjaxMinScriptBundle : Bundle
    {
        public AjaxMinScriptBundle(string virtualPath)
            : this(virtualPath, null)
        {

        }

        public AjaxMinScriptBundle(string virtualPath, string cdnPath)
            : base(virtualPath, cdnPath, new IBundleTransform[] { })
        {
            this.Builder = new AjaxMinScriptBundleBuilder();
            //base.ConcatenationToken = ";" + Environment.NewLine;
        }
    }
}