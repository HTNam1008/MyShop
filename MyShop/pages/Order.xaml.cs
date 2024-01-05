using Microsoft.Data.SqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Printing;
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

namespace MyShop.pages
{
    /// <summary>
    /// Interaction logic for Order.xaml
    /// </summary>
    public partial class Order : Page
    {
        BindingList<CustomerOrder> _OrderList = new BindingList<CustomerOrder>();
        BindingList<OrderDetail> _OrderDetail = new BindingList<OrderDetail>();
        int _orderPerPage = 5;
        int _totalOrders = -1;
        int _totalOrderItems = -1;
        int _currentOrderPage = 1;
        int _totalOrderCount = -1;
        int _orderDetailPerPage = 5;
        int _totalDetailOrders = -1;
        int _totalDetailOrderItems = -1;
        int _currentDetailOrderPage = 1;
        int _totalDetailOrderCount = -1;
        int _selectedOrderIndex = -1;
        SqlDataReader _dataReader;
        public Order()
        {
            InitializeComponent();
        }


        private void getDataFromDatabase()
        {
            _dataReader?.Close();
            string sql = @"select *, count(*) over() as Total from CUSTOMERORDER" + @" Order by OrderID offset @Skip rows fetch next @Take rows only";

            _OrderList.Clear();
            

            SqlCommand command = new SqlCommand(sql, Database.Instance.Connection);
            command.Parameters.Add("@Skip", SqlDbType.Int).Value = (_currentOrderPage - 1) * _orderPerPage;
            command.Parameters.Add("@Take", SqlDbType.Int).Value = _orderPerPage;
            
            try
            {
                _dataReader = command.ExecuteReader();

                while (_dataReader.Read())
                {
                    CustomerOrder order = readOrderFromDatabase(_dataReader);
                    _OrderList.Add(order);

                    _totalOrderCount = (int)_dataReader["Total"];
                }

                _dataReader.Close();

                if (_totalOrderCount != _totalOrderItems)
                {
                    _totalOrderItems = _totalOrderCount;
                    _totalOrders = (_totalOrderItems / _orderPerPage) +
                        (((_totalOrderItems % _orderPerPage) == 0) ? 0 : 1);
                }

                CurOrderPage.Text = _currentOrderPage.ToString();
                TotalOrderPage.Text = _totalOrders.ToString();

                if (_totalOrders == -1 || _totalOrders == 0)
                {
                    CurOrderPage.Text = "1";
                    TotalOrderPage.Text = "1";
                }

                OrderList.ItemsSource = _OrderList;
            } catch
            {
                MessageBox.Show("Failed to connect to database!", "Error", MessageBoxButton.OK);
            }
            

        }

        private CustomerOrder readOrderFromDatabase(SqlDataReader dataReader)
        {
            CustomerOrder order = new CustomerOrder()
            {
                OrderId = (int)dataReader["OrderID"],
                PhoneNum = (string)dataReader["PhoneNum"],
                Status = (string)dataReader["Status"],
                CreateDate = (DateTime)dataReader["CreateDate"],
                ShipDate = (DateTime)dataReader["ShipmentDate"],
                TotalCost = (Int32)dataReader["totalcost"]
            };

            order.StatusImgPath = "res\\icon\\" + order.Status + ".png";

            return order;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            getDataFromDatabase();
        }

        private CustomerClass GetCustomerClass(string phoneNum)
        {
            try
            {
                string sql = $"select * from CUSTOMERClass where phoneNum = '{phoneNum}'";
                SqlCommand command = new SqlCommand(sql, Database.Instance.Connection);
                _dataReader = command.ExecuteReader();

                if (!_dataReader.Read())
                {
                    MessageBox.Show("Cannot find customer");
                    throw (new Exception("Cannot find customer"));
                }    

                CustomerClass customerClass = new CustomerClass()
                {
                    PhoneNum = (string)_dataReader["phoneNum"],
                    Name = (string)_dataReader["Name"], 
                    Address = (string)_dataReader["Address"],
                    Email = (string)_dataReader["Email"]
                };

                _dataReader.Close();

                return customerClass;
            } catch (Exception e)
            {
                _dataReader.Close();

                MessageBox.Show(e.ToString());
            }

            return new CustomerClass();
        }

