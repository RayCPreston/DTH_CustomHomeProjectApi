using Microsoft.EntityFrameworkCore;

namespace DTH.API.Models
{
    [Index(nameof(Username), IsUnique = true)]
    public class User
    {
        public Guid Id { get; set; }
        public required string Username { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;
        public string? Password { get; set; } = string.Empty;
        public string? FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; } = string.Empty;
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<UserRole>? Roles { get; set; } = new List<UserRole>();
        public ICollection<UserClaim>? Claims { get; set; } = new List<UserClaim>();
    }

    public class UserRole
    {
        public Guid Id { get; set; }
        public string RoleName { get; set; } = string.Empty;
    }

    public class UserClaim
    {
        public Guid Id { get; set; }
        public string ClaimType { get; set; } = string.Empty;
        public string ClaimValue { get; set; } = string.Empty;
    }

    public record UserDTO(string Username, string? Email, string? FirstName, string? LastName);
}

