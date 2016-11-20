using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CraulerLib
{
    public interface IWebCrawler
    {
        Task<CrawlResult> PerformCraulingAsync(IEnumerable<string> rootUrls);
    }
}
