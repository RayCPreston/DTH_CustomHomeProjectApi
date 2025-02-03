using DTH.API.Data;
using DTH.API.Exceptions;
using DTH.API.Models;
using DTH.API.Utility;

namespace DTH.API.Services.HomeProjectServices
{
    public class UpdateHomeProjectService
    {
        private readonly ILogger<UpdateHomeProjectService> _logger;
        private readonly HomeProjectDbContext _context;
        private readonly GetHomeProjectByProjectId _getHomeProjectByProjectId;

        public UpdateHomeProjectService(ILogger<UpdateHomeProjectService> logger,
            HomeProjectDbContext context,
            GetHomeProjectByProjectId getHomeProjectByProjectId)
        {
            _logger = logger;
            _context = context;
            _getHomeProjectByProjectId = getHomeProjectByProjectId;
        }

        /// <summary>
        /// Updates the HomeProject record in the DB with a matching ProjectId using the passed HomeProject object.
        /// </summary>
        /// <param name="homeProject">HomeProject</param>
        /// <returns>HomeProject</returns>
        /// <exception cref="ValidationFailedException">Thrown when HomeProject is not valid</exception>
        /// <exception cref="NotFoundException">Thrown when HomeProject with a matching ProjectId is not found</exception>
        public HomeProject UpdateHomeProject(HomeProject homeProject)
        {
            try
            {
                if (!homeProject.IsValid())
                {
                    throw new ValidationFailedException("Home Project is not valid.", "004");
                }
                if (homeProject.ProjectId == null || homeProject.ProjectId.Length == 0)
                {
                    throw new ValidationFailedException("ProjectId is required.", "064");
                }
                HomeProject? existingProject = _getHomeProjectByProjectId.GetHomeProject(homeProject.ProjectId);
                if (existingProject == null)
                {
                    throw new NotFoundException($"ProjectId {homeProject.ProjectId} not found.", "044");
                }
                homeProject.Id = existingProject.Id;
                _context.Entry(existingProject).CurrentValues.SetValues(homeProject);
                _context.SaveChanges();
                return existingProject;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Exception ocurred in DeleteHomeProjectService.DeleteHomeProject {ex.Message}");
                throw;
            }
        }
    }
}
