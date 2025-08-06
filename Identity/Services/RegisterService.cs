using Identity.Interfaces;
using Identity.Models;
using Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Services
{
    public class RegisterService : IRegisterService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public RegisterService(UserManager<AppUser> userManager, IJwtTokenGenerator jwtTokenGenerator)
        {
            _userManager = userManager;
            _jwtTokenGenerator = jwtTokenGenerator;
        }
        public async Task<AuthResult> RegisterAsync(string fullName, string email, string phoneNumber, string password)
        {
            var user = new AppUser
            {
                UserName = email,
                FullName = fullName,
                Email = email,
                PhoneNumber = phoneNumber,
            };

            var result = await _userManager.CreateAsync(user, password);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description).ToArray();
                return AuthResult.Failure(errors);
            }

            var token = _jwtTokenGenerator.GenerateToken(user);

            return AuthResult.Success(token, user.Email, user.FullName, user.PhoneNumber);

        }
    }
}
