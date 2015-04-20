using System.Web.Optimization;

namespace AspNetBundling
{
    internal class SourceMapBundle : Bundle
    {
        public string Content { get; private set; }

        public SourceMapBundle(string virtualPath)
            : base(virtualPath)
        {
            Builder = new SourceMapBundleBuilder(this);
        }

        internal void SetContent(string sourceMapContent)
        {
            Content = sourceMapContent;
        }
    }
}
