using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFCrauler
{
    interface IModel
    {
        string ResultUrlTree { get;  }
        void DoCraulingAsync(object obj);
        bool CanCrauling(object obj);
        event EventHandler<ExceptionEventArgs> ExceptionOccured;
    }
}
