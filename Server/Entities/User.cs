using Middleware.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Entities
{
    public class User:MarshalByRefObject,IUserRPC

    {
        private SqlConnection connection;

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }

        public User()
        {
            connection = new SqlConnection("Data Source=PC\\SQLEXPRESS;Initial Catalog=Parapharmacydb;Integrated Security=True");

        }
        public User(string Name ,string Email,string Password,int RoleId)
        {
            Name = Name;
            Email = Email;
            Password = Password;
            RoleId = RoleId;
            connection = new SqlConnection("Data Source=PC\\SQLEXPRESS;Initial Catalog=Parapharmacydb;Trust Server Certificate=True;Integrated Security=True");
            
        }

        public bool Login(string Email,string Password)
        {
            string userDbPassword = "";

            string query = "Select * From User where Email = @e";
            using (SqlCommand cmd = new SqlCommand(query,connection))
            {
                cmd.Parameters.AddWithValue("@e", Email);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        userDbPassword = reader.GetString(4);
                    
                    }
                }
            }
            return BCrypt.Net.BCrypt.HashPassword(Password) == userDbPassword;
        }


        public bool Register(string Email, string Password, string Name, int RoleId)
        {
            string query = "Insert into \"user\" (Name,Email,Password,RoleId) values(@n,@e,@p,@r);";
            connection.Open();
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@e", Email);
                cmd.Parameters.AddWithValue("@n", Name);
                cmd.Parameters.AddWithValue("@p", BCrypt.Net.BCrypt.HashPassword(Password));
                cmd.Parameters.AddWithValue("@r", RoleId);
                cmd.ExecuteNonQuery();
                
            }
            connection.Close();
            return true;

        }
            
    }

}

