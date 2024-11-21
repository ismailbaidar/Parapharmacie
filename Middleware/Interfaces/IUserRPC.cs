using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Middleware.Interfaces
{
    public interface IUserRPC
    {
        bool Login(string Email,string Password);
        bool Register(string Email, string Password, string Name, int RoleId);
    }
}
