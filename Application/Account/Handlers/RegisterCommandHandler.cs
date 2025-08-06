using Application.Account.Commands;
using Identity.Models;
using Identity.Interfaces;
using MediatR;

namespace Application.Account.Handlers
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, AuthResult>
    {
        private readonly IRegisterService _registerService;

        public RegisterCommandHandler(IRegisterService registerService)
        {
            _registerService = registerService;
        }

        public async Task<AuthResult> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            return await _registerService.RegisterAsync(request.fullName, request.email, request.phoneNumber, request.password);
        }
    }
}
