using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace MyShop
{
    public class Server
    {
        private static Server? _instance = null;
        public string Name { get; set; }
        public static Server Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Server("");
                }
                return _instance;
            }
        }
        public Server(string name)
        {
            Name = name;
        }
    }
    public class Database
    {
        private static Database? _instance = null;
        private SqlConnection _connection = null;

        public string ConnectionString { get; set; }
        public SqlConnection? Connection
        {
            get
            {
                if (_connection == null)
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
                if (_instance == null)
                {
                    _instance = new Database();
                }
                return _instance;
            }
        }
    }
}
