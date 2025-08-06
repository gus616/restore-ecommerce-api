using Identity.Models;
using MediatR;

namespace Application.Account.Commands
{
    public record RegisterCommand(string email, string phoneNumber, string fullName, string password) : IRequest<AuthResult>;
}
