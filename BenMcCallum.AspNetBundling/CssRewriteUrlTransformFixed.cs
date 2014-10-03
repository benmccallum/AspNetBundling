namespace System.Web.Optimization
{
    using System.Text.RegularExpressions;

    /// <summary>
    /// Fix for the standard System.Web.Optimization.CssRewriteUrlTransform which doesn't play nice with data URIs.
    /// I've logged the bug on codeplex, but this will have to do for now: https://aspnetoptimization.codeplex.com/workitem/88
    /// </summary>
    public class CssRewriteUrlTransformFixed : IItemTransform
    {
        private static string RebaseUrlToAbsolute(string baseUrl, string url)
        {
            if (string.IsNullOrWhiteSpace(url) || string.IsNullOrWhiteSpace(baseUrl) || url.StartsWith("/", StringComparison.OrdinalIgnoreCase)
				|| url.StartsWith("data:") || url.StartsWith("http://") || url.StartsWith("https://"))
            {
                return url;
            }
            if (!baseUrl.EndsWith("/", StringComparison.OrdinalIgnoreCase))
            {
                baseUrl += "/";
            }
            return VirtualPathUtility.ToAbsolute(baseUrl + url);
        }
        private static string ConvertUrlsToAbsolute(string baseUrl, string content)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                return content;
            }
            var regex = new Regex("url\\(['\"]?(?<url>[^)]+?)['\"]?\\)");
            return regex.Replace(content, (Match match) => "url(" + CssRewriteUrlTransformFixed.RebaseUrlToAbsolute(baseUrl, match.Groups["url"].Value) + ")");
        }
        public string Process(string includedVirtualPath, string input)
        {
            if (includedVirtualPath == null)
            {
                throw new ArgumentNullException("includedVirtualPath");
            }
            var directory = VirtualPathUtility.GetDirectory(includedVirtualPath.Substring(1));
            return CssRewriteUrlTransformFixed.ConvertUrlsToAbsolute(directory, input);
        }
    }
}