﻿using System;
using System.IO;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using App.Infrastructure.Amd;
using App.Infrastructure.Cassette;
using Cassette.Scripts;
using Cassette.Stylesheets;
using Cassette.Views;
using Newtonsoft.Json;

namespace App.Infrastructure.Web
{
    public class HtmlFormatter : MediaTypeFormatter
    {
        public HtmlFormatter()
        {
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/xhtml+xml"));
        }

        public override bool CanReadType(Type type)
        {
            return false;
        }

        public override bool CanWriteType(Type type)
        {
            return true;
        }

        public override async System.Threading.Tasks.Task WriteToStreamAsync(Type type, object value, Stream stream, HttpContentHeaders contentHeaders, System.Net.TransportContext transportContext)
        {
            contentHeaders.ContentType = new MediaTypeHeaderValue("text/html");

            var page = value as Page;

            var filename = Path.Combine(HttpRuntime.AppDomainAppPath, page.HtmlFile ?? "app.html");
            using (var file = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (var reader = new StreamReader(file))
                {
                    Bundles.Reference<StylesheetBundle>("Infrastructure/Scripts/Vendor");
                    Bundles.Reference<ScriptBundle>("Infrastructure/Scripts/Vendor");
                    
                    var html = await reader.ReadToEndAsync();

                    html = html.Replace("$lang$", page.Language);
                    html = html.Replace("$styles$", Bundles.RenderStylesheets().ToHtmlString());
                    html = html.Replace("$scripts$", Bundles.RenderScripts().ToHtmlString());
                    html = html.Replace("$styleMap$", StylesheetPathProvider.PathMapJson);
                    html = html.Replace("$requirejson$", JsonConvert.SerializeObject(AmdModuleCollection.Instance.Require));

                    var bytes = Encoding.UTF8.GetBytes(html);
                    await stream.WriteAsync(bytes, 0, bytes.Length);
                }
            }
        }
    }
}