using Identity.Models;
using MediatR;


namespace Application.Account.Commands
{
    public record LoginCommand(string Autenticator, string Password) : IRequest<AuthResult>;    
}
