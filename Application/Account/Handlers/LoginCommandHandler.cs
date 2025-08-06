using Application.Account.Commands;
using Identity.Interfaces;
using Identity.Models;
using Infrastructure.Identity.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Account.Handlers
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, AuthResult>
    {
        private readonly ILoginService _loginService;
        public LoginCommandHandler(ILoginService loginService)
        {
            _loginService = loginService;
        }
        public async Task<AuthResult> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            return await _loginService.LoginAsync(request.Autenticator, request.Password);
        }
    }
}