        private void getOrderDetail(int id)
        {
            string sql = $"select *, count(*) over() as TotalCount  from ORDERDETAIL where OrderId = {id} Order by phonename offset @Skip rows fetch next @Take rows only";
            
            SqlCommand command = new SqlCommand(sql, Database.Instance.Connection);
            command.Parameters.Add("@Skip", SqlDbType.Int).Value = (_currentDetailOrderPage - 1) * _orderDetailPerPage;
            command.Parameters.Add("@Take", SqlDbType.Int).Value = _orderDetailPerPage;
            try
            {
                _dataReader = command.ExecuteReader();


                _OrderDetail.Clear();

                while (_dataReader.Read())
                {
                    OrderDetail phone = new OrderDetail()
                    {
                        phone = (string)_dataReader["PhoneName"],
                        image = (string)_dataReader["image"],
                        total = (Int32)_dataReader["total"],
                        amount = (Int32)_dataReader["amount"]
                    };
                    _totalDetailOrderCount = (int)_dataReader["TotalCount"];
                    _OrderDetail.Add(phone);
                }
                if (_totalDetailOrderCount != _totalDetailOrderItems)
                {
                    _totalDetailOrderItems = _totalDetailOrderCount;
                    _totalDetailOrders = (_totalDetailOrderItems / _orderDetailPerPage) +
                        (((_totalDetailOrderItems % _orderDetailPerPage) == 0) ? 0 : 1);
                }

                CurOrderDetailPage.Text = _currentDetailOrderPage.ToString();
                TotalOrderDetailPage.Text = _totalDetailOrders.ToString();

                if (_totalDetailOrders == -1 || _totalDetailOrders == 0 || _OrderDetail.Count == 0)
                {
                    CurOrderDetailPage.Text = "1";
                    TotalOrderDetailPage.Text = "1";
                }

                _dataReader.Close();
            } catch (Exception e) {
                MessageBox.Show(e.ToString());
                _dataReader.Close();
            }

            OrderDetailList.ItemsSource = _OrderDetail;
        }

        private void OrderList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!_dataReader.IsClosed)
            {
                _dataReader.Close();
            }    

            int selectedIndex = OrderList.SelectedIndex;

           

            if (selectedIndex < 0) {
                _OrderDetail.Clear();

                OrderDetailList.ItemsSource = _OrderDetail;

                IdField.Text = "";

                Customer_name.Text = "";
                Email.Text = "";
                Address.Text = "";
                Customer_PhoneNum.Text = "";
                CreateDate.Text = "";
                ShipDate.Text = "";
                TotalCost.Text = "";

                CurOrderDetailPage.Text = "1";
                TotalOrderDetailPage.Text = "1";
                return;
            }

            OrderSelected.Text = _OrderList[selectedIndex].OrderId.ToString();
            _currentDetailOrderPage = 1;
            getOrderDetail(_OrderList[selectedIndex].OrderId);

            CustomerClass customer = GetCustomerClass(_OrderList[selectedIndex].PhoneNum);

            IdField.Text = _OrderList[selectedIndex].OrderId.ToString();

