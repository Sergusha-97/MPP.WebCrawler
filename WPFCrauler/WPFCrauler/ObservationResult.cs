using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using CraulerLib;

namespace WPFCrauler
{
    public class ObservableResult 
    {
        private string title;
        private readonly ObservableCollection<ObservableResult> items;

        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                title = value;
            }
        }

        public ObservableCollection<ObservableResult> Items
        {
            get
            {
                return items;
            }
        }

        public ObservableResult() 
        {
            items = new ObservableCollection<ObservableResult>();
        }

        public static ObservableResult CreateFromCrawlingResult(CrawlResult result)
        {
            ObservableResult ci = new ObservableResult();
            ci.Title = result.Url;
            foreach (var nestedResults in result.Childs)
            {
                ci.Items.Add(CreateFromCrawlingResult(nestedResults));
            }
            return ci;
        }
    }
}
