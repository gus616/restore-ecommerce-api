using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Account.DTOs
{
    public class LoginDto
    {
        public string Identifier { get; set; } = null!; // Could be email or mobile

        public string Password { get; set; } = null!;
    }
}
