using DTH.API.Data;
using DTH.API.Exceptions;
using DTH.API.Models;

namespace DTH.API.Services.HomeProjectServices
{
    public class GetAllHomeProjects
    {
        private readonly ILogger<GetAllHomeProjects> _logger;
        private readonly HomeProjectDbContext _context;
        public GetAllHomeProjects(ILogger<GetAllHomeProjects> logger, HomeProjectDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        /// <summary>
        /// Returns a List of all HomeProjects.
        /// </summary>
        /// <returns>List&lt;HomeProject&gt;</returns>
        /// <exception cref="NotFoundException">Thrown when no HomeProjects are found</exception>
        public List<HomeProject> GetHomeProjects()
        {
            try
            {
                if (_context.HomeProjects == null)
                {
                    throw new NotFoundException("No home projects found.", "034");
                }
                List<HomeProject> homeProjects = _context.HomeProjects.ToList();
                if (homeProjects == null || !homeProjects.Any())
                {
                    throw new NotFoundException("No home projects found.", "034");
                }
                return homeProjects;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Exception ocurred in GetAllHomeProjects.GetHomeProjects {ex.Message}");
                throw;
            }
        }
    }
}
