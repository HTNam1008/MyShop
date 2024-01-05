using MyShop.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using System.Reflection;

namespace MyShop.DAO
{
    public class OrderDetailDAO
    {
        SqlDataReader _dataReader;
        public List<OrderDetailDTO> GetAll()
        {
            string sql = @"select * from "+ Database.Instance.tableOrder;

            SqlCommand command = new SqlCommand(sql, Database.Instance.Connection);

            _dataReader = command.ExecuteReader();

            List<OrderDetailDTO> _list = new List<OrderDetailDTO>();

            while(_dataReader.Read())
            {
                OrderDetailDTO phone = new OrderDetailDTO()
                {
                    phone = (string)_dataReader["PhoneName"],
                    image = (string)_dataReader["image"],
                    total = (Int32)_dataReader["total"],
                    amount = (Int32)_dataReader["amount"]
                };
                _list.Add(phone);
            }
            return _list;
        }

        public List<OrderDetailDTO> GetByOrderID(string orderID)
        {
            string sql = $"select * from " + Database.Instance.tableOrder + @" where OrderId = {orderID} Order by phonename";

            SqlCommand command = new SqlCommand(sql, Database.Instance.Connection);

            _dataReader = command.ExecuteReader();

            List<OrderDetailDTO> _list = new List<OrderDetailDTO>();

            while (_dataReader.Read())
            {
                OrderDetailDTO phone = new OrderDetailDTO()
                {
                    phone = (string)_dataReader["PhoneName"],
                    image = (string)_dataReader["image"],
                    total = (Int32)_dataReader["total"],
                    amount = (Int32)_dataReader["amount"]
                };
                _list.Add(phone);
            }
            return _list;
        }

        public int DeleteOrder(string orderID)
        {
            string sql = $"delete from " + Database.Instance.tableOrder + @" where OrderId = " + orderID;
            SqlCommand command = new SqlCommand(sql, Database.Instance.Connection);
            int recordChanged = command.ExecuteNonQuery();
            return recordChanged;
        }
    }
}
