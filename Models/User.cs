using Microsoft.EntityFrameworkCore;

namespace DTH.API.Models
{
    [Index(nameof(Username), IsUnique = true)]
    public class User
    {
        public Guid Id { get; set; }
        public required string Username { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<UserRole>? Roles { get; set; } = new List<UserRole>();
        public ICollection<UserClaim>? Claims { get; set; } = new List<UserClaim>();
    }

    public class UserRole
    {
        public Guid Id { get; set; }
        public string? RoleName { get; set; }
    }

    public class UserClaim
    {
        public Guid Id { get; set; }
        public string? ClaimType { get; set; }
        public string? ClaimValue { get; set; }
    }

    public record UserDTO(string Username, string? Email, string? FirstName, string? LastName);
}

