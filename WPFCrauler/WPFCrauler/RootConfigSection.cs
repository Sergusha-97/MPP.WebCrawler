using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace WPFCrauler
{
    internal class RootConfigSection : ConfigurationSection
    {
        [ConfigurationProperty("rootResources")]
        internal RootResourcesCollection RootResources
        {
            get
            {
                return base["rootResources"] as RootResourcesCollection;
            }
        }
        [ConfigurationProperty("Depth")]
        internal DepthElement Depth
        {
            get
            {
                return base["Depth"] as DepthElement;
            }
        }

    }
}
