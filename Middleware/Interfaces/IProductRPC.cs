using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Middleware.Interfaces
{
    public interface IProductRPC
    {
        List<Dictionary<string, object>> GetAllProducts();
        void DeleteProduct(int productId);
        void AddProduct(string name, string description, double price, int quantity, int categoryId);
    }
}
