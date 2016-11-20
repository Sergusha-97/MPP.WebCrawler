using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using CraulerLib;
using System.Windows;

namespace WPFCrauler
{
    class ApplicationViewModel 
    {
        private  AbstractModel currentModel;
        private RelayCommand doCommand;
        private  void OnExceptionOccured(object obj, ExceptionEventArgs e)
        {
            MessageBox.Show(e.OccuredException.Message, "Error!",MessageBoxButton.OK, MessageBoxImage.Error);
        }
        public RelayCommand DoCommand
        {
            get
            {
                return doCommand;
            }
        }
        public AbstractModel Model
        {
            get
            {
                return currentModel;
            }
            set
            {
                currentModel = value;
            }
        }

        public ApplicationViewModel(AbstractModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException();
            }
            currentModel = model;
            doCommand = new RelayCommand(currentModel.DoCraulingAsync, currentModel.CanCrauling);
            currentModel.ExceptionOccured += OnExceptionOccured;
        }
    }
}
