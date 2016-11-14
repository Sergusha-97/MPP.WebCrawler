using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CraulerLib
{
    public class CraulResult
    {
        private List<CraulResult> _childs;
        public List<CraulResult> Childs
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
        
        public CraulResult(string url)
        {
            Url = url;
            _childs = new List<CraulResult>();
        }
    }
}
