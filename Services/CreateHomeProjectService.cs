using DTH.API.Data;
using DTH.API.Exceptions;
using DTH.API.Models;
using DTH.API.Utility;

namespace DTH.API.Services
{
    public class CreateHomeProjectService
    {
        private readonly ILogger<CreateHomeProjectService> _logger;
        private readonly HomeProjectDbContext _context;
        private readonly GetHomeProjectByProjectId _getHomeProjectByProjectId;
        public CreateHomeProjectService(ILogger<CreateHomeProjectService> logger, 
            HomeProjectDbContext context, 
            GetHomeProjectByProjectId getHomeProjectByProjectId)
        {
            _logger = logger;
            _context = context;
            _getHomeProjectByProjectId = getHomeProjectByProjectId;
        }

        /// <summary>
        /// Creates a new HomeProject record in the database using a HomeProjectDbContext
        /// </summary>
        /// <param name="homeProject">HomeProject</param>
        /// <returns>HomeProject</returns>
        /// <exception cref="ValidationFailedException">Thrown when HomeProject is not valid</exception>
        /// <exception cref="AlreadyExistsException">Thrown when HomeProject with the same ProjectId already exists</exception>
        public HomeProject CreateHomeProject(HomeProject homeProject)
        {
            try
            {
                if (!homeProject.IsValid())
                {
                    throw new ValidationFailedException("Home Project is not valid.", "004");
                }
                if (homeProject.ProjectId == null || homeProject.ProjectId.Length == 0)
                {
                    homeProject.ProjectId = homeProject.CreateHomeProjectId();
                }
                homeProject.RemoveWhitespaceFromProjectId();
                if (homeProject.ProjectStatus == null)
                {
                    homeProject.ProjectStatus = ProjectStatus.NotStarted;
                }
                if(_getHomeProjectByProjectId.GetHomeProject(homeProject.ProjectId) != null)
                {
                    throw new AlreadyExistsException($"ProjectId {homeProject.ProjectId} already exists.", "014");
                }
                homeProject.ClientStanding = ClientStanding.Good;
                _context.Add(homeProject);
                _context.SaveChanges();
                return homeProject;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Exception ocurred in CreateHomeProjectService.CreateHomeProject {ex.Message}");
                throw;
            }
        }
    }
}
