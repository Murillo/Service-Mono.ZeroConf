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
using ServiceDiscovery;


namespace CreateService
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Types type = new Types();
            type.Name = "Murillo Fake Printer";
            type.RegType = "_ipp._tcp";
            type.Domain = "local.";
            type.Port = 1235;
            type.Txt = new List<TypeDescription>{ new TypeDescription { Key = "Password", Value = "False" } };
            Register register = new Register();
            register.RegisterService(type);
        }
    }
}
