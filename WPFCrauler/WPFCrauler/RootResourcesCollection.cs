using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace WPFCrauler
{
    [ConfigurationCollection(typeof(RootResourcesElement), AddItemName = "resource", CollectionType = ConfigurationElementCollectionType.BasicMap)]
    internal class RootResourcesCollection : ConfigurationElementCollection, IEnumerable<String>
    {
       internal RootResourcesElement this[int index]
        {
            get
            {
                return (RootResourcesElement)BaseGet(index);
            }
        }
        protected override ConfigurationElement CreateNewElement()
        {
            return new RootResourcesElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((RootResourcesElement)element).href;
        }
        public  new  IEnumerator<String> GetEnumerator() // Internal!!!&&&????
        {
            int count = base.Count;
            for (int i = 0; i < count; i++)
            {
                yield return (base.BaseGet(i) as RootResourcesElement).href;
            }
        }
    }
}
