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
using System.Threading;

namespace FindServices
{
    public partial class MainWindow : Window
    {
        private Dictionary<String, String> _dictRegTypes;
        private List<Types> _listTypes;
        private Search _search;
        private String _regType = String.Empty;

        public MainWindow()
        {
            InitializeComponent();

            _dictRegTypes = new Dictionary<String, String>();
            _dictRegTypes.Add("Print", "_ipp._tcp");
            _dictRegTypes.Add("Music", "_daap._tcp");

            _search = new Search();
            _listTypes = new List<Types>();

            Thread thread = new Thread(StartFind);
            thread.Name = "Find Service";
            thread.Start();
            
        }

        #region Events

        private void cmbItemService_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            String textKey = (e.AddedItems[0] as ComboBoxItem).Content as string;
            _search.ClearServices();
            _listTypes.Clear();
            for (int i = ltbServices.Items.Count - 1; i >= 0; i--)
                ltbServices.Items.RemoveAt(i);

            _regType = _dictRegTypes.FirstOrDefault(k => k.Key == textKey).Value;
        }

        #endregion

        #region Methods

        private void StartFind()
        {
            while (true)
            {
                if (!String.IsNullOrWhiteSpace(_regType))
                {
                    System.Diagnostics.Debug.WriteLine("In TimerCallback: " + DateTime.Now);
                    ltbServices.Dispatcher.Invoke(
                        new Action(() =>
                        {
                            _search.FindAll(_regType, "local.");
                            foreach (var item in _search.ServicesTypes)
                            {
                                if (!_listTypes.Any(k => k.Name.Equals(item.Name)))
                                {
                                    _listTypes.Add(item);
                                    ltbServices.Items.Add(item.Name);
                                }
                            }
                        })
                    , null);
                }
                Thread.Sleep(1000);
            }
        }

        #endregion
    }
}
