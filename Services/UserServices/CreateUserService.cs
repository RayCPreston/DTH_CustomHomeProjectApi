using DTH.API.Data;
using DTH.API.Exceptions;
using DTH.API.Models;
using DTH.API.Utility;

namespace DTH.API.Services.UserServices
{
    public class CreateUserService
    {
        private readonly ILogger<CreateUserService> _logger;
        private readonly UserDbContext _context;
        private readonly GetUserService _getUserService;

        public CreateUserService(ILogger<CreateUserService> logger, UserDbContext context, GetUserService getUserService)
        {
            _logger = logger;
            _context = context;
            _getUserService = getUserService;
        }

        /// <summary>
        /// Creates a new User in the DB.
        /// </summary>
        /// <param name="user">User</param>
        /// <returns>User</returns>
        /// <exception cref="ValidationFailedException">Thrown when the passed user object has invalid contents</exception>
        public User CreateUser(User user)
        {
            try
            {
                if (_context.Users == null)
                {
                    throw new Exception("No users DB found.");
                }
                if (!user.IsValid())
                {
                    throw new ValidationFailedException("User is not valid.", "074");
                }
                User? existingUser = _getUserService.GetUser(user.Username);
                if (existingUser != null)
                {
                    throw new ValidationFailedException($"User with username {user.Username} already exists.", "084");
                }
                _context.Users.Add(user);
                _context.SaveChanges();
                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Exception ocurred in CreateUserService.CreateUser {ex.Message}");
                throw;
            }
        }
    }
}
