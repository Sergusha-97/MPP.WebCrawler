using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace WPFCrauler
{
    internal class Configuration
    {
        internal readonly IEnumerable<string> Resources;
        internal readonly int Depth;
        /*
         CustomApplicationConfigSection config = System.Configuration.ConfigurationManager.GetSection(CustomApplicationConfigSection.SECTION_NAME) as CustomApplicationConfigSection;
         */
        internal Configuration(RootConfigSection RootConfig)
        {
            if (RootConfig == null)
            {
                throw new ArgumentNullException();
            }
            Depth = RootConfig.Depth.Value;
            Resources = RootConfig.RootResources;
        }
    }
}
