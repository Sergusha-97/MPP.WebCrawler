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
        private Exception resultException;
        public string ResultUrlTree
        {
            get
            {
                return resultUrlsTree;
            }
            private set
            {
                resultUrlsTree = value;
                OnPropertyChanged("ResultUrlTree");
            }
        }
        public event EventHandler<ExceptionEventArgs> ExceptionOccured;
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string prop)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(prop));
        }
        private void OnExceptionSet()
        {
            EventHandler<ExceptionEventArgs> handler = ExceptionOccured;
            if (handler != null)
            {
                handler(this, new ExceptionEventArgs(resultException));
            }

        }
        public bool  CanCrauling(object obj)
        {
            return canExecute;
        }
        public async void DoCraulingAsync(object obj)
        {
            try
            {
                canExecute = false;
                try
                {
                    RootConfigSection config = System.Configuration.
                        ConfigurationManager.GetSection(RootConfigSection.SECTION_NAME) as RootConfigSection;
                    depth = config.Depth.Value;
                    rootUrls = config.RootResources;
                }
                catch
                {
                    throw new Exception("Error in app.config!");
                }
                try
                {
                    IWebCrauler webCrauler = new WebCrauler(depth);
                    CraulResult result = await webCrauler.PerformCraulingAsync(rootUrls);
                    ResultUrlTree = await Task<string>.Run(() => { return result.ToString(); });
                }
                catch
                {
                    throw new Exception("Error in WebCrawling");
                }
            }
            catch (Exception ex)
            {
                resultException = ex;
                OnExceptionSet();// Is private property will be good at this case?
            }
            finally
            {
                canExecute = true;
            }            
        }
    }
}
