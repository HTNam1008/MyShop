using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
using static System.Net.Mime.MediaTypeNames;
using ExcelDataReader;

namespace MyShop.pages
{
    /// <summary>
    /// Interaction logic for Product.xaml
    /// </summary>
    ///  
    public class Phone
    {
        public string name { get; set; }
        public string os { get; set; }

        public string image { get; set; }
    }
    public partial class Product : Page
    {
        public string[] nameOS;
        public DataTable OperatingSystemData { get; set; } = new DataTable();
        public string recentOS {  get; set; }
        
        public Product()
        {
            InitializeComponent();
        }
        private void PageOpened(object sender, RoutedEventArgs e)
        {
            ImportDataToSQL();
            LoadDataIntoDataGrid();
        }
        private void LoadDataIntoDataGrid()
        {
            var sql = "SELECT name, os, image FROM Mobile1";
            var command = new SqlCommand(sql, DB.Instance.Connection);

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(OperatingSystemData);
            nameOS = OperatingSystemData.AsEnumerable().Select(row => row.Field<string>("os")).Distinct().ToArray();
            comboBox.ItemsSource = nameOS;
            comboBox.SelectedItem = nameOS[0];
        }
         private void ImportDataToSQL()
        {
            // Mở hộp thoại chọn file Excel
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Excel Files|*.xls;*.xlsx"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                string excelFilePath = openFileDialog.FileName;

                // Chuỗi kết nối đến cơ sở dữ liệu SQL Server
                string connectionString = "Server=DESKTOP-8IL3H18\\SQLEXPRESS01;Database=ImportExcel;Integrated Security=True;";

                try
                {
                    // Mở kết nối đến SQL Server
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        // Đọc dữ liệu từ file Excel
                        using (var stream = System.IO.File.Open(excelFilePath, System.IO.FileMode.Open, System.IO.FileAccess.Read))
                        {
                            using (var reader = ExcelDataReader.ExcelReaderFactory.CreateReader(stream))
                            {
                                var dataSet = reader.AsDataSet(new ExcelDataSetConfiguration()
                                {
                                    ConfigureDataTable = (_) => new ExcelDataTableConfiguration() { UseHeaderRow = true }
                                });
                                DataTable dataTable = dataSet.Tables[0];

                                // Loại bỏ các cột không có header
                                var columnsToRemove = new List<DataColumn>();
                                foreach (DataColumn column in dataTable.Columns)
                                {
                                    if (column.ColumnName.StartsWith("Column") || string.IsNullOrWhiteSpace(column.ColumnName))
                                    {
                                        columnsToRemove.Add(column);
                                    }
                                }

                                // Xóa các cột không có header
                                foreach (var columnToRemove in columnsToRemove)
                                {
                                    dataTable.Columns.Remove(columnToRemove);
                                }

                                foreach (DataColumn col in dataTable.Columns)
                                {
                                    col.ColumnName = ConvertToNoSpaceNoAccent(col.ColumnName);
                                }

                                // Tạo bảng mới trong cơ sở dữ liệu nếu bảng không tồn tại
                                CreateNewTable(connection, "Mobile1", dataTable);

                                // Import dữ liệu vào cơ sở dữ liệu
                                ImportData(connection, "Mobile1", dataTable);
                            }
                        }

                        MessageBox.Show("Data imported successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void comboBox_Selected(object sender, SelectionChangedEventArgs e)
        {
            recentOS = (string)(comboBox.SelectedItem);
            int count = 0;
            foreach (DataRow row in OperatingSystemData.Rows)
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
            int pageNow = (int)(comboPage.SelectedItem)-1;
            displayDataGrid(pageNow);
        }

        private void displayDataGrid(int pageNow)
        {
            int count = 0;
            DataTable filteredData = new DataTable();
            filteredData = OperatingSystemData.Clone(); // Tạo một bản sao của DataTable ban đầu

            foreach (DataRow row in OperatingSystemData.Rows)
            {
                if (recentOS == row["os"].ToString())
                {
                    if (count >= (pageNow) * 7 && count <= (pageNow) * 7 + 6)
                    {
                        filteredData.ImportRow(row);
                    }

                    count++;
                }
            }

            ListPhone.ItemsSource = filteredData.DefaultView;

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
                };
                SetDetails(phone);

            }

        }

        private void SetDetails(Phone phone)
        {
            this.DataContext = phone;
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

        private void CreateNewTable(SqlConnection connection, string tableName, DataTable dataTable)
        {
            using (SqlCommand command = new SqlCommand($"CREATE TABLE {tableName} (", connection))
            {
                string outputData = "";
                foreach (DataColumn col in dataTable.Columns)
                {
                    string columnType = GetSqlDbType(col.DataType);/*
                    outputData += columnType;
                    outputData += " ";*/

                    command.CommandText += $"{col.ColumnName} {columnType}, ";
                }
                command.CommandText = command.CommandText.TrimEnd(',', ' ') + ");";
                command.ExecuteNonQuery();
            }
        }

        private string ConvertToNoSpaceNoAccent(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return string.Empty;
            }

            // Loại bỏ khoảng trắng và chuyển về chữ thường
            string noSpaceNoAccent = input.Replace(" ", "").ToLowerInvariant();

            // Dùng thư viện System.Globalization để loại bỏ dấu
            var normalizedString = noSpaceNoAccent.Normalize(NormalizationForm.FormKD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = System.Globalization.CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != System.Globalization.UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString();
        }

        // Hàm import dữ liệu vào bảng
        private void ImportData(SqlConnection connection, string tableName, DataTable dataTable)
        {

            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection))
            {
                bulkCopy.DestinationTableName = tableName;

                foreach (DataColumn col in dataTable.Columns)
                {
                    bulkCopy.ColumnMappings.Add(col.ColumnName, col.ColumnName);
                }

                try
                {
                    bulkCopy.WriteToServer(dataTable);
                }
                catch (Exception ex)
                {
                    // Xử lý lỗi khi ghi dữ liệu vào cơ sở dữ liệu
                    Console.WriteLine($"Error during data import: {ex.Message}");
                }
            } // Kết nối sẽ tự động đóng tại đây, ngay cả khi có lỗi.


        }

        // Hàm chuyển đổi kiểu dữ liệu .NET sang kiểu dữ liệu SQL
        private string GetSqlDbType(Type dataType)
        {
            if (dataType == typeof(int) || dataType == typeof(double) || dataType == typeof(float))
            {
                return "INT";

            }
            else if (dataType == typeof(string))
            {
                return "NVARCHAR(300)";

            }
            else if (dataType == typeof(object))
            {
                return "NVARCHAR(300)"; // hoặc kiểu dữ liệu mặc định khác

            }
            else
                throw new NotSupportedException($"Unsupported data type: {dataType}");
        }

    }
}
