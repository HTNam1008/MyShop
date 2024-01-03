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
using System.Windows.Shapes;

namespace MyShop.pages
{
    /// <summary>
    /// Interaction logic for AddWindow.xaml
    /// </summary>
    public partial class AddWindow : Window
    {
        private int ID;
        public string Name { get; set; }
        public string OS { get; set; }
        public string Manufacturer { get; set; }
        public string MemoryStorage { get; set; }
        public int Price { get; set; }
        public string Image { get; set; }

        public AddWindow(int Id)
        {
            InitializeComponent();
            ID = Id;
        }


        private void newButton_Click(object sender, RoutedEventArgs e)
        {

            Name = NamePhone.Text;
            OS = OSPhone.Text;
            Manufacturer = ManufacturerPhone.Text;
            MemoryStorage = MemoryPhone.Text;
            Price = Convert.ToInt32(PricePhone.Text);
            Image = ImagePhone.Text;


            string sql = """           
                insert into ImportExcel(ID,Name, OS, Manufacturer, Price, MemoryStorage,Image )
                values (@ID,@Name, @OS, @Manufacturer, @Price,@MemoryStorage,@Image)
                select ident_current('ImportExcel')
                """;
            if (Database.Instance.Connection.State == System.Data.ConnectionState.Closed)
            {
                Database.Instance.Connection.Open();
            }
            var command = new SqlCommand(sql, Database.Instance.Connection);
            command.Parameters.Add("@ID", System.Data.SqlDbType.Int)
                .Value = ID;
            command.Parameters.Add("@OS", System.Data.SqlDbType.Text)
                .Value = OS;
            command.Parameters.Add("@Manufacturer", System.Data.SqlDbType.Text)
                .Value = Manufacturer;
            command.Parameters.Add("@Name", System.Data.SqlDbType.Text)
                .Value = Name;
            command.Parameters.Add("@Price", System.Data.SqlDbType.Int)
                .Value = Price;
            command.Parameters.Add("@MemoryStorage", System.Data.SqlDbType.Text)
                .Value = MemoryStorage;
            command.Parameters.Add("@Image", System.Data.SqlDbType.Text)
                .Value = Image;
            int count = command.ExecuteNonQuery();

            if (count > 0)
            {
                MessageBox.Show($"Insert successfully new phone: {Name}");
                DialogResult = true;

            }
            else
            {
                MessageBox.Show("Insert failed");
            }

        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
           this.Close();
        }
    }
}
