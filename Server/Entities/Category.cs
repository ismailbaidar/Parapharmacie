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
    public class Category : MarshalByRefObject, ICategoryRPC
    {
        private readonly SqlConnection connection;
        public int Id { get; set; }
        public string Name { get; set; }

        public Category()
        {
            connection = new SqlConnection("Data Source=PC\\SQLEXPRESS;Initial Catalog=Parapharmacydb;Integrated Security=True");
        }

        public List<Dictionary<string, object>> GetAllCategories()
        {


            List<Dictionary<string, object>> categories = new List<Dictionary<string, object>>();

            string query = "SELECT * FROM Category";  

            connection.Open();



            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Dictionary<string, object> category = new Dictionary<string, object>
                                    {
                                        { "Id", reader.IsDBNull(0) ? 0 : reader.GetInt32(0) },
                                        { "Name", reader.IsDBNull(1) ? string.Empty : reader.GetString(1) },
                                        
                                    };


                        categories.Add(category);  
                    }
                }
            }



            connection.Close();

            return categories;
        }
        public void DeleteCategory(int categoryId)
        {
            string query = "DELETE FROM Category WHERE Id = @Id";



            try
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {

                    cmd.Parameters.AddWithValue("@Id", categoryId);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Category deleted successfully.");
                    }
                    else
                    {
                        MessageBox.Show("Category not found or already deleted.");
                    }
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting Category: " + ex.Message);
            }
        }
        public void AddCategory(string name)
        {
            string query = "INSERT INTO Category (Name) VALUES (@Name)";
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
                            MessageBox.Show("Category added successfully.");
                        }
                        else
                        {
                            MessageBox.Show("Failed to add categoory.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error inserting category: " + ex.Message);
                }
            }
        }

        public void UpdateCategory(int categoryId, string name)
        {
            string query = "Update Category set Name = @n where id = @i";
            {
                try
                {
                    connection.Open();

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@n", name);
                        cmd.Parameters.AddWithValue("@i", categoryId);
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Category updated successfully.");
                        }
                        else
                        {
                            MessageBox.Show("Failed to update Category.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error updating Category: " + ex.Message);
                }
            }
        }

        public Dictionary<string, object> GetCategoryById(int categoryId)
        {
            Dictionary<string, object> category;

            string query = "SELECT * FROM Category where id = @i";

            connection.Open();



            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@i", categoryId);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                        reader.Read();
                    
                         category =new Dictionary<string, object>(){
                                        { "Id", reader.IsDBNull(0) ? 0 : reader.GetInt32(0) },
                                        { "Name", reader.IsDBNull(1) ? string.Empty : reader.GetString(1) },

                                    };


                        
                    
                }
            }



            connection.Close();

            return category;
        }
    }
    
}
