using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace WPFCrauler
{
    internal class RootResourcesElement : ConfigurationElement
    {
        [ConfigurationProperty("href")]
        internal string href
        {
            get
            {
                return base["href"] as string;
            }
        }
    }
}