            Customer_name.Text = customer.Name;
            Email.Text = customer.Email;
            Address.Text = customer.Address;
            Customer_PhoneNum.Text = customer.PhoneNum;
            CreateDate.Text = _OrderList[selectedIndex].CreateDate.ToString();
            ShipDate.Text = _OrderList[selectedIndex].ShipDate.ToString();
            TotalCost.Text = _OrderList[selectedIndex].TotalCost.ToString();
        }

        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TxtSearch.Text == "Search" || TxtSearch.Text == "" || TxtSearch.Text.Contains(" "))
            {
                return;
            }

        }

        private void TxtSearch_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (TxtSearch.Text == "Search")
            {
                TxtSearch.Text = string.Empty;

                return;
            }    

            if (TxtSearch.Text.Length == 0)
            {
                TxtSearch.Text = "Search";
            }    
        }

        private void SearchBtn_Click(object sender, RoutedEventArgs e)
        {
            if (TxtSearch.Text == "Search" || TxtSearch.Text == "")
            {
                getDataFromDatabase();

                return;
            }

            try
            {
                int id = int.Parse(TxtSearch.Text);

                string sql = @"select *, count(*) over() as Total from CUSTOMERORDER where OrderId = @id" + @" Order by OrderID offset @Skip rows fetch next @Take rows only";

                _OrderList.Clear();

                _currentOrderPage = 1;

                SqlCommand command = new SqlCommand(sql, Database.Instance.Connection);
                command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                command.Parameters.Add("@Skip", SqlDbType.Int).Value = (_currentOrderPage - 1) * _orderPerPage;
                command.Parameters.Add("@Take", SqlDbType.Int).Value = _orderPerPage;
                _dataReader = command.ExecuteReader();

                while (_dataReader.Read())
                {
                    CustomerOrder order = readOrderFromDatabase(_dataReader);
                    _OrderList.Add(order);

                    _totalOrderCount = (int)_dataReader["Total"];
                }

                _dataReader.Close();

                if (_totalOrderCount != _totalOrderItems)
                {
                    _totalOrderItems = _totalOrderCount;
                    _totalOrders = (_totalOrderItems / _orderPerPage) +
                        (((_totalOrderItems % _orderPerPage) == 0) ? 0 : 1);
                }

                CurOrderPage.Text = _currentOrderPage.ToString();
                TotalOrderPage.Text = _totalOrders.ToString();

                OrderList.ItemsSource = _OrderList;
            } catch (Exception ex)
            {
                _dataReader.Close();
                MessageBox.Show($"Search value: {TxtSearch.Text} was invalid!");
            }
        }

        private void PreviousOrderPage_Click(object sender, RoutedEventArgs e)
        {
            if (_currentOrderPage == 1)
            {
                return;
            }

            _currentOrderPage--;
            getDataFromDatabase();
        }

        private void NextOrderPage_Click(object sender, RoutedEventArgs e)
        {
            if (_currentOrderPage == _totalOrders)
            {
                return;
            }

            _currentOrderPage++;
            getDataFromDatabase();
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            Window window = new NewOrder();
            window.Show();
        }

        private void AddBtn_MouseEnter(object sender, MouseEventArgs e)
        {
            AddBtn.FontSize = 25;
        }

        private void AddBtn_MouseLeave(object sender, MouseEventArgs e)
        {
            AddBtn.FontSize = 20;
        }

        private void PreviousOrderDetailPage_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int index = int.Parse(IdField.Text);

                if (_currentDetailOrderPage == 1)
                {
                    return;
                }

                _currentDetailOrderPage--;
                getOrderDetail(index);
            }
            catch
            {
                _OrderDetail.Clear();

                OrderDetailList.ItemsSource = _OrderDetail;

                IdField.Text = "";

                Customer_name.Text = "";
                Email.Text = "";
                Address.Text = "";
                Customer_PhoneNum.Text = "";
                CreateDate.Text = "";
                ShipDate.Text = "";
                TotalCost.Text = "";

                CurOrderDetailPage.Text = "1";
                TotalOrderDetailPage.Text = "1";
            }
        }

        private void NextOrderDetailPage_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int index = int.Parse(IdField.Text);

                if (_currentDetailOrderPage == _totalDetailOrders)
                {
                    return;
                }

                _currentDetailOrderPage++;
                getOrderDetail(index);
            } catch
            {
                _OrderDetail.Clear();

                OrderDetailList.ItemsSource = _OrderDetail;

                IdField.Text = "";

                Customer_name.Text = "";
                Email.Text = "";
                Address.Text = "";
                Customer_PhoneNum.Text = "";
                CreateDate.Text = "";
                ShipDate.Text = "";
                TotalCost.Text = "";

                CurOrderDetailPage.Text = "1";
                TotalOrderDetailPage.Text = "1";
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result;
            try
            {
                int index = int.Parse(IdField.Text);

                result = MessageBox.Show("Are you sure you want to delete this?", "Delete Order", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.No)
                {
                    return;
                }

                string sql = $"delete from OrderDetail where OrderId = {index}";

                SqlCommand command = new SqlCommand(sql, Database.Instance.Connection);

                int recordChanged = command.ExecuteNonQuery();

                if (recordChanged < 1)
                {
                    MessageBox.Show("Failed to delete!");

                    return;
                }

                sql = $"delete from CustomerOrder where OrderId = {index}";

                command = new SqlCommand(sql, Database.Instance.Connection);

                recordChanged = command.ExecuteNonQuery();

                if (recordChanged < 1)
                {
                    MessageBox.Show("Failed to delete!");

                    return;
                }

                _currentOrderPage = 1;
                _currentDetailOrderPage = 1;
                getDataFromDatabase();
            }
            catch
            {
                _OrderDetail.Clear();

                OrderDetailList.ItemsSource = _OrderDetail;

                IdField.Text = "";

                Customer_name.Text = "";
                Email.Text = "";
                Address.Text = "";
                Customer_PhoneNum.Text = "";
                CreateDate.Text = "";
                ShipDate.Text = "";
                TotalCost.Text = "";

                CurOrderDetailPage.Text = "1";
                TotalOrderDetailPage.Text = "1";
            }
        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Window window = new EditOrder(int.Parse(IdField.Text));
                window.ShowDialog();

                _currentOrderPage = 1;
                getDataFromDatabase();
            }
            catch
            {
                return;
            } 
            
        }
    }
}
