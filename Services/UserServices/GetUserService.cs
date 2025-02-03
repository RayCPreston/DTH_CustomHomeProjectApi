using DTH.API.Data;
using DTH.API.Exceptions;
using DTH.API.Models;

namespace DTH.API.Services.UserServices
{
    public class GetUserService
    {
        private readonly ILogger<GetUserService> _logger;
        private readonly UserDbContext _context;

        public GetUserService(ILogger<GetUserService> logger, UserDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        /// <summary>
        /// Returns a User whose Username matches the passed parameter.
        /// </summary>
        /// <param name="username">string</param>
        /// <returns>User</returns>
        /// <exception cref="NotFoundException">Thrown when no users are found or the passed username finds no match</exception>
        public User? GetUser(string username)
        {
            try
            {
                if (_context.Users == null)
                {
                    throw new NotFoundException("No users found.", "094");
                }
                User? user = _context.Users
                    .Where(u => u.Username != null && u.Username == username)
                    .FirstOrDefault();
                if (user == null)
                {
                    return null;
                }
                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Exception ocurred in GetUserByUsername.GetUser {ex.Message}");
                throw;
            }
        }
    }
}
