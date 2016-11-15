using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFCrauler
{
    public class ExceptionEventArgs : EventArgs
    {
        public Exception OccuredException { get; private set; }
        public ExceptionEventArgs(Exception exception)
        {
            if (exception == null)
            {
                throw new ArgumentNullException();
            }
            OccuredException = exception;
                

        }
    }
}
