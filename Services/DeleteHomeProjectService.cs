using DTH.API.Data;
using DTH.API.Exceptions;
using DTH.API.Models;

namespace DTH.API.Services
{
    public class DeleteHomeProjectService
    {
        private readonly ILogger<DeleteHomeProjectService> _logger;
        private readonly HomeProjectDbContext _context;
        private readonly GetHomeProjectByProjectId _getHomeProjectByProjectId;
        public DeleteHomeProjectService(ILogger<DeleteHomeProjectService> logger,
            HomeProjectDbContext context,
            GetHomeProjectByProjectId getHomeProjectByProjectId)
        {
            _logger = logger;
            _context = context;
            _getHomeProjectByProjectId = getHomeProjectByProjectId;
        }

        /// <summary>
        /// Deletes a HomeProject record from the DB using a projectId as the lookup.
        /// </summary>
        /// <param name="projectId">string</param>
        /// <returns>HomeProject</returns>
        /// <exception cref="NotFoundException">Thrown when HomeProject is not found</exception>
        public HomeProject DeleteHomeProject(string projectId)
        {
            try
            {
                if (_context.HomeProjects == null)
                {
                    throw new NotFoundException("No home projects found.", "034");
                }
                HomeProject? homeProject = _getHomeProjectByProjectId.GetHomeProject(projectId);
                if (homeProject == null)
                {
                    throw new NotFoundException($"ProjectId {projectId} not found.", "044");
                }
                _context.Remove(homeProject);
                _context.SaveChanges();
                return homeProject;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Exception ocurred in DeleteHomeProjectService.DeleteHomeProject {ex.Message}");
                throw;
            }
        }
    }
}
