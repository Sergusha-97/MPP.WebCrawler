using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using CraulerLib;

namespace WPFCrauler
{
    public class Model : INotifyPropertyChanged, IModel
    {
        private string resultUrlsTree;
        private int depth;
        private IEnumerable<string> rootUrls;
        private bool canExecute = true;
        public string ResultUrlTree
        {
            get
            {
                return resultUrlsTree;
            }
            set
            {
                resultUrlsTree = value;
                OnPropertyChanged("ResultUrlTree");
            }
        }
        public Model()
        {
            ResultUrlTree = "dghd";
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        public bool CanCrauling(object obj)
        {
            return canExecute;
        }
        public async void DoCrauling(object obj)
        {
            canExecute = false;
            RootConfigSection config = System.Configuration.
                ConfigurationManager.GetSection(RootConfigSection.SECTION_NAME) as RootConfigSection;
            depth = config.Depth.Value;
            rootUrls = config.RootResources;
            IWebCrauler webCrauler = new WebCrauler(depth);
            CraulResult result = await webCrauler.PerformCraulingAsync(rootUrls);
            ResultUrlTree = await Task<string>.Run(() => { return result.ToString(); });
            canExecute = true;
        }
        private async Task DoCraulingAsync()
        {
            try
            {
                
                RootConfigSection config;
                await Task.Run(() =>
                {
                    config = System.Configuration.
                    ConfigurationManager.GetSection(RootConfigSection.SECTION_NAME) as RootConfigSection;
                    depth = config.Depth.Value;
                    rootUrls = config.RootResources;
                });
                IWebCrauler webCrauler = new WebCrauler(depth);
                CraulResult result = await webCrauler.PerformCraulingAsync(rootUrls);
                ResultUrlTree = await Task<string>.Run(() => { return  result.ToString(); });
            }
            catch
            {

            }
        }


    }
}
