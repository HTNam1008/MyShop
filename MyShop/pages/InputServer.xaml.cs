﻿using System;
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
    /// Interaction logic for InputServer.xaml
    /// </summary>
    public partial class InputServer : Window
    {
        public InputServer()
        {
            InitializeComponent();
        }

        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void textServer_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtServer.Focus();
        }

        private void txtServer_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtServer.Text) && txtServer.Text.Length > 0)
                textServer.Visibility = Visibility.Collapsed;
            else
                textServer.Visibility = Visibility.Visible;
        }

        private void connectServerButton_Click(object sender, RoutedEventArgs e)
        {
            if(!string.IsNullOrEmpty(txtServer.Text) && txtServer.Text.Length > 0)
            {
                //Server server = new Server(txtServer.Text);
                Server.Instance.Name= txtServer.Text;
                MessageBox.Show($"Successfully connected to server {Server.Instance.Name}!", "Success!", MessageBoxButton.OK, MessageBoxImage.Information);
                Window window = new InputDatabase();
                window.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Please enter server name!", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        private void borderServer_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var sourceImage = new SourceImage()
            {
                closeImage = "res/asset/close.png",
                dbImage = "res/asset/server.png",
            };
            this.DataContext = sourceImage;
        }
    }
}
