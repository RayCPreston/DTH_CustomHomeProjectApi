using DTH.API.Data;
using DTH.API.Exceptions;
using DTH.API.Interfaces;
using DTH.API.Models;

namespace DTH.API.Services.HomeProjectServices
{
    public class GetHomeProjectsByProjectNameService : IGetHomeProjectsService
    {
        private readonly ILogger<GetHomeProjectsByProjectNameService> _logger;
        private readonly HomeProjectDbContext _context;
        public GetHomeProjectsByProjectNameService(ILogger<GetHomeProjectsByProjectNameService> logger, HomeProjectDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        /// <summary>
        /// Returns a List of HomeProjects where the ProjectName matches passed parameter.
        /// Matching is case-insensitive.
        /// </summary>
        /// <param name="projectName">string</param>
        /// <returns>List</returns>
        /// <exception cref="NotFoundException">Thrown when no HomeProjects are found</exception>
        public List<HomeProject> GetHomeProjects(string projectName)
        {
            try
            {
                if (_context.HomeProjects == null)
                {
                    throw new NotFoundException("No home projects found.", "034");
                }
                List<HomeProject> homeProjects = _context.HomeProjects
                    .Where(hp => hp.ProjectName != null && hp.ProjectName.ToLower() == projectName.ToLower())
                    .ToList();
                if (homeProjects == null || !homeProjects.Any())
                {
                    throw new NotFoundException($"No home projects found with project name: {projectName}.", "044");
                }
                return homeProjects;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Exception ocurred in GetHomeProjectsByProjectNameAction.GetHomeProjectsByProjectName {ex.Message}");
                throw;
            }
        }
    }
}