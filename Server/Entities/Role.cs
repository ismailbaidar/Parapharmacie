using Middleware.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Server.Entities
{
    public class Role : MarshalByRefObject, IRoleRPC
    {
        SqlConnection connection;
        public int Id { get; set; }
        public string Name { get; set; }

        public Role()
        {
            connection = new SqlConnection("Data Source=PC\\SQLEXPRESS;Initial Catalog=Parapharmacydb;Integrated Security=True");
        }

        public List<Dictionary<string, object>> GetAllRoles()
        {
            List<Dictionary<string, object>> roles = new List<Dictionary<string, object>>();

            string query = "SELECT * FROM Role";
            connection.Open();
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Dictionary<string, object> role = new Dictionary<string, object>
                                    {
                                        { "Id", reader.IsDBNull(0) ? 0 : reader.GetInt32(0) },
                                        { "Name", reader.IsDBNull(1) ? string.Empty : reader.GetString(1) },
                                    };
                        roles.Add(role);
                    }
                }
            }
            connection.Close();
            return roles;
        }


        public void DeleteRole(int roleId)
        {
            string query = "DELETE FROM Role WHERE Id = @Id";
            try
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Id", roleId);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Role deleted successfully.");
                    }
                    else
                    {
                        MessageBox.Show("Role not found or already deleted.");
                    }
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting Role: " + ex.Message);
            }
        }

        public void AddRole(string name)
        {
            string query = "INSERT INTO Role (Name) VALUES (@Name)";
            {
                try
                {
                    connection.Open();

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@Name", name);
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Role added successfully.");
                        }
                        else
                        {
                            MessageBox.Show("Failed to add role.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error inserting role: " + ex.Message);
                }
            }
        }

        public void UpdateRole(int roleId, string roleName)
        {
            string query = "Update Role set Name = @n where id = @i";
            {
                try
                {
                    connection.Open();

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@n", roleName);
                        cmd.Parameters.AddWithValue("@i", roleId);
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Role updated successfully.");
                        }
                        else
                        {
                            MessageBox.Show("Failed to update role.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error updating role: " + ex.Message);
                }
            }
        }
    }
}
