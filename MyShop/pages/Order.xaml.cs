using System;
using System.Collections.Generic;
using System.Configuration;
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

namespace MyShop.pages
{
    /// <summary>
    /// Interaction logic for Order.xaml
    /// </summary>
    public partial class Order : Page
    {
        public Order()
        {
            InitializeComponent();
            Unloaded += Page_Unloaded;
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings["LastClosedPage"].Value = "pages/Order.xaml";
            config.Save(ConfigurationSaveMode.Minimal);

            ConfigurationManager.RefreshSection("appSettings");
        }
    }
}
