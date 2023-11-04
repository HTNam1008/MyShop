using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop_Login
{
    public class Database
    {
        private static Database? _instance = null;
        private SqlConnection _connection = null;

        public string ConnectionString { get; set; }
        public SqlConnection? Connection
        {
            get
            {
                if(_connection == null)
                {
                    _connection = new SqlConnection(ConnectionString); ;
                    _connection.Open();
                }
                return _connection;
            }
        }

        public static Database Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new Database();
                }
                return _instance;
            }
        }
    }
}
