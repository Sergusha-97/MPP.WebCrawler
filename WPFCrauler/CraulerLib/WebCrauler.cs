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
        public AggregateException EncounteredErrors { get; private set; }
        private List<Exception> errorList;

        public WebCrauler(int depth)
        {
            if (depth < 0)
            {
                depth = 1;
            }
            this.depth = depth;
            errorList = new List<Exception>();
        }

        public Task<CraulResult> PerformCraulingAsync(IEnumerable<string> rootUrls)
        {
            return Task<CraulResult>.Run(async () =>
            {
                CraulResult result = new CraulResult("root");                     
                AddChildsToResult(result, rootUrls);
                await CraulRootAsync(result, 0);
                if (errorList.Count != 0)
                {
                    EncounteredErrors = new AggregateException(errorList);
                }     
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
            foreach (var rootUrl in result.Childs)
            {
                string htmlPage="";
                IEnumerable<string> newRootUrls = new List<string>();
                WebClient webClient = new WebClient();
                try
                {
                    htmlPage = await webClient.DownloadStringTaskAsync(rootUrl.Url);
                    newRootUrls = await GetRootUrlsAsync(htmlPage);
                    AddChildsToResult(rootUrl, newRootUrls);
                }
                catch (Exception e)
                {
                    errorList.Add(e);
                }  
                await CraulRootAsync(rootUrl, level + 1);                
            }
        }
        private async Task<IEnumerable<string>> GetRootUrlsAsync(string htmlPage)
        {

            IEnumerable<string> RootUrlsList = new List<string>();
            IHtmlDocument angle = await new HtmlParser().ParseAsync(htmlPage);
            string url;
            foreach (IElement element in angle.QuerySelectorAll("a"))
            {
                url = element.GetAttribute("href");
                if (url != null)
                {
                    ((List<string>)RootUrlsList).Add(url);
                }              
            }      
            return RootUrlsList;
        }
        private void AddChildsToResult(CraulResult result, IEnumerable<string> rootUrls)
        {
                foreach (string url in rootUrls)
                {
                    if (url.StartsWith("http"))
                    {
                        result.Childs.Add(new CraulResult(url));
                    }
                }

        }
    }
}
