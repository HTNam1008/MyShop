﻿using Microsoft.Data.SqlClient;
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

namespace MyShop.pages
{
    /// <summary>
    /// Interaction logic for EditCustomer.xaml
    /// </summary>
    public partial class NewCustomer : Window
    {
        SqlDataReader _dataReader;
        public NewCustomer()
        {
            InitializeComponent();
        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string sql;
                sql = $"select * from CUSTOMERCLASS where phonenum = '{TxtPhone.Text}'";

                using (SqlCommand command = new SqlCommand(sql, Database.Instance.Connection))
                {
                    _dataReader = command.ExecuteReader();

                    if (_dataReader.Read())
                    {
                        MessageBox.Show("Failed to create customer information: \nBecause the phone number is already registered", "Error", MessageBoxButton.OK);

                        _dataReader.Close();
                        return;
                    }

                    _dataReader.Close();
                }


                sql = $"insert into CUSTOMERCLASS(phonenum, name, address, gender, email) values ('{TxtPhone.Text}','{TxtName.Text}', '{TxtAddress.Text}','{TxtGender.Text}','{TxtEmail.Text}')";

                using (SqlCommand command = new SqlCommand(sql, Database.Instance.Connection))
                {
                    command.ExecuteNonQuery();
                }

                MessageBox.Show("Update successfully!", "Edit customer", MessageBoxButton.OK);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to add customer information: \n" + ex.ToString(), "Error", MessageBoxButton.OK);
            }
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
