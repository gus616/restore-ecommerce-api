
namespace Identity.Models
{    public class AuthResult
    {
        public bool Succeeded { get; set; }
        public string[] Errors { get; set; } = [];
        public string? Token { get; set; }
        public string? Email { get; set; }
        public string? UserName { get; set; }
        public string? PhoneNumber { get; set; }

        public static AuthResult Success(string token, string email, string userName, string phoneNumber) =>
            new() { Succeeded = true, Token = token, Email = email, UserName = userName, PhoneNumber = phoneNumber };

        public static AuthResult Failure(params string[] errors) =>
            new() { Succeeded = false, Errors = errors };
    }

}
