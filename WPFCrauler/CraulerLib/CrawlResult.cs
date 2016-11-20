using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CraulerLib
{
    public class CrawlResult
    {
        private List<CrawlResult> _childs;
        public List<CrawlResult> Childs
        {
            get
            {
                return _childs; 
            }
            set
            {
                _childs = value;
            }
        }
        public string Url { get; private set; }
        
        public CrawlResult(string url)
        {
            Url = url;
            _childs = new List<CrawlResult>();
        }
        private void Print(List<CrawlResult> craulResult,string indent, int level, ref string result)
        {
            foreach (CrawlResult node in craulResult)
            {
                string startsWith = level == 0 ? $"root{craulResult.IndexOf(node)+1} " : $"|> level{level} ";
                result = $"{result}{indent}{startsWith}({node.Url})\n";
                Print(node._childs, indent + "   ",level+1, ref result);
            }
        }
        public override String ToString()
        {
            string result = String.Empty;
            Print(this.Childs, String.Empty, 0, ref result);
            return result;
        }
    }
}
