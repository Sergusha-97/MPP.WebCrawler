using AngleSharp.Dom;
using AngleSharp.Dom.Html;
using AngleSharp.Parser.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CraulerLib
{
    public class Parser : IParser
    {
        public async Task<IEnumerable<string>> GetRootUrlsAsync(string htmlPage)
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
    }
}
