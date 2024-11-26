using Middleware.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server.Entities
{
    public class User : MarshalByRefObject, IUserRPC

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
        public User(string Name, string Email, string Password, int RoleId)
        {
            Name = Name;
            Email = Email;
            Password = Password;
            RoleId = RoleId;
            connection = new SqlConnection("Data Source=PC\\SQLEXPRESS;Initial Catalog=Parapharmacydb;Trust Server Certificate=True;Integrated Security=True");

        }

        public List<object> Login(string Email, string Password)
        {
            string userDbPassword = "";
            int roleId = 0;

            string query = "Select * From \"User\" where Email = @e";
            connection.Open();
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@e", Email);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        userDbPassword = reader.GetString(3);
                        roleId = reader.GetInt32(4);

                    }
                }
            }
            connection.Close();
            return new List<object>() { BCrypt.Net.BCrypt.Verify(Password, userDbPassword), roleId };
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

        public List<Dictionary<string, object>> getAllUsers()
        {
            List<Dictionary<string, object>> users = new List<Dictionary<string, object>>();

            string query = "SELECT * FROM \"User\" ";

            connection.Open();



            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Dictionary<string, object> user = new Dictionary<string, object>
                                    {
                                        { "Id", reader.IsDBNull(0) ? 0 : reader.GetInt32(0) },
                                        { "Name", reader.IsDBNull(1) ? string.Empty : reader.GetString(1) },
                                        { "Email", reader.IsDBNull(1) ? string.Empty : reader.GetString(2) },
                                        { "RoleId", reader.IsDBNull(1) ?0 : reader.GetInt32(4) },



                                    };


                        users.Add(user);
                    }
                }
            }



            connection.Close();

            return users;
        }

        public void ResetPassword(int userId)
        {
            string password = BCrypt.Net.BCrypt.HashPassword("test");
            string query = "Update \"user\" set password = @p where id=@i";
            connection.Open();
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@i", userId);
                cmd.Parameters.AddWithValue("@p", password);
                cmd.ExecuteNonQuery();

            }
            connection.Close();

        }

        public void DeleteUser(int userId)
        {
            string query = "DELETE FROM \"User\" WHERE Id = @Id";



            try
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {

                    cmd.Parameters.AddWithValue("@Id", userId);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("User deleted successfully.");
                    }
                    else
                    {
                        MessageBox.Show("User not found or already deleted.");
                    }
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting user: " + ex.Message);
            }
        }
    }

}

