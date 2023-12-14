using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
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

namespace MyShop.pages
{
    /// <summary>
    /// Interaction logic for SignIn.xaml
    /// </summary>
    public partial class SignIn : Window
    {
        public SignIn()
        {
            InitializeComponent();
        }

        // sign in button
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            /*if (!string.IsNullOrEmpty(txtEmail.Text) && !string.IsNullOrEmpty(passwordBox.Password))
            {
                MessageBox.Show("Successfully signed In");
            }*/
            string username = txtUsername.Text;
            string password = passwordBox.Password;

            // Data Source=DESKTOP-MQMBQC9;Initial Catalog=CustomerDB;Integrated Security=True
            var builder = new SqlConnectionStringBuilder();
            builder.DataSource = "DESKTOP-MQMBQC9";
            builder.InitialCatalog = "MyShopDB";
            builder.IntegratedSecurity = true;
            builder.UserID = username;
            builder.Password = password;
            builder.TrustServerCertificate = true;


            string connectionString = builder.ConnectionString;

            var connection = new SqlConnection(connectionString);

            connection = await Task.Run(() =>
            {
                var _connection = new SqlConnection(connectionString);
                try
                {
                    _connection.Open();
                }
                catch (Exception ex)
                {
                    _connection = null;
                    MessageBox.Show(ex.Message);
                }
                return _connection;
            });

            if (connection != null && !string.IsNullOrEmpty(txtUsername.Text) && !string.IsNullOrEmpty(passwordBox.Password))
            {
                /*MessageBox.Show("Successfully connected to SQL Server");*/
                if (rememberMe.IsChecked == true)
                {
                    var passwordInBytes = Encoding.UTF8.GetBytes(password);
                    var entropy = new byte[20];
                    using (var rng = new RNGCryptoServiceProvider())
                    {
                        rng.GetBytes(entropy);
                    }
                    var cypherText = ProtectedData.Protect(passwordInBytes, entropy,
                            DataProtectionScope.CurrentUser);
                    var passwordIn64 = Convert.ToBase64String(cypherText);
                    var entropyIn64 = Convert.ToBase64String(entropy);


                    var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                    config.AppSettings.Settings["Username"].Value = username;
                    config.AppSettings.Settings["Password"].Value = passwordIn64;
                    config.AppSettings.Settings["Entropy"].Value = entropyIn64;
                    config.Save(ConfigurationSaveMode.Minimal);

                    ConfigurationManager.RefreshSection("appSettings");
                }

                Database.Instance.ConnectionString = connectionString;

                var sql = "SELECT * FROM CUSTOMER WHERE Email = @username";
                var command = new SqlCommand(sql, Database.Instance.Connection);
                command.Parameters.Add("@Username", System.Data.SqlDbType.Char)
                    .Value = username;
                

                var reader = command.ExecuteReader();

                if(reader.Read())
                {
                    /*MessageBox.Show("Successfully signed in");*/
                    string passwordResult = (string)reader["Password"];

                    // unhash password
                    string[] passwordResultSplit = passwordResult.Split("@@@@");
                    string passwordIn64 = passwordResultSplit[0];
                    string entropyIn64 = passwordResultSplit[1];

                    var cyperTextInBytes = Convert.FromBase64String(passwordIn64);
                    var entropyInBytes = Convert.FromBase64String(entropyIn64);

                    var passwordInBytes = ProtectedData.Unprotect(cyperTextInBytes, entropyInBytes,
                                               DataProtectionScope.CurrentUser);
                    var passwordResultUnhash = Encoding.UTF8.GetString(passwordInBytes);

                    if (passwordResultUnhash == password)
                    {
                        /*MessageBox.Show("Successfully signed in");*/
                        Window window = new InputDatabase();
                        window.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Wrong password", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        Window window = new SignIn();
                        window.Show();
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("User is not exist in database! Please sign up!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    Window window = new SignUp();
                    window.Show();
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Invalid username or password!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var sourceImage = new SourceImage()
            {
                closeImage = "res/asset/close.png",
                lockImage = "res/asset/Lock.png",
                emailImage = "res/asset/Email.png",
                eyeImage = "res/asset/eye.png",
            };
            this.DataContext = sourceImage;
            var passwordIn64 = ConfigurationManager.AppSettings["Password"];

            if (passwordIn64.Length != 0)
            {
                var entropyIn64 = ConfigurationManager.AppSettings["Entropy"];

                var cyperTextInBytes = Convert.FromBase64String(passwordIn64);
                var entropyInBytes = Convert.FromBase64String(entropyIn64);

                var passwordInBytes = ProtectedData.Unprotect(cyperTextInBytes, entropyInBytes,
                    DataProtectionScope.CurrentUser);
                var password = Encoding.UTF8.GetString(passwordInBytes);
                passwordBox.Password = password;

                txtUsername.Text = ConfigurationManager.AppSettings["Username"];
            }
        }

        private void Border_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(passwordBox.Password) && passwordBox.Password.Length > 0)
                textPassword.Visibility = Visibility.Collapsed;
            else
                textPassword.Visibility = Visibility.Visible;
        }

        private void textPassword_MouseDown(object sender, MouseButtonEventArgs e)
        {
            passwordBox.Focus();
        }

        private void txtUsername_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtUsername.Text) && txtUsername.Text.Length > 0)
                textUsername.Visibility = Visibility.Collapsed;
            else
                textUsername.Visibility = Visibility.Visible;
        }

        private void textUsername_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtUsername.Focus();
        }

        private void signUpButton_Click(object sender, RoutedEventArgs e)
        {
            Window window = new SignUp();
            window.Show();
            this.Close();
        }

        private void toggleShowPassword_Click(object sender, RoutedEventArgs e)
        {
            if (toggleShowPassword.IsChecked == true)
            {
                txtVisiblePassword.Text = passwordBox.Password;
                txtVisiblePassword.Visibility = Visibility.Visible;
                passwordBox.Visibility = Visibility.Collapsed;
            }
            else
            {
                txtVisiblePassword.Visibility = Visibility.Collapsed;
                passwordBox.Visibility = Visibility.Visible;
                passwordBox.Focus();
            }
        }
    }
}
