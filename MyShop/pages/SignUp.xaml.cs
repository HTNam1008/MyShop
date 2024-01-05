﻿using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
    /// Interaction logic for SignUp.xaml
    /// </summary>
    public partial class SignUp : Window
    {
        Server server = Server.Instance;
        public string gender = "";
        public SignUp()
        {
            InitializeComponent();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }
        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            Window window = new SignIn();
            window.Show();
            this.Close();
        }

        private async void signUpSubmitButton_Click(object sender, RoutedEventArgs e)
        {
            // connect to SQL Server
            var builder = new SqlConnectionStringBuilder();
            builder.DataSource = server.Name;
            builder.InitialCatalog = Database.Instance.Name;
            builder.IntegratedSecurity = true;
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
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                return _connection;
            });

            if (connection != null)
            {
                /*MessageBox.Show("Successfully connected to SQL Server");*/
                Database.Instance.ConnectionString = connectionString;

                try
                {
                    // Create random ID from 000 to 999
                    // After create ID, check if it exists in database => re-create ID
                    // if not => use it
                    string idString = "";
                    bool isExist = true;
                    while (isExist)
                    {
                        Random random = new Random();
                        int id = random.Next(0, 999);
                        idString = id.ToString();
                        if (id < 10)
                        {
                            idString = "00" + idString;
                        }
                        else if (id < 100)
                        {
                            idString = "0" + idString;
                        }

                        string sqlCheck = @"select * from Admin where ID = @ID";
                        var commandCheck = new SqlCommand(sqlCheck, Database.Instance.Connection);
                        commandCheck.Parameters.Add("@ID", System.Data.SqlDbType.Char)
                            .Value = idString;
                        var reader = commandCheck.ExecuteReader();
                        if (!reader.HasRows)
                        {
                            isExist = false;
                        }
                        reader.Close();
                    }

                    var newCustomer = new Customer()
                    {
                        ID = idString,
                        FirstName = firstNameTextBox.Text,
                        LastName = lastNameTextBox.Text,
                        Email = emailTextBox.Text,
                        Address = addressTextBox.Text,
                        Phone = phoneTextBox.Text,
                        Gender = gender,
                        Age = int.Parse(ageTextBox.Text),
                        Password = passwordTextBox.Text,
                    };
                    string firstName = newCustomer.FirstName;
                    string lastName = newCustomer.LastName;
                    string email = newCustomer.Email;
                    string address = newCustomer.Address;
                    string phone = newCustomer.Phone;
                    int age = newCustomer.Age;
                    string genderCustomer = newCustomer.Gender;
                    string password = newCustomer.Password;

                    string passwordHash = Encryption.Encrypt(newCustomer.Password, "1234567890123456");

                    // add to database
                    string sql = @"insert into Admin(id, firstName, lastName, gender, email, address, phone, age, password) 
                                values(@ID, @FirstName, @LastName, @Gender, @Email,
                                @Address, @Phone, @Age, @Password)";
                    var command = new SqlCommand(sql, Database.Instance.Connection);
                    command.Parameters.Add("@ID", System.Data.SqlDbType.Char)
                        .Value = idString;
                    command.Parameters.Add("@FirstName", System.Data.SqlDbType.Text)
                        .Value = firstName;
                    command.Parameters.Add("@LastName", System.Data.SqlDbType.Text)
                        .Value = lastName;
                    command.Parameters.Add("@Gender", System.Data.SqlDbType.Text)
                        .Value = genderCustomer;
                    command.Parameters.Add("@Email", System.Data.SqlDbType.Text)
                        .Value = email;
                    command.Parameters.Add("@Address", System.Data.SqlDbType.Text)
                        .Value = address;
                    command.Parameters.Add("@Phone", System.Data.SqlDbType.Text)
                        .Value = phoneTextBox.Text;
                    command.Parameters.Add("@Age", System.Data.SqlDbType.Int)
                        .Value = age;
                    command.Parameters.Add("@Password", System.Data.SqlDbType.Text)
                        .Value = passwordHash;

                    int rows = command.ExecuteNonQuery();
                    if (rows > 0)
                    {
                        MessageBox.Show("Successfully signed up!", "Success!", MessageBoxButton.OK, MessageBoxImage.Information);
                        Window newWindow = new SignIn();
                        newWindow.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Failed to sign up!", "Fail!", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                }
                catch (SqlException ex)
                {
                    MessageBox.Show($"{ex.Message}\nFailed to sign up!", "Fail", MessageBoxButton.OK, MessageBoxImage.Error);
                    Window window1 = new SignUp();
                    window1.Show();
                }
                Window window = new SignIn();
                window.Show();
                this.Close();
            }
        }

        private void genderButton_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender == maleButton)
            {
                MessageBox.Show("You choose Male", "!!!", MessageBoxButton.OK, MessageBoxImage.Information);
                gender = "Male";
            }
            else if (sender == femaleButton)
            {
                MessageBox.Show("You choose Female", "!!!", MessageBoxButton.OK, MessageBoxImage.Information);
                gender = "Female";
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var sourceImage = new SourceImage()
            {
                closeImage = "res/asset/close.png",
                personImage = "res/asset/img.png"
            };
            this.DataContext = sourceImage;
        }
    }
}
