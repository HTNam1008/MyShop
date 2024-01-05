using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using MyShop.DTO;
using MyShop.Helpers;
using MyShop.Helpers;

namespace MyShop.DAO
{
    public class AdminDAO
    {
        public AdminDTO? GetAdmin(string username, string password)
        {
            var sql = "SELECT * FROM Admin WHERE Email = @Username and Password = @Password";

            string unhashedPassword = Encryption.Encrypt(password, "1234567890123456");

            var command = new SqlCommand(sql, Database.Instance.Connection);
            command.Parameters.AddWithValue("@Username", username);
            command.Parameters.AddWithValue("@Password", unhashedPassword);

            var reader = command.ExecuteReader();

            AdminDTO? admin = null;
            if(reader.Read())
            {
                admin = new AdminDTO()
                {
                    ID = (string)reader["ID"],
                    FirstName = (string)reader["FirstName"],
                    LastName = (string)reader["LastName"],
                    Email = (string)reader["Email"],
                    Phone = (string)reader["Phone"],
                    Address = (string)reader["Address"],
                    Gender = (string)reader["Gender"],
                    Age = (int)reader["Age"],
                    Password = (string)reader["Password"],
                };
            }
            reader.Close();

            return admin;
        }

        public bool IsAdminExist(string id)
        {
            var sql = @"SELECT * FROM Admin Where ID = @ID";
            var command = new SqlCommand(sql, Database.Instance.Connection);
            command.Parameters.AddWithValue("@ID", id);

            var reader = command.ExecuteReader();
            if(!reader.HasRows)
            {
                return false;
            }
            return true;
        }

        public bool CreateAdmin(AdminDTO admin, string connectionString)
        {
            // add to database
            var sql = @"insert into Admin(id, firstName, lastName, gender, email, address, phone, age, password) 
                                values(@ID, @FirstName, @LastName, @Gender, @Email,
                                @Address, @Phone, @Age, @Password)";
            var command = new SqlCommand(sql, Database.Instance.Connection);
            command.Parameters.AddWithValue("@ID", admin.ID);
            command.Parameters.AddWithValue("@FirstName", admin.FirstName);
            command.Parameters.AddWithValue("@LastName", admin.LastName);
            command.Parameters.AddWithValue("@Gender", admin.Gender);
            command.Parameters.AddWithValue("@Email", admin.Email);
            command.Parameters.AddWithValue("@Address", admin.Address);
            command.Parameters.AddWithValue("@Phone", admin.Phone);
            command.Parameters.AddWithValue("@Age", admin.Age);
            command.Parameters.AddWithValue("Password", admin.Password);

            int rowsAffected = command.ExecuteNonQuery();

            return rowsAffected > 0;
        }
    }
}
