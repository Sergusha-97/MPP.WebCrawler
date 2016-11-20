using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Serilog;
using Serilog.Exceptions;
using System.Configuration;

namespace WPFCrauler
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            string path = ConfigurationManager.AppSettings["LogFolder"];
            ILogger logger = new LoggerConfiguration()
                .WriteTo.RollingFile(path+"Log-{Date}.txt")
                .CreateLogger();
            DataContext = new ApplicationViewModel(new Model(logger));
        }
    }
}
