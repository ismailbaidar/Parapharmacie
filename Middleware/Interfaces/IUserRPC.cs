using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Middleware.Interfaces
{
    public interface IUserRPC
    {
        List<object> Login(string Email,string Password);
        bool Register(string Email, string Password, string Name, int RoleId);
        List<Dictionary<string, object>> getAllUsers();
        void ResetPassword(int userId);
        void DeleteUser(int userId);
    }
}
