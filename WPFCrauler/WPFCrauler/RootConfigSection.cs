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
        internal const string SECTION_NAME = "root";
        [ConfigurationProperty("rootResources")]
        internal RootResourcesCollection RootResources
        {
            get
            {
                return base["rootResources"] as RootResourcesCollection;
            }
        }
        [ConfigurationProperty("depth")]
        internal DepthElement Depth
        {
            get
            {
                return base["depth"] as DepthElement;
            }
        }

    }
}
