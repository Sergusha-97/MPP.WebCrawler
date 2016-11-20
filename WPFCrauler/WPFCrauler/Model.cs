using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using CraulerLib;
using Serilog;
using Serilog.Exceptions;

namespace WPFCrauler
{
    public class Model :  AbstractModel
    {

        private ILogger logger;
        private int depth;
        private IEnumerable<string> rootUrls;
        private bool canExecute = true;      
        public override bool  CanCrauling(object obj)
        {
            return canExecute;
        }
        private string resultUrlsTree;
        private CrawlResult resultUrl;
        public Model(ILogger logger)
        {
            if (logger == null)
            {
                throw new ArgumentOutOfRangeException();
            }
            this.logger = logger;
        }
        public override string ResultUrlTree
        {
            get
            {
                return resultUrlsTree;
            }
            protected set
            {
                resultUrlsTree = value;
                OnPropertyChanged("ResultUrlTree");
            }
        }
        public override CrawlResult ResultUrl
        {
            get
            {
                return resultUrl;
            }
            protected set
            {
                resultUrl = value;
                OnPropertyChanged("ResultUrl");
            }
        }
        public override async void DoCraulingAsync(object obj)
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
                    IWebCrawler webCrauler = new WebCrawler(depth, new Parser(),logger);
                    ResultUrl = await webCrauler.PerformCraulingAsync(rootUrls);
                    ResultUrlTree = ResultUrl.ToString();
                }
                catch
                {
                    throw new Exception("Error in WebCrawling");
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
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
