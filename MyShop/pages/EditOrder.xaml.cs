﻿using Microsoft.Data.SqlClient;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace MyShop.pages
{
    /// <summary>
    /// Interaction logic for NewOrder.xaml
    /// </summary>
    public partial class EditOrder : Window
    {
        BindingList<Phone> _product = new BindingList<Phone>();
        SqlDataReader _dataReader;
        int _orderId;
        int _productPerPage = 5;
        int _totalProduct = -1;
        int _totalProductItems = -1;
        int _currentProductPage = 1;
        int _totalProductCount = -1;
        int _selectedProduct = -1;
        int _selectedEdit = -1;
        Int32 _totalCost = 0;
        BindingList<OrderDetail> _od = new BindingList<OrderDetail>();

        List<OrderDetail> _phones = new List<OrderDetail>();
        public EditOrder()
        {
            InitializeComponent();
        }

        public EditOrder(int orderId)
        {
            InitializeComponent();

            _orderId = orderId;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            getDataFromDatabase();

            ShipDate.SelectedDate = DateTime.Now.AddDays(3);
            ShipDate.DisplayDateEnd = DateTime.Now.AddYears(2);

            setOrder();
        }

        private void setOrder()
        {
            try
            {
                string sql = $"select * from CustomerOrder where OrderId = {_orderId}";

                _dataReader = new SqlCommand(sql, Database.Instance.Connection).ExecuteReader();

                if (!_dataReader.Read())
                {
                    MessageBox.Show("Cannot get data from database! Please retry!");

                    return;
                }

                CustomerOrder order = readOrderFromDatabase(_dataReader);

                _dataReader.Close();

                CustomerClass customer = GetCustomerClass(order.PhoneNum);

                if (customer == null)
                {
                    MessageBox.Show("Cannot get data from database! Please retry!");

                    return;
                }

                _dataReader.Close();

                getOrderDetail(_orderId);

                Customer_name.Text = customer.Name;
                Customer_PhoneNum.Text = customer.PhoneNum;
                Email.Text = customer.Email;
                Address.Text = customer.Address;
                Gender.Text = customer.Gender;
                ShipDate.SelectedDate = order.ShipDate;
                TotalCost.Text = order.TotalCost.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Cannot get data from database! Please retry!");

                return;
            }
        }

        private void getOrderDetail(int id)
        {
            string sql = $"select * from ORDERDETAIL where OrderId = {id}";

            SqlCommand command = new SqlCommand(sql, Database.Instance.Connection);
            try
            {
                _dataReader = command.ExecuteReader();


                _od.Clear();

                while (_dataReader.Read())
                {
                    OrderDetail phoneD = new OrderDetail()
                    {
                        phone = (string)_dataReader["PhoneName"],
                        image = (string)_dataReader["image"],
                        total = (Int32)_dataReader["total"],
                        amount = (Int32)_dataReader["amount"]
                    };

                    _phones.Add(new OrderDetail()
                    {
                        phone = phoneD.phone,
                        image = phoneD.image,
                        amount = 0,
                        total = phoneD.total,
                    });

                    _od.Add(phoneD);
                }

                _dataReader.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                _dataReader.Close();
            }

            OrderDetailList.ItemsSource = _od;
        }

        private CustomerClass GetCustomerClass(string phoneNum)
        {
            try
            {
                string sql = $"select * from CUSTOMERCLASS where phoneNum = '{phoneNum}'";
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
                    Email = (string)_dataReader["Email"],
                    Gender = (string)_dataReader["Gender"]
                };

                _dataReader.Close();

                return customerClass;
            }
            catch (Exception e)
            {
                _dataReader.Close();

                MessageBox.Show(e.ToString());
            }

            return null;
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
        private void getDataFromDatabase()
        {
            string sql = $"select *, count(*) over() as TotalCount from mobile Order by name offset @Skip rows fetch next @Take rows only";

            SqlCommand command = new SqlCommand(sql, Database.Instance.Connection);
            command.Parameters.Add("@Skip", SqlDbType.Int).Value = (_currentProductPage - 1) * _productPerPage;
            command.Parameters.Add("@Take", SqlDbType.Int).Value = _productPerPage;
            try
            {
                _dataReader = command.ExecuteReader();

                _product.Clear();

                while (_dataReader.Read())
                {
                    Phone phone = new Phone()
                    {
                        name = (string)_dataReader["name"],
                        image = (string)_dataReader["image"],
                        price = (Int32)_dataReader["price"],
                        amount = (Int32)_dataReader["amount"],
                        os = (string)_dataReader["os"],
                        manufacturer = (string)_dataReader["manufacturer"],
                        memoryStorage = (string)_dataReader["memoryStorage"]
                    };
                    _totalProductCount = (int)_dataReader["TotalCount"];
                    _product.Add(phone);
                }
                if (_totalProductCount != _totalProductItems)
                {
                    _totalProductItems = _totalProductCount;
                    _totalProduct = (_totalProductItems / _productPerPage) +
                        (((_totalProductItems % _productPerPage) == 0) ? 0 : 1);
                }

                CurProductPage.Text = _currentProductPage.ToString();
                TotalProductPage.Text = _totalProduct.ToString();

                if (_totalProduct == -1 || _totalProduct == 0 || _product.Count == 0)
                {
                    CurProductPage.Text = "1";
                    TotalProductPage.Text = "1";
                }

                _dataReader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                _dataReader.Close();
            }

            ProductList.ItemsSource = _product;
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (Customer_PhoneNum.Text.Length != 10)
            {
                MessageBox.Show("Invalid phonenum");

                return;
            }

            if (_od.Count == 0)
            {
                MessageBox.Show("Select at least 1 item");

                return;
            }    

            string phonenum = Customer_PhoneNum.Text;

            foreach (var item in phonenum)
            {
                if (item < '0' || item > '9')
                {
                    MessageBox.Show("Invalid phonenum");
                }    
            }    

            try
            {
                int res = 0;
                string sql = $"select count(*) over() as total from CustomerClass where PhoneNum like '{Customer_PhoneNum.Text}'";

                SqlCommand command = new SqlCommand(sql, Database.Instance.Connection);
                _dataReader = command.ExecuteReader();

               
                if (!_dataReader.Read())
                {
                    _dataReader.Close();
                    sql = $"insert into CustomerClass(name, address, phonenum, email, gender)" +
                        $" values ('{Customer_name.Text}', '{Address.Text}', '{Customer_PhoneNum.Text}', '{Email.Text}', '{Gender.Text}')";
                    command = new SqlCommand(sql, Database.Instance.Connection);
                    res = command.ExecuteNonQuery();

                    if (res == 0)
                    {
                        MessageBox.Show("Fail to create order please recheck your information: Customer!");

                        return;
                    }
                }
                else
                {
                    _dataReader.Close();
                    CustomerClass cus = GetCustomerClass(Customer_PhoneNum.Text);

                    do
                    {
                        if (cus.Name == Customer_name.Text && cus.Address == Address.Text && cus.Gender == Gender.Text && cus.Email == Email.Text)
                        {
                            break;
                        }    
                        MessageBoxResult result = MessageBox.Show("This phone number already exists in the database!\n Would you like to update the information?", "Phone number exists", MessageBoxButton.YesNo);

                        if (result == MessageBoxResult.No)
                        {
                            return;
                        }

                        if (result == MessageBoxResult.Yes)
                        {
                            sql = $"delete from CustomerClass where phonenum = '{phonenum}'";

                            command = new SqlCommand(sql, Database.Instance.Connection);
                            res = command.ExecuteNonQuery();

                            if (res == 0)
                            {
                                MessageBox.Show("Fail to create order please recheck your information!");

                                return;
                            }

                            sql = $"insert into CustomerClass(name, address, phonenum, email , gender)" +
                            $" values ('{Customer_name.Text}', '{Address.Text}', '{Customer_PhoneNum.Text}', '{Email.Text}', '{Gender.Text}')";

                            command = new SqlCommand(sql, Database.Instance.Connection);
                            res = command.ExecuteNonQuery();

                            if (res == 0)
                            {
                                MessageBox.Show("Fail to edit order please recheck your information!");

                                return;
                            }

                        }
                    } while (false);
                }

                sql = $"delete from OrderDetail where OrderId = {_orderId}";

                command = new SqlCommand(sql, Database.Instance.Connection);
                res = command.ExecuteNonQuery();
                MessageBox.Show(sql + $" {res} rows affected");
                if (res <= 0)
                {
                    MessageBox.Show("Fail to complete order please recheck your information!");

                    return;
                }

                foreach (var item in _od)
                {
                    sql = $"insert into OrderDetail( OrderId, amount, Phonename, total, image)" +
                        $" values ({_orderId}, {item.amount}, '{item.phone}', {item.total}, '{item.image}')";
                    command = new SqlCommand(sql, Database.Instance.Connection);
                    res = command.ExecuteNonQuery();
                    MessageBox.Show(sql + $" {res} rows affected");
                    if (res <= 0)
                    {
                        MessageBox.Show("Fail to complete order please recheck your information!");

                        return;
                    }
                }

                foreach (var item in _phones)
                {
                    sql = $"update Mobile set amount = amount + {item.amount} where name = '{item.phone}'";
                    command = new SqlCommand(sql, Database.Instance.Connection);
                    res = command.ExecuteNonQuery();
                    MessageBox.Show(sql + $" {res} rows affected");
                    if (res <= 0)
                    {
                        MessageBox.Show("Fail to complete order please recheck your information!");


                    }
                }

                } catch (Exception ex)
            {
                MessageBox.Show("Fail to create order please recheck your information!" + ex.ToString());

                return;
            }

            MessageBox.Show("Successfully created Order!");


            this.Close();
        }

        private void PreviousProductPage_Click(object sender, RoutedEventArgs e)
        {
            if (_currentProductPage == 1)
            {
                return;
            }

            _currentProductPage--;
            getDataFromDatabase();
        }

        private void NextProductPage_Click(object sender, RoutedEventArgs e)
        {
            if (_currentProductPage == _totalProduct || TotalProductPage.Text == "1")
            {
                return;
            }

            _currentProductPage++;
            getDataFromDatabase();
        }
        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedProduct < 0)
            {
                MessageBox.Show("No product choosed");
                return;
            }
            ProductList.SelectedIndex = _selectedProduct;

            try
            {
                int amount = int.Parse(Amount.Text);

                if (amount <= 0)
                {
                    MessageBox.Show("Invalid amount");

                    return;
                }

                if (amount > _product[_selectedProduct].amount)
                {
                    MessageBox.Show("Out of stock");

                    return;
                }

                ProductList.ItemsSource = new List<Product>();
                OrderDetailList.ItemsSource = new List<OrderDetail>();

                foreach (var item in _od)
                {
                    if (item.phone == _product[_selectedProduct].name)
                    {
                        foreach (var i in _phones)
                        {
                            if (i.phone == item.phone)
                            {
                                i.amount = amount;
                            }
                        }
                        _product[_selectedProduct].amount -= amount;
                        item.amount += amount;
                        item.total += amount * _product[_selectedProduct].price;

                        OrderDetailList.ItemsSource = _od;
                        ProductList.ItemsSource = _product;

                        return;
                    }
                }

                _product[_selectedProduct].amount -= amount;

                OrderDetail od = new OrderDetail()
                {
                    phone = _product[_selectedProduct].name,
                    amount = amount,
                    image = _product[_selectedProduct].image,
                    total = amount * _product[_selectedProduct].price
                };

                _phones.Add(new OrderDetail()
                {
                    phone = od.phone,
                    amount = od.amount,
                    image = od.image,
                    total = od.total
                }) ;

                _od.Add(od);

                _totalCost += od.total;

                OrderDetailList.ItemsSource = _od;
                ProductList.ItemsSource = _product;
                TotalCost.Text = _totalCost.ToString();
            } catch
            {
                MessageBox.Show("Invalid amount");
            }
        }

        private void ProductList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ProductList.SelectedIndex < 0)
            {
                return;
            }    

            _selectedProduct = ProductList.SelectedIndex;

            selectedItem.Text = _product[_selectedProduct].name;
            BitmapImage bitmap = new BitmapImage(
                new Uri(_product[_selectedProduct].image, UriKind.Absolute)
            );
            selectedImage.Source = bitmap;
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            _selectedEdit = OrderDetailList.SelectedIndex;
            if (_selectedEdit < 0 || _od.Count == 0 || _selectedEdit >= _od.Count)
            {
                MessageBox.Show("No product choosed");
                return;
            }
            _phones[OrderDetailList.SelectedIndex].amount = _od[OrderDetailList.SelectedIndex].amount;
            _totalCost -= _od[_selectedEdit].total;
            _od.RemoveAt(OrderDetailList.SelectedIndex);
            

            AmountEdit.Text = "";

            BitmapImage bitmap = new BitmapImage();
            selectedImageOrder.Source = bitmap;

            TotalCost.Text = _totalCost.ToString();
        }

        private void OrderDetailList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedEdit = OrderDetailList.SelectedIndex;
            if (_selectedEdit < 0 || _od.Count == 0 || _selectedEdit >= _od.Count)
            {
                return;
            }

            AmountEdit.Text = _od[_selectedEdit].amount.ToString();
            BitmapImage bitmap = new BitmapImage(
                new Uri(_od[_selectedEdit].image, UriKind.Absolute)
            );
            selectedImageOrder.Source = bitmap;
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            _selectedEdit = OrderDetailList.SelectedIndex;
            if (_selectedEdit < 0 || _od.Count == 0 || _selectedEdit >= _od.Count)
            {
                return;
            }

            try
            {
                int amount = int.Parse(AmountEdit.Text);

                if (amount <= 0)
                {
                    MessageBox.Show("Invalid amount");

                    return;
                }
               

                foreach (var item in _product)
                {
                    if (item.name == _od[_selectedEdit].phone)
                    {
                        if (amount != _od[_selectedEdit].amount)
                        {
                            foreach (var i in _phones)
                            {
                                if (i.phone == item.name)
                                {
                                    i.amount = _od[_selectedEdit].amount - amount;

                                    item.amount += i.amount;
                                }
                            }

                            _od[_selectedEdit].amount = amount;
                            _od[_selectedEdit].total = amount * item.price;

                            _totalCost = 0;

                            foreach (var i in _od)
                            {
                                _totalCost += i.total;
                            }    

                            TotalCost.Text = _totalCost.ToString();

                            OrderDetailList.ItemsSource = new BindingList<OrderDetail>();
                            ProductList.ItemsSource = new BindingList<Product>();

                            OrderDetailList.ItemsSource = _od;
                            ProductList.ItemsSource = _product;

                            return;
                        }    
                    }    
                }

                MessageBox.Show("Invalid selection");
            }
            catch
            {
                MessageBox.Show("Invalid amount");
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
                string sql = @"select *, count(*) over() as Total from Mobile where name like @id" + @" Order by name offset @Skip rows fetch next @Take rows only";

                _product.Clear();

                _currentProductPage = 1;

                SqlCommand command = new SqlCommand(sql, Database.Instance.Connection);
                command.Parameters.Add("@id", SqlDbType.Text).Value = TxtSearch.Text;
                command.Parameters.Add("@Skip", SqlDbType.Int).Value = (_currentProductPage - 1) * _productPerPage;
                command.Parameters.Add("@Take", SqlDbType.Int).Value = _productPerPage;
                _dataReader = command.ExecuteReader();

                while (_dataReader.Read())
                {
                    Phone phone = new Phone()
                    {
                        name = (string)_dataReader["name"],
                        image = (string)_dataReader["image"],
                        price = (Int32)_dataReader["price"],
                        amount = (Int32)_dataReader["amount"],
                        os = (string)_dataReader["os"],
                        manufacturer = (string)_dataReader["manufacturer"],
                        memoryStorage = (string)_dataReader["memoryStorage"]
                    };
                    _totalProductCount = (int)_dataReader["TotalCount"];
                    _product.Add(phone);
                }
                if (_totalProductCount != _totalProductItems)
                {
                    _totalProductItems = _totalProductCount;
                    _totalProduct = (_totalProductItems / _productPerPage) +
                        (((_totalProductItems % _productPerPage) == 0) ? 0 : 1);
                }

                CurProductPage.Text = _currentProductPage.ToString();
                TotalProductPage.Text = _totalProduct.ToString();

                if (_totalProduct == -1 || _totalProduct == 0 || _product.Count == 0)
                {
                    CurProductPage.Text = "1";
                    TotalProductPage.Text = "1";
                }

                _dataReader.Close();
            } catch
            {
                _dataReader.Close();
            }
        }
    }
}