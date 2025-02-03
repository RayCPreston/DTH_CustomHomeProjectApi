using DTH.API.Data;
using DTH.API.Exceptions;
using DTH.API.Models;

namespace DTH.API.Services.HomeProjectServices
{
    public class GetHomeProjectByProjectId
    {
        private readonly ILogger<GetHomeProjectByProjectId> _logger;
        private readonly HomeProjectDbContext _context;
        public GetHomeProjectByProjectId(ILogger<GetHomeProjectByProjectId> logger, HomeProjectDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        /// <summary>
        /// Returns a HomeProject with a ProjectId matching the passed parameter.
        /// </summary>
        /// <param name="projectId">string</param>
        /// <returns>HomeProject</returns>
        public HomeProject? GetHomeProject(string projectId)
        {
            try
            {
                if (_context.HomeProjects == null)
                {
                    throw new NotFoundException("No home projects found.", "034");
                }
                HomeProject? homeProject = _context.HomeProjects
                    .Where(hp => hp.ProjectId != null && hp.ProjectId.ToLower() == projectId.ToLower())
                    .FirstOrDefault();
                if (homeProject == null)
                {
                    return null;
                }
                return homeProject;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Exception ocurred in GetHomeProjectByProjectId.GetHomeProject {ex.Message}");
                throw;
            }
        }
    }
}
