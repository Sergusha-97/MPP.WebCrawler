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
        private void Print(List<CraulResult> craulResult,string indent, int level, ref string result)
        {
            foreach (CraulResult node in craulResult)
            {
                result = $"{result}{indent}{node.Url}\n";
                Print(node._childs, indent + " ",level+1,ref result);
            }
            indent = indent.Substring(0, indent.Length - 1 < 0 ? 0 : indent.Length - 1);
        }
        public override String ToString()
        {
            string result = String.Empty;
            Print(this.Childs, String.Empty, 0, ref result);
            return result;
        }
    }
}
