using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
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

namespace MyShop.pages
{
    /// <summary>
    /// Interaction logic for Product.xaml
    /// </summary>
    public partial class Product : Page
    {
        public string[] nameOS;
        public DataTable MobileData { get; set; } = new DataTable();
        public string recentOS { get; set; }

        public Product()
        {
            InitializeComponent();
        }
        private void PageOpened(object sender, RoutedEventArgs e)
        {
            Database.Instance.ImportDataToSQL();
            LoadDataIntoDataGrid();
        }
        private void LoadDataIntoDataGrid()
        {
            var sql = $"SELECT name, os, price, manufacturer, memorystorage, image FROM {Database.Instance.tableName}";
            var command = new SqlCommand(sql, Database.Instance.Connection);

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(MobileData);
            nameOS = MobileData.AsEnumerable().Select(row => row.Field<string>("os")).Distinct().ToArray();
            comboBox.ItemsSource = nameOS;
            comboBox.SelectedItem = nameOS[0];
        }


        private void comboBox_Selected(object sender, SelectionChangedEventArgs e)
        {
            recentOS = (string)(comboBox.SelectedItem);
            int count = 0;
            foreach (DataRow row in MobileData.Rows)
            {
                if (recentOS == row["os"].ToString())
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
            int pageNow = (int)(comboPage.SelectedItem) - 1;
            displayDataGrid(pageNow);
        }

        private void displayDataGrid(int pageNow)
        {
            int count = 0;
            DataTable filteredData = new DataTable();
            filteredData = MobileData.Clone(); // Tạo một bản sao của DataTable ban đầu


            foreach (DataRow row in MobileData.Rows)
            {
                if (recentOS == row["os"].ToString())
                {
                    if (count >= (pageNow) * 7 && count <= (pageNow) * 7 + 6)
                    {
                        DataRow newRow = filteredData.NewRow();
                        newRow.ItemArray = row.ItemArray;

                        filteredData.Rows.Add(newRow);
                    }

                    count++;
                }
            }

            ListPhone.ItemsSource = filteredData.DefaultView;
            var columnToHide = ListPhone.Columns.SingleOrDefault(c => c.Header.ToString() == "image");

            if (columnToHide != null)
            {
                columnToHide.Visibility = Visibility.Collapsed;
            }
        }
        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListPhone.SelectedItem != null && ListPhone.SelectedItems.Count == 1)
            {
                DataRowView selectedRow = ListPhone.SelectedItem as DataRowView;
                Phone phone = new Phone
                {
                    name = selectedRow["name"].ToString(),
                    os = selectedRow["os"].ToString(),
                    image = selectedRow["image"].ToString(),
                    manufacturer = selectedRow["manufacturer"].ToString(),
                    memoryStorage = selectedRow["memorystorage"].ToString(),
                };
                if (int.TryParse(selectedRow["price"].ToString(), out int priceValue))
                {
                    phone.price = priceValue;
                }
                else
                {
                    phone.price = 0;
                }
                SetDetails(phone);

            }

        }

        private void SetDetails(Phone phone)
        {
            this.DataContext = phone;
        }
        private void btnPrevious_Click(object sender, RoutedEventArgs e)
        {
            if (comboPage.SelectedIndex > 0)
            {
                comboPage.SelectedIndex--;
            }
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            if (comboPage.SelectedIndex < comboPage.Items.Count - 1)
            {
                comboPage.SelectedIndex++;
            }
        }

    }
}
