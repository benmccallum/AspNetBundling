using System.Web.Optimization;

namespace AspNetBundling
{
    internal class AdHocBundle : Bundle
    {
        public string Content { get; private set; }

        public AdHocBundle(string virtualPath)
            : base(virtualPath)
        {
            Builder = new AdHocBundleBuilder();
        }

        internal void SetContent(string sourceMapContent)
        {
            Content = sourceMapContent;
        }
    }
}
