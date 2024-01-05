using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyShop.DTO;
using Microsoft.Data.SqlClient;

namespace MyShop.DAO
{
    public class PhoneDAO
    {
        public bool insertPhone(PhoneDTO phone)
        {
            var sql = @"INSERT INTO " + Database.Instance.tablePhone + "(ID,Name,Quantity, OS, Manufacturer,PriceOriginal, Price, MemoryStorage,Image,Details)"
                    + " values (@ID,@Name,@Quantity, @OS, @Manufacturer,@PriceOriginal, @Price,@MemoryStorage,@Image,@Details)";
            var command = new SqlCommand(sql, Database.Instance.Connection);

            command.Parameters.Add("@ID", System.Data.SqlDbType.Int).Value = phone.id;
            command.Parameters.Add("@Name", System.Data.SqlDbType.NVarChar).Value = phone.name;
            command.Parameters.Add("@Quantity", System.Data.SqlDbType.Int).Value = phone.quantity;
            command.Parameters.Add("@OS", System.Data.SqlDbType.NVarChar).Value = phone.os;
            command.Parameters.Add("@Manufacturer", System.Data.SqlDbType.NVarChar).Value = phone.manufacturer;
            command.Parameters.Add("@PriceOriginal", System.Data.SqlDbType.Int).Value = phone.priceOriginal;
            command.Parameters.Add("@Price", System.Data.SqlDbType.Int).Value = phone.price;
            command.Parameters.Add("@MemoryStorage", System.Data.SqlDbType.NVarChar).Value = phone.memoryStorage;
            command.Parameters.Add("@Image", System.Data.SqlDbType.NVarChar).Value = phone.image;
            command.Parameters.Add("@Details", System.Data.SqlDbType.NVarChar).Value = phone.details;

            int result = command.ExecuteNonQuery();

            return result > 0;
        }
        public void deletePhoneByID(int phoneID)
        {
            var sql = @"DELETE FROM " + Database.Instance.tablePhone + " WHERE ID = @ID";
            var command = new SqlCommand(sql, Database.Instance.Connection);

            command.Parameters.Add("@ID", System.Data.SqlDbType.Int).Value = phoneID;

            command.ExecuteNonQuery();
        }

        public void updatePhone(PhoneDTO phone)
        {
            var sql = @"UPDATE " + Database.Instance.tablePhone +
                " set Name=@name, OS=@os, Manufacturer=@manufacturer, Price=@price,MemoryStorage=@memoryStorage, Image=@image, Details=@details, Quantity=@quantity, PriceOriginal=@priceOriginal" +
                " where ID=@ID";
            var command = new SqlCommand(sql, Database.Instance.Connection);
            command.Parameters.Add("@name", System.Data.SqlDbType.Text)
                .Value = phone.name;
            command.Parameters.Add("@os", System.Data.SqlDbType.Text)
                .Value = phone.os;
            command.Parameters.Add("@price", System.Data.SqlDbType.Int)
                .Value = phone.price;
            command.Parameters.Add("@manufacturer", System.Data.SqlDbType.Text)
                .Value = phone.manufacturer;
            command.Parameters.Add("@memoryStorage", System.Data.SqlDbType.Text)
                .Value = phone.memoryStorage;
            command.Parameters.Add("@image", System.Data.SqlDbType.Text)
                .Value = phone.image;
            command.Parameters.Add("@ID", System.Data.SqlDbType.Int)
                .Value = phone.id;
            command.Parameters.Add("@details", System.Data.SqlDbType.Text)
                .Value = phone.details;
            command.Parameters.Add("@quantity", System.Data.SqlDbType.Int)
                .Value = phone.quantity;
            command.Parameters.Add("@priceOriginal", System.Data.SqlDbType.Int)
                .Value = phone.priceOriginal;

            command.ExecuteNonQuery();
        }
    }
}
