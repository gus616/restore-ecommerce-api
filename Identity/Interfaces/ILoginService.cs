

using Identity.Models;

namespace Identity.Interfaces
{
    public interface ILoginService
    {
        Task<AuthResult> LoginAsync(string authenticator, string password);
    }
}
