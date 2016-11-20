using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using AngleSharp.Dom.Html;
using AngleSharp.Parser.Html;
using AngleSharp.Dom;
using Serilog;

namespace CraulerLib
{
    public class WebCrawler : IWebCrawler
    {
        private int depth;
        private IParser parser;
        private ILogger logger;

        public WebCrawler(int depth, IParser parser, ILogger logger): this(depth, parser)
        {
            if (logger == null)
            {
                throw new ArgumentOutOfRangeException();
            }
            this.logger = logger;
        }
        public WebCrawler(int depth, IParser parser)
        {
            if (parser == null)
            {
                throw new ArgumentOutOfRangeException();
            }
            this.parser = parser;
            if (depth < 0)
            {
                depth = 1;
            }
            this.depth = depth;
        }

        public async Task<CrawlResult> PerformCraulingAsync(IEnumerable<string> rootUrls)
        {
            CrawlResult result = new CrawlResult("root");                     
            AddChildsToResult(result, rootUrls);
            await CraulRootAsync(result, 0).ConfigureAwait(continueOnCapturedContext: false);   
            return result;
        }
        private async Task CraulRootAsync(CrawlResult result, int level)
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
                    htmlPage = await webClient.DownloadStringTaskAsync(rootUrl.Url).ConfigureAwait(continueOnCapturedContext: false);
                    newRootUrls = await parser.GetRootUrlsAsync(htmlPage).ConfigureAwait(continueOnCapturedContext:false);
                    AddChildsToResult(rootUrl, newRootUrls);
                }
                catch (Exception e)
                {
                    if (logger != null)
                    {
                        logger.Error(e.Message);
                    }
                }  
                await CraulRootAsync(rootUrl, level + 1);                
            }
        }
        private void AddChildsToResult(CrawlResult result, IEnumerable<string> rootUrls)
        {
                foreach (string url in rootUrls)
                {
                    if (url.StartsWith("http"))
                    {
                        result.Childs.Add(new CrawlResult(url));
                    }
                }

        }
    }
}
