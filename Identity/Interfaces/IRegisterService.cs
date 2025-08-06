using Identity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Interfaces
{
    public interface IRegisterService
    {
        Task<AuthResult> RegisterAsync(string fullName, string email, string phoneNumber, string password);
    }
}
