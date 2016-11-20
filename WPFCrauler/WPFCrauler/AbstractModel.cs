using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace WPFCrauler
{
    public abstract class AbstractModel : INotifyPropertyChanged
    {
        public abstract CraulerLib.CrawlResult ResultUrl { get; protected set; }
        public abstract string ResultUrlTree { get; protected set; }
        public abstract void DoCraulingAsync(object obj);
        public abstract bool CanCrauling(object obj);
        public event EventHandler<ExceptionEventArgs> ExceptionOccured;
        public event PropertyChangedEventHandler PropertyChanged;
        protected Exception resultException;

        protected void OnPropertyChanged(string prop)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(prop));
        }
        protected void OnExceptionSet()
        {
            EventHandler<ExceptionEventArgs> handler = ExceptionOccured;
            if (handler != null)
            {
                handler(this, new ExceptionEventArgs(resultException));
            }

        }
    }
}
