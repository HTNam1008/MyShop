using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ExcelDataReader;
using Microsoft.Data.SqlClient;
using Microsoft.Win32;

namespace MyShop
{
    public class Server
    {
        private static Server? _instance = null;
        
        public static Server Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Server();
                }
                return _instance;
            }
        }
        public string Name { get; set; }
/*        public Server(string name)
        {
            Name = name;
        }*/
    }
    public class Database
    {
        private static Database? _instance = null;
        private SqlConnection _connection = null;

        public string ConnectionString { get; set; }
        public SqlConnection? Connection
        {
            get
            {
                if (_connection == null)
                {
                    _connection = new SqlConnection(ConnectionString); ;
                    _connection.Open();
                }
                if (_connection.State != ConnectionState.Open)
                {
                    _connection.Open();
                }
                return _connection;
            }
        }

        public static Database Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Database();
                }
                return _instance;
            }
        }

        public string tableName = "ImportExcel";
        public string Name { get; set; }
        public void ImportDataToSQL()
        {
            try
            {
                // Mở kết nối đến SQL Server
                using (SqlConnection connection = new SqlConnection(this.ConnectionString))
                {
                    connection.Open();

                    if (TableExists(this.tableName) != true)
                    {
                       

                        // Mở hộp thoại chọn file Excel
                        OpenFileDialog openFileDialog = new OpenFileDialog
                        {
                            Filter = "Excel Files|*.xls;*.xlsx"
                        };

                        if (openFileDialog.ShowDialog() == true)
                        {
                            string excelFilePath = openFileDialog.FileName;


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
                                    CreateNewTable(connection, this.tableName, dataTable);

                                    // Import dữ liệu vào cơ sở dữ liệu
                                    ImportData(connection, this.tableName, dataTable);
                                }
                            }
                        }
                        MessageBox.Show("Data imported successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }


        }

        private bool TableExists(string tableName)
        {
            bool result = false;

            try
            {
                using (SqlCommand command = new SqlCommand($"IF OBJECT_ID('{tableName}', 'U') IS NOT NULL SELECT 1 ELSE SELECT 0", Connection))
                {
                    result = (int)command.ExecuteScalar() == 1;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error checking table existence: {ex.Message}");
            }

            return result;
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
