using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using MyShop.DTO;

namespace MyShop.DAO
{
    public class CustomerOrderDAO
    {
        SqlDataReader _dataReader;

        public List<CustomerOrderDTO> GetAll()
        {
            string sql = @"select * from " + Database.Instance.tableCustomerOrder + @" Order by OrderID";

            SqlCommand command = new SqlCommand(sql, Database.Instance.Connection);

            _dataReader = command.ExecuteReader();

            List<CustomerOrderDTO> _list = new List<CustomerOrderDTO>();


            while (_dataReader.Read())
            {
                CustomerOrderDTO order = new CustomerOrderDTO()
                {
                    OrderId = (int)_dataReader["OrderID"],
                    PhoneNum = (string)_dataReader["PhoneNum"],
                    Status = (string)_dataReader["Status"],
                    CreateDate = (DateTime)_dataReader["CreateDate"],
                    ShipDate = (DateTime)_dataReader["ShipmentDate"],
                    TotalCost = (Int32)_dataReader["totalcost"]
                };
                order.StatusImgPath = "res\\icon\\" + order.Status + ".png";
                _list.Add(order);
            }

            return _list;
        }

        public List<CustomerOrderDTO> GetByID(string OrderID)
        {
            string sql = @"select * from " + Database.Instance.tableCustomerOrder + " where OrderId = "+ OrderID + @" Order by OrderID";

            SqlCommand command = new SqlCommand(sql, Database.Instance.Connection);

            _dataReader = command.ExecuteReader();

            List<CustomerOrderDTO> _list = new List<CustomerOrderDTO>();


            while (_dataReader.Read())
            {
                CustomerOrderDTO order = new CustomerOrderDTO()
                {
                    OrderId = (int)_dataReader["OrderID"],
                    PhoneNum = (string)_dataReader["PhoneNum"],
                    Status = (string)_dataReader["Status"],
                    CreateDate = (DateTime)_dataReader["CreateDate"],
                    ShipDate = (DateTime)_dataReader["ShipmentDate"],
                    TotalCost = (Int32)_dataReader["totalcost"]
                };
                order.StatusImgPath = "res\\icon\\" + order.Status + ".png";
                _list.Add(order);
            }

            return _list;
        }
        public int DeleteCustomerOrder(string orderID)
        {
            string sql = $"delete from " + Database.Instance.tableCustomerOrder + @" where OrderId = " + orderID;
            SqlCommand command = new SqlCommand(sql, Database.Instance.Connection);
            int recordChanged = command.ExecuteNonQuery();
            return recordChanged;
        }
    }
}
