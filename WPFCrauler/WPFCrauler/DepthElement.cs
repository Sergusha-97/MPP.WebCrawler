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
                int? result = base["Value"] as int?;
                return result ?? 6;
            }
        }
    }
}
