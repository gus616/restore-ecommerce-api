using Identity.Interfaces;
using Identity.Models;
using Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace Identity.Services
{
    public class LoginService : ILoginService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        public LoginService(UserManager<AppUser> userManager, IJwtTokenGenerator jwtTokenGenerator)
        {
            _userManager = userManager;
            _jwtTokenGenerator = jwtTokenGenerator;
        }
        public async Task<AuthResult> LoginAsync(string identifier, string password)
        {
            AppUser? user = null;
           if(identifier.Contains("@"))
            {
                user = await _userManager.FindByEmailAsync(identifier);
            }

           else
            {
                user = await _userManager.Users.FirstOrDefaultAsync(u =>  u.Id == identifier);
            }

            if (user == null || !await _userManager.CheckPasswordAsync(user, password)) 
            {
                return AuthResult.Failure("Invalid credentials");
            }

            var token = _jwtTokenGenerator.GenerateToken(user);
            return AuthResult.Success(token, user.Email!, user.FullName!, user.PhoneNumber!);


        }
    }
}
