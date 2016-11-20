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
    }
}
