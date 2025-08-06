using Infrastructure.Identity.Entities;


namespace Identity.Interfaces
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(AppUser user);
    }
}
