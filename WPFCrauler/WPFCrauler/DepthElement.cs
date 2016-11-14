using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace WPFCrauler
{
    internal class DepthElement : ConfigurationElement
    {
        [ConfigurationProperty("value")]
        internal int Value
        {
            get
            {
                int defaultResult = 6;
                int result = (int)base["value"];
                return result > defaultResult ? defaultResult : result;
                
            }
        }
    }
}
