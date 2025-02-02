using DTH.API.Data;
using DTH.API.Exceptions;
using DTH.API.Models;

namespace DTH.API.Services
{
    public class GetHomeProjectsByStatusService
    {
        private readonly ILogger<GetHomeProjectsByStatusService> _logger;
        private readonly HomeProjectDbContext _context;
        public GetHomeProjectsByStatusService(ILogger<GetHomeProjectsByStatusService> logger, HomeProjectDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        /// <summary>
        /// Returns a List of HomeProjects where the ProjectStatus matches passed parameter.
        /// </summary>
        /// <param name="projectStatus">ProjectStatus enum</param>
        /// <returns>List&lt;HomeProject&gt;</returns>
        /// <exception cref="NotFoundException">Thrown when no HomeProjects are found</exception>
        public List<HomeProject> GetHomeProjects(ProjectStatus projectStatus)
        {
            try
            {
                if (_context.HomeProjects == null)
                {
                    throw new NotFoundException("No home projects found.", "034");
                }
                List<HomeProject> homeProjects = _context.HomeProjects
                    .Where(hp => hp.ProjectStatus != null && hp.ProjectStatus == projectStatus)
                    .ToList();
                if (homeProjects == null || !homeProjects.Any())
                {
                    throw new NotFoundException($"No home projects found with status {projectStatus.ToString()}.", "044");
                }
                return homeProjects;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Exception ocurred in GetHomeProjectsByStatusService.GetHomeProjectsByStatus {ex.Message}");
                throw;
            }
        }
    }
}
