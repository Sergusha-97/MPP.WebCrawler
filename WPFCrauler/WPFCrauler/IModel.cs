using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFCrauler
{
    interface IModel
    {
        string ResultUrlTree { get; set; }
        void DoCrauling(object obj);
        bool CanCrauling(object obj);
    }
}
