using System.Diagnostics;

using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web.Hosting;
using System.Web.Optimization;

namespace AspNetBundling
{
    /// <summary>
    /// Represents a custom AjaxMin bundle builder for bundling from individual file contents.
    /// Generates source map files for JS and CSS.
    /// </summary>
    public class AjaxMinBundleBuilder : IBundleBuilder
    {
        private readonly BundleFileTypes bundleFileType;

        public AjaxMinBundleBuilder(BundleFileTypes bundleFileType)
        {
            this.bundleFileType = bundleFileType;
        }

        public string BuildBundleContent(Bundle bundle, BundleContext context, IEnumerable<BundleFile> files)
        {
            if (files == null)
            {
                return string.Empty;
            }
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            if (bundle == null)
            {
                throw new ArgumentNullException("bundle");
            }

            // Generates source map using an approach documented here: http://ajaxmin.codeplex.com/discussions/446616

            // Get paths and create any directories required
            var mapVirtualPath = bundle.Path + ".map";
            var sourcePath = HostingEnvironment.MapPath(bundle.Path);
            var mapPath = HostingEnvironment.MapPath(mapVirtualPath);
            var directoryPath = Path.GetDirectoryName(mapPath);
            if (directoryPath == null)
            {
                throw new InvalidOperationException("directoryPath was invalid.");
            }

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            // Concatenate file contents to be minified, including the sourcemap hints
            var contentConcated = new StringBuilder();
            foreach (var file in files)
            {
                var filePath = HostingEnvironment.MapPath(file.VirtualFile.VirtualPath);
                if (bundleFileType == BundleFileTypes.JavaScript)
                {
                    contentConcated.AppendLine(";///#SOURCE 1 1 " + filePath);
                }
                contentConcated.AppendLine(file.ApplyTransforms());
            }
            var contentConcatedString = contentConcated.ToString();

            // Try minify (+ source map) using AjaxMin dll
            try
            {
                var contentBuilder = new StringBuilder();
                using (var contentWriter = new StringWriter(contentBuilder))
                using (var mapWriter = new StreamWriter(mapPath, false, new UTF8Encoding(false)))
                using (var sourceMap = new V3SourceMap(mapWriter))
                {
                    sourceMap.StartPackage(sourcePath, mapPath);

                    var settings = new CodeSettings()
                    {
                        EvalTreatment = EvalTreatment.MakeImmediateSafe,
                        PreserveImportantComments = false,
                        SymbolsMap = sourceMap,
                        TermSemicolons = true
                    };

                    var minifier = new Minifier();
                    string contentMinified;
                    switch (bundleFileType)
                    {
                        case BundleFileTypes.JavaScript:
                            contentMinified = minifier.MinifyJavaScript(contentConcatedString, settings);
                            break;
                        case BundleFileTypes.StyleSheet:
                            var cssSettings = new CssSettings
                            {
                                TermSemicolons = true
                            };
                            contentMinified = minifier.MinifyStyleSheet(contentConcatedString, cssSettings, settings);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException("Unrecognised BundleFileTypes enum value. Could not find minifier method to handle.");
                    }
                    if (minifier.ErrorList.Count > 0)
                    {
                        return GenerateMinifierErrorsContent(contentConcatedString, minifier);
                    }

                    contentWriter.Write(contentMinified);

                    sourceMap.EndPackage();
                    sourceMap.EndFile(contentWriter, "\r\n");
                }

                return contentBuilder.ToString();
            }
            catch (Exception ex)
            {
                Trace.TraceWarning("An exception occurred trying to build bundle contents for bundle with virtual path: " + bundle.Path + ". See Exception details.", ex, typeof(AjaxMinBundleBuilder));
                return GenerateGenericErrorsContent(contentConcatedString);
            }
        }

        private static string GenerateGenericErrorsContent(string contentConcatedString)
        {
            var sbContent = new StringBuilder();
            sbContent.Append("/* ");
            sbContent.Append("An error occurred during minification, see Sitecore log for more details - returning concatenated content unminified.").Append("\r\n");
            sbContent.Append(" */\r\n");
            sbContent.Append(contentConcatedString);
            return sbContent.ToString();
        }

        private static string GenerateMinifierErrorsContent(string contentConcatedString, Minifier minifier)
        {
            var sbContent = new StringBuilder();
            sbContent.Append("/* ");
            sbContent.Append("An error occurred during minification, see errors below - returning concatenated content unminified.").Append("\r\n");
            foreach (var error in minifier.ErrorList)
            {
                sbContent.Append(error).Append("\r\n");
            }
            sbContent.Append(" */\r\n");
            sbContent.Append(contentConcatedString);
            return sbContent.ToString();
        }
    }
}
