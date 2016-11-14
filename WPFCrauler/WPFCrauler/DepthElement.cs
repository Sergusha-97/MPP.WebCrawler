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
        [ConfigurationProperty("Value")]
        internal int Value
        {
            get
            {
                int defaultResult = 6;
                int result;
                if (Int32.TryParse(base["Value"] as string,out result))
                {
                    return result > defaultResult ? defaultResult : result;
                }
                else
                {
                    return defaultResult;
                }
                
            }
        }
    }
}
