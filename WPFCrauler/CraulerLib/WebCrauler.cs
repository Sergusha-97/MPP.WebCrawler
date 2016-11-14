using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using AngleSharp.Dom.Html;
using AngleSharp.Parser.Html;
using AngleSharp.Dom;

namespace CraulerLib
{
    public class WebCrauler : IWebCrauler
    {
        private int depth;
        public WebCrauler(int depth)
        {
            if (depth < 0)
            {
                depth = 1;
            }
            this.depth = depth;
        }

        public Task<CraulResult> PerformCraulingAsync(IEnumerable<string> rootUrls)
        {
            return Task<CraulResult>.Run(async () =>
            {
                CraulResult result = new CraulResult("root");
                AddChildsToResult(result, rootUrls);
                await CraulRootAsync(result, 0);
                return result;
            }
            );

        }
        private async Task CraulRootAsync(CraulResult result, int level)
        {
            if (level > depth)
            {
                return;
            }
            WebClient webClient = new WebClient();
            foreach (var rootUrl in result.Childs)
            {
                string htmlPage = await webClient.DownloadStringTaskAsync(rootUrl.Url);
                IEnumerable<string> newRootUrls = await GetRootUrlsAsync(htmlPage);
                AddChildsToResult(rootUrl, newRootUrls);
                await CraulRootAsync(rootUrl, level + 1);                
            }
        }
        private async Task<IEnumerable<string>> GetRootUrlsAsync(string htmlPage)
        {

            IEnumerable<string> RootUrlsList = new List<string>();
            IHtmlDocument angle = await new HtmlParser().ParseAsync(htmlPage);
            foreach (IElement element in angle.QuerySelectorAll("a"))
                ((List<string>)RootUrlsList).Add(element.GetAttribute("href"));
            return RootUrlsList;
        }
        private void AddChildsToResult(CraulResult result, IEnumerable<string> rootUrls)
        {
            foreach ( string url in rootUrls)
            {
                if (url.StartsWith("http"))
                {
                    result.Childs.Add(new CraulResult(url));
                }

            }
        }
    }
}
