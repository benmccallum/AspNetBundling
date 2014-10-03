using System.Web.Optimization;

namespace AspNetBundling
{
    /// <summary>
    /// Represents a list of file references to be bundled together as a single resource
    /// using AjaxMin as the minifier and a custom bundle builder (<see cref="Ten.Website.Helpers.Bundling.AjaxMinBundleBuilder"/>).
    /// </summary>
    public class AjaxMinBundle : Bundle
    {
        public readonly BundleFileTypes BundleFileType;

        public AjaxMinBundle(string virtualPath, BundleFileTypes bundleFileType)
            : this(virtualPath, null, bundleFileType)
        {

        }

        public AjaxMinBundle(string virtualPath, string cdnPath, BundleFileTypes bundleFileType)
            : base(virtualPath, cdnPath, new IBundleTransform[] { })
        {
            this.BundleFileType = bundleFileType;
            this.Builder = new AjaxMinBundleBuilder(bundleFileType);
            //base.ConcatenationToken = ";" + Environment.NewLine;
        }
    }
}