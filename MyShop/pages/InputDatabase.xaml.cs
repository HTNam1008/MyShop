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
using System.Windows.Shapes;
using Microsoft.Data.SqlClient;

namespace MyShop.pages
{
    /// <summary>
    /// Interaction logic for InputDatabase.xaml
    /// </summary>
    public partial class InputDatabase : Window
    {
        Server server = Server.Instance;
        public InputDatabase()
        {
            InitializeComponent();
        }

        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private async void connectDatabaseButton_Click(object sender, RoutedEventArgs e)
        {
            string database = txtDatabase.Text;

            loadingBar.Visibility = Visibility.Visible;
            loadingBar.IsIndeterminate = true;
            
            var builder = new SqlConnectionStringBuilder();
            builder.DataSource = server.Name;
            builder.InitialCatalog = database;
            builder.IntegratedSecurity = true;
            builder.TrustServerCertificate = true;

            string connectionString = builder.ConnectionString;

            // DESKTOP-MQMBQC9
            var connection = new SqlConnection(connectionString);

            connection = await Task.Run(() =>
            {
                var _connection = new SqlConnection(connectionString);
                try
                {
                    _connection.Open();
                }
                catch (SqlException ex)
                {
                    if(ex.Number == 4060)
                    {
                        MessageBoxResult result = MessageBox.Show($"Database {database} is not exist. " +
                            "Do you want to create new database?", "!!!", MessageBoxButton.YesNo, MessageBoxImage.Question);
                        if(result == MessageBoxResult.Yes)
                        {
                            try
                            {
                                // Create database
                                /*string createDatabase = "CREATE DATABASE " + database;
                                SqlCommand command = new SqlCommand(createDatabase, _connection);
                                command.ExecuteNonQuery();

                                MessageBox.Show($"Database {database} has been created successfully.", "!!!", MessageBoxButton.OK, MessageBoxImage.Information);

                                // Mở kết nối mới đến cơ sở dữ liệu đã tạo mới
                                _connection.Close();
                                _connection.ConnectionString = builder.ConnectionString;
                                _connection.Open();*/

                                string connectionStr = "Data Source=.;Integrated Security=True;TrustServerCertificate = true;";
                                SqlConnection connection1 = new SqlConnection(connectionStr);
                                SqlCommand cmd;
                                string sql = $"CREATE DATABASE {database}";
                                connection1.Open();
                                cmd = connection1.CreateCommand();
                                cmd.CommandText = sql;
                                cmd.ExecuteNonQuery();
                                connection1.Close();

                                _connection = connection1;

                                MessageBox.Show($"Database {database} has been created successfully.", "!!!", MessageBoxButton.OK, MessageBoxImage.Information);
                            } catch(SqlException ex2)
                            {
                                _connection = null;
                                MessageBox.Show($"Cannot create database:\n{ex.Message}", "Fail!", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                        else
                        {
                            _connection = null;
                        }

                    }
                    else
                    {
                        _connection = null;
                        MessageBox.Show($"Cannot create database:\n{ex.Message}", "Fail!", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                return _connection;
            });

            loadingBar.Visibility = Visibility.Collapsed;
            loadingBar.IsIndeterminate = false;

            if(connection != null)
            {
                MessageBox.Show($"Connected to database {database} successfully.", "!!!", MessageBoxButton.OK, MessageBoxImage.Information);
                Window window = new MainWindow();
                window.Show();
                this.Close();
            }
            else
            {
                Window window = new InputDatabase();
                window.Show();
                this.Close();
            }
        }

        private void textDatabase_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtDatabase.Focus();
        }

        private void txtDatabase_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtDatabase.Text) && txtDatabase.Text.Length > 0)
                textDatabase.Visibility = Visibility.Collapsed;
            else
                textDatabase.Visibility = Visibility.Visible;
        }

        private void borderDatabase_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var sourceImage = new SourceImage()
            {
                closeImage = "res/asset/close.png",
                dbImage = "res/asset/database.png",
            };
            this.DataContext = sourceImage;
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            Window window = new SignIn();
            window.Show();
            this.Close();
        }
    }
}
