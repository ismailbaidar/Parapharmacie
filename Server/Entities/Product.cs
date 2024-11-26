using Microsoft.SqlServer.Server;
using Middleware.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Server.Entities
{
    public class Product : MarshalByRefObject, IProductRPC
    {
        private readonly SqlConnection connection;
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int Qte { get; set; }
        public int CategoryId { get; set; }

        public Product()
        {
            connection = new SqlConnection("Data Source=PC\\SQLEXPRESS;Initial Catalog=Parapharmacydb;Integrated Security=True");

        }

        public Product(string Name, string Description, double Price, int Qte,int CategoryId)
        {
            Name = Name;
            Description = Description;
            Price = Price;
            Qte = Qte;
            CategoryId = CategoryId;
        }
        public Product(int Id,string Name, string Description, double Price, int Qte, int CategoryId)
        {
            Id = Id;
            Name = Name;
            Description = Description;
            Price = Price;
            Qte = Qte;
            CategoryId = CategoryId;
        }


        // get all products
        public List<Dictionary<string, object>> GetAllProducts()
        {

            
                List<Dictionary<string,object>> products = new List<Dictionary<string, object>>();

                string query = "SELECT * FROM Product";  // SQL query to fetch all products

                connection.Open();
                
                    

                        using (SqlCommand cmd = new SqlCommand(query, connection))
                        {
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    Dictionary<string, object> product = new Dictionary<string, object>
                                    {
                                        { "Id", reader.IsDBNull(0) ? 0 : reader.GetInt32(0) },
                                        { "Name", reader.IsDBNull(1) ? string.Empty : reader.GetString(1) },
                                        { "Description", reader.IsDBNull(2) ? string.Empty : reader.GetString(2) },
                                        { "Price", reader.IsDBNull(3) ? 0.0 : reader.GetDouble(3) },
                                        { "Qte", reader.IsDBNull(4) ? 0 : reader.GetInt32(4) },
                                        { "CategoryId", reader.IsDBNull(5) ? 0 : reader.GetInt32(5) }
                                    };

                        // Add the product as an object to the list
                        products.Add(product);  // Cast to object
                    }
                }
            }



            connection.Close();

            return products;
        }

        public void DeleteProduct(int productId)
        {
            string query = "DELETE FROM Product WHERE Id = @Id";

            

            try
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {

                    cmd.Parameters.AddWithValue("@Id", productId);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Product deleted successfully.");
                    }
                    else
                    {
                        MessageBox.Show("Product not found or already deleted.");
                    }
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting product: " + ex.Message);
            }
        }

        public void AddProduct(string name, string description, double price, int quantity, int categoryId)
        {
            string query = "INSERT INTO Product (Name, Description, Price, Qte, CategoryId) VALUES (@Name, @Description, @Price, @Qte, @CategoryId)";

            
            {
                try
                {
                    connection.Open();

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        // Add parameters to avoid SQL injection
                        cmd.Parameters.AddWithValue("@Name", name);
                        cmd.Parameters.AddWithValue("@Description", description);
                        cmd.Parameters.AddWithValue("@Price", price);
                        cmd.Parameters.AddWithValue("@Qte", quantity);
                        cmd.Parameters.AddWithValue("@CategoryId", categoryId);

                        // Execute the query
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Product added successfully.");
                        }
                        else
                        {
                            MessageBox.Show("Failed to add product.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error inserting product: " + ex.Message);
                }
            }
        }
    }
}

