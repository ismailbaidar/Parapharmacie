using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Middleware.Interfaces
{
    public interface ICategoryRPC
    {

        List<Dictionary<string, object>> GetAllCategories();
        void AddCategory(string name);
        void DeleteCategory(int categoryId);
        void UpdateCategory(int categoryId,string name);
        Dictionary<string, object> GetCategoryById(int categoryId);


    }
}
