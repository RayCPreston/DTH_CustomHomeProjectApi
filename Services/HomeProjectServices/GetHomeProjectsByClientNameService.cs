using DTH.API.Data;
using DTH.API.Exceptions;
using DTH.API.Interfaces;
using DTH.API.Models;

namespace DTH.API.Services.HomeProjectServices
{
    public class GetHomeProjectsByClientNameService : IGetHomeProjectsService
    {
        private readonly ILogger<GetHomeProjectsByClientNameService> _logger;
        private readonly HomeProjectDbContext _context;

        public GetHomeProjectsByClientNameService(ILogger<GetHomeProjectsByClientNameService> logger, HomeProjectDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        /// <summary>
        /// Returns a List of HomeProjects where the ClientName matches passed parameter.
        /// Matching is case-insensitive.
        /// </summary>
        /// <param name="clientName">string</param>
        /// <returns>List</returns>
        /// <exception cref="NotFoundException">Thrown when no HomeProjects are found</exception>
        public List<HomeProject> GetHomeProjects(string clientName)
        {
            try
            {
                if (_context.HomeProjects == null)
                {
                    throw new NotFoundException("No home projects found.", "034");
                }
                List<HomeProject> homeProjects = _context.HomeProjects
                    .Where(hp => hp.ClientName != null && hp.ClientName.ToLower() == clientName.ToLower())
                    .ToList();
                if (homeProjects == null || !homeProjects.Any())
                {
                    throw new NotFoundException($"No home projects found for {clientName}.", "044"); ;
                }
                return homeProjects;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Exception ocurred in GetHomeProjectsByClientNameAction.GetHomeProjectsByClientName {ex.Message}");
                throw;
            }
        }
    }
}
