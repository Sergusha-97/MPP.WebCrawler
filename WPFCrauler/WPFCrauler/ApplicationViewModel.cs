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
    class ApplicationViewModel : INotifyPropertyChanged
    {
        private IModel currentModel;
        private RelayCommand doCommand;
        public RelayCommand DoCommand
        {
            get
            {
                return doCommand;
            }
        }
        public string ResultUrlTree
        {
            get
            {
                return currentModel.ResultUrlTree;
            }
            set
            {
                currentModel.ResultUrlTree = value;
                OnPropertyChanged("ResultUrlTree");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        public ApplicationViewModel(IModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException();
            }
            currentModel = model;
            doCommand = new RelayCommand(currentModel.DoCrauling, currentModel.CanCrauling);
        }
    }
}
