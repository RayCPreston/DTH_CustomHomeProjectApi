using DTH.API.Models;

namespace DTH.API.Services.HomeProjectServices
{
    public class HomeProjectsService
    {
        private readonly CreateHomeProjectService _createHomeProjectService;
        private readonly DeleteHomeProjectService _deleteHomeProjectService;
        private readonly GetAllHomeProjects _getAllHomeProjectsService;
        private readonly GetHomeProjectByProjectId _getHomeProjectByProjectId;
        private readonly GetHomeProjectsByClientNameService _getHomeProjectsByClientNameService;
        private readonly GetHomeProjectsByProjectNameService _getHomeProjectsByProjectNameService;
        private readonly GetHomeProjectsByStatusService _getHomeProjectsByStatusService;
        private readonly UpdateHomeProjectService _updateHomeProjectService;

        public HomeProjectsService(
            CreateHomeProjectService createHomeProjectService,
            DeleteHomeProjectService deleteHomeProjectService,
            GetAllHomeProjects getAllHomeProjectsService,
            GetHomeProjectByProjectId getHomeProjectByProjectId,
            GetHomeProjectsByClientNameService getHomeProjectsByClientNameService,
            GetHomeProjectsByProjectNameService getHomeProjectsByProjectNameService,
            GetHomeProjectsByStatusService getHomeProjectsByStatusService,
            UpdateHomeProjectService updateHomeProjectService)
        {
            _createHomeProjectService = createHomeProjectService;
            _deleteHomeProjectService = deleteHomeProjectService;
            _getAllHomeProjectsService = getAllHomeProjectsService;
            _getHomeProjectByProjectId = getHomeProjectByProjectId;
            _getHomeProjectsByClientNameService = getHomeProjectsByClientNameService;
            _getHomeProjectsByProjectNameService = getHomeProjectsByProjectNameService;
            _getHomeProjectsByStatusService = getHomeProjectsByStatusService;
            _updateHomeProjectService = updateHomeProjectService;
        }

        /// <summary>
        /// Creates a new HomeProject record in the database using a HomeProjectDbContext
        /// </summary>
        /// <param name="homeProject">HomeProject</param>
        /// <returns>HomeProject</returns>
        /// <exception cref="ValidationFailedException">Thrown when HomeProject is not valid</exception>
        /// <exception cref="AlreadyExistsException">Thrown when HomeProject with the same ProjectId already exists</exception>
        public HomeProject CreateHomeProject(HomeProject homeProject) => _createHomeProjectService.CreateHomeProject(homeProject);

        /// <summary>
        /// Deletes a HomeProject record from the DB using a projectId as the lookup.
        /// </summary>
        /// <param name="projectId">string</param>
        /// <returns>HomeProject</returns>
        /// <exception cref="NotFoundException">Thrown when HomeProject is not found</exception>
        public HomeProject DeleteHomeProject(string projectId) => _deleteHomeProjectService.DeleteHomeProject(projectId);

        /// <summary>
        /// Returns a List of all HomeProjects.
        /// </summary>
        /// <returns>List&lt;HomeProject&gt;</returns>
        /// <exception cref="NotFoundException">Thrown when no HomeProjects are found</exception>
        public List<HomeProject> GetAllHomeProjects() => _getAllHomeProjectsService.GetHomeProjects();

        /// <summary>
        /// Returns a HomeProject with a ProjectId matching the passed parameter.
        /// Returns null if no match is found.
        /// </summary>
        /// <param name="projectId">string</param>
        /// <returns>HomeProject</returns>
        public HomeProject? GetHomeProjectByProjectId(string projectId) => _getHomeProjectByProjectId.GetHomeProject(projectId);

        /// <summary>
        /// Returns a List of HomeProjects where the ClientName matches passed parameter.
        /// Matching is case-insensitive.
        /// </summary>
        /// <param name="clientName">string</param>
        /// <returns>List</returns>
        /// <exception cref="NotFoundException">Thrown when no HomeProjects are found</exception>
        public List<HomeProject> GetHomeProjectsByClientName(string clientName) => _getHomeProjectsByClientNameService.GetHomeProjects(clientName);

        /// <summary>
        /// Returns a List of HomeProjects where the ProjectName matches passed parameter.
        /// Matching is case-insensitive.
        /// </summary>
        /// <param name="projectName">string</param>
        /// <returns>List</returns>
        /// <exception cref="NotFoundException">Thrown when no HomeProjects are found</exception>
        public List<HomeProject> GetHomeProjectsByProjectName(string projectName) => _getHomeProjectsByProjectNameService.GetHomeProjects(projectName);

        /// <summary>
        /// Returns a List of HomeProjects where the ProjectStatus matches passed parameter.
        /// </summary>
        /// <param name="projectStatus">ProjectStatus enum</param>
        /// <returns>List&lt;HomeProject&gt;</returns>
        /// <exception cref="NotFoundException">Thrown when no HomeProjects are found</exception>
        public List<HomeProject> GetHomeProjectsByStatus(ProjectStatus status) => _getHomeProjectsByStatusService.GetHomeProjects(status);

        /// <summary>
        /// Updates the HomeProject record in the DB with a matching ProjectId using the passed HomeProject object.
        /// </summary>
        /// <param name="homeProject">HomeProject</param>
        /// <returns>HomeProject</returns>
        /// <exception cref="ValidationFailedException">Thrown when HomeProject is not valid</exception>
        /// <exception cref="AlreadyExistsException">Thrown when HomeProject with the same ProjectId already exists</exception>
        public HomeProject UpdateHomeProject(HomeProject homeProject) => _updateHomeProjectService.UpdateHomeProject(homeProject);
    }
}