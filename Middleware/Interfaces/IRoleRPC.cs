using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Middleware.Interfaces
{
    public interface IRoleRPC
    {
        List<Dictionary<string, object>> GetAllRoles();
        void DeleteRole(int roleId);
        void AddRole(string name);
        void UpdateRole(int roleId, string roleName);
    }
}
