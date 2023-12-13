﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace MyShop.pages
{
    public class DB
    {
        private static DB? _instance = null;
        private SqlConnection _connection = null;

        public string ConnectionString { get; set; } = "Server=DESKTOP-8IL3H18\\SQLEXPRESS01;Database=ImportExcel;Integrated Security=True;";

        public SqlConnection? Connection
        {
            get
            {
                if (_connection == null)
                {
                    _connection = new SqlConnection(ConnectionString);
                    _connection.Open();
                }

                return _connection;
            }
        }

        // Chuỗi kết nối với tùy chọn Windows Authentication

        public static DB Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DB();
                }

                return _instance;
            }
        }
    }
}
