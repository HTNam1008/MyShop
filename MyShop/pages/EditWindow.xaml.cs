﻿using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
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
    /// Interaction logic for EditWindow.xaml
    /// </summary>
    public partial class EditWindow : Window
    {
        public Phone editPhone { get; set; }
        public EditWindow(Phone EditPhone)
        {
            InitializeComponent();
            editPhone = EditPhone;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = editPhone;
        }
        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            editPhone.name = NamePhone.Text;
            editPhone.os = OSPhone.Text;
            editPhone.manufacturer = ManufacturerPhone.Text;
            editPhone.memoryStorage = MemoryPhone.Text;
            editPhone.price =Convert.ToInt32(PricePhone.Text);
            editPhone.image= ImagePhone.Text;
            string sql = """           
                update ImportExcel
                set Name=@name, OS=@os, Manufacturer=@manufacturer, Price=@price,MemoryStorage=@memoryStorage, Image=@image
                where ID=@ID
                """;
            if (Database.Instance.Connection.State == System.Data.ConnectionState.Closed)
            {
                Database.Instance.Connection.Open();
            }
            var command = new SqlCommand(sql, Database.Instance.Connection);   
            command.Parameters.Add("@name", System.Data.SqlDbType.Text)
                .Value = editPhone.name;
            command.Parameters.Add("@os", System.Data.SqlDbType.Text)
                .Value = editPhone.os;
            command.Parameters.Add("@price", System.Data.SqlDbType.Int)
                .Value = editPhone.price;
            command.Parameters.Add("@manufacturer", System.Data.SqlDbType.Text)
                .Value = editPhone.manufacturer;
            command.Parameters.Add("@memoryStorage", System.Data.SqlDbType.Text)
                .Value = editPhone.memoryStorage;
            command.Parameters.Add("@image", System.Data.SqlDbType.Text)
                .Value = editPhone.image;
            command.Parameters.Add("@ID", System.Data.SqlDbType.Int)
                .Value = editPhone.id;

            int count = command.ExecuteNonQuery();

            if (count > 0)
            {
                MessageBox.Show($"Edit successfully phone: {editPhone.name}");
                DialogResult = true;
            }
            else
            {
                MessageBox.Show("Edit failed");
            }
        }

    }
}