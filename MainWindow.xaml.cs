using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection;
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

namespace OperatingSystemFilter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public class OS
    {
        public string[] Name =
        {
            "IOS",
            "Android",
            "Common"
        };
    }
    public class Phone
    {
        public string Name{ get; set; }
        public string Os { get; set; }
    }
    public partial class MainWindow : Window
    {
        public OS os = new OS();

        public string recentOS {  get; set; }
        
        public Phone[] list = new Phone[] {
            new Phone { Name = "iPhone 13", Os = "IOS" },
            new Phone { Name = "iPhone 12", Os = "IOS" },
            new Phone { Name = "iPhone 11", Os = "IOS" },
            new Phone { Name = "iPhone X", Os = "IOS" },
            new Phone { Name = "iPhone SE", Os = "IOS" },
            new Phone { Name = "iPhone 8", Os = "IOS" },
            new Phone { Name = "iPhone 7", Os = "IOS" },
            new Phone { Name = "iPhone 6S", Os = "IOS" },
            new Phone { Name = "iPhone SE (1st Gen)", Os = "IOS" },
            new Phone { Name = "iPhone 5S", Os = "IOS" },
            new Phone { Name = "Samsung Galaxy S21", Os = "Android" },
            new Phone { Name = "Samsung Galaxy S20", Os = "Android" },
            new Phone { Name = "Samsung Galaxy S10", Os = "Android" },
            new Phone { Name = "Samsung Galaxy Note 20", Os = "Android" },
            new Phone { Name = "Samsung Galaxy Note 10", Os = "Android" },
            new Phone { Name = "Samsung Galaxy S9", Os = "Android" },
            new Phone { Name = "Samsung Galaxy S8", Os = "Android" },
            new Phone { Name = "Google Pixel 6", Os = "Android" },
            new Phone { Name = "Google Pixel 5", Os = "Android" },
            new Phone { Name = "Google Pixel 4", Os = "Android" },
            new Phone { Name = "Common Phone 1", Os = "Common" },
            new Phone { Name = "Common Phone 2", Os = "Common" },
            new Phone { Name = "Common Phone 3", Os = "Common" },
            new Phone { Name = "Common Phone 4", Os = "Common" },
            new Phone { Name = "Common Phone 5", Os = "Common" },
            new Phone { Name = "Common Phone 6", Os = "Common" },
            new Phone { Name = "Common Phone 7", Os = "Common" },
            new Phone { Name = "Common Phone 8", Os = "Common" },
            new Phone { Name = "Common Phone 9", Os = "Common" },
            new Phone { Name = "Common Phone 10", Os = "Common" }
        };
        public MainWindow()
        {
            InitializeComponent();
        }
        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            comboBox.ItemsSource = os.Name;
        }


        private void comboBox_Selected(object sender, SelectionChangedEventArgs e)
        {
            recentOS = (string)(comboBox.SelectedItem);
            int count = 0;
            for (int i = 0; i < list.Length; i++)
            {
                if(recentOS == list[i].Os)
                {
                    count++;
                }

            }
            List<int> pages = new List<int>();
            for (int i = 1; i <= (int)(count / 7) + 1; i++)
            {
                pages.Add(i);
            }
            comboPage.ItemsSource = pages;
            comboPage.SelectedItem = 1;
            displayDataGrid(0);
        }

        private void comboPage_Selected(object sender, SelectionChangedEventArgs e)
        {
            int pageNow = (int)(comboPage.SelectedItem)-1;
            displayDataGrid(pageNow);
        }

        private void displayDataGrid(int pageNow)
        {
            int count = 0;
            List<Phone> product = new List<Phone>();
            for (int i = 0; i < list.Length; i++)
            {
                if (recentOS == list[i].Os)
                {
                    if ((count >= (pageNow) * 7) && (count <= (pageNow) * 7 + 6))
                    {
                        product.Add(list[i]);
                    }

                    count++;

                }
            }
            ListPhone.ItemsSource = product;
        }
        private void DataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            Phone dataItem = e.Row.DataContext as Phone;
            e.Row.PreviewMouseDown += (s, args) =>
            {
                DataContext = dataItem;
            };
        }

        private void btnPrevious_Click(object sender, RoutedEventArgs e)
        {
            if(comboPage.SelectedIndex > 0)
            {
                comboPage.SelectedIndex--;
            }
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            if (comboPage.SelectedIndex < comboPage.Items.Count-1)
            {
                comboPage.SelectedIndex++;
            }
        }
    }
}
