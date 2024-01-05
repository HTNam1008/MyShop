using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
using System.Windows.Controls.Primitives;
using static System.Net.Mime.MediaTypeNames;
using MyShop.DTO;
using MyShop.DAO;
using MyShop.ViewModel;

namespace MyShop.Views.MainView.pages.ProductView
{
    /// <summary>
    /// Interaction logic for Product.xaml
    /// </summary>
    public partial class Product : Page
    {
        private ProductVM _productVM;

        public Product()
        {
            InitializeComponent();
            _productVM = new ProductVM();
            DataContext = _productVM;
        }

        private async void PageOpened(object sender, RoutedEventArgs e)
        {
            await _productVM.LoadDataAsync();
        }
        private void comboBox_Selected(object sender, SelectionChangedEventArgs e)
        {
            _productVM.ApplyFilters();
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            _productVM.ApplyFilters();
        }

        private void priceComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _productVM.ApplyFilters();
        }

    }

}
