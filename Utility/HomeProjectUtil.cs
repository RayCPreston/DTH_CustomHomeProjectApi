using DTH.API.Models;

namespace DTH.API.Utility
{
    public static class HomeProjectUtil
    {
        /// <summary>
        /// Converts a HomeProjectDTO to a HomeProject
        /// </summary>
        /// <param name="homeProjectDto">HomeProjectDTO</param>
        /// <returns>HomeProject</returns>
        public static HomeProject ToHomeProject(this HomeProjectDTO homeProjectDto)
        {

            return new HomeProject
            {
                Id = new Guid(),
                ProjectId = homeProjectDto.ProjectId,
                ProjectName = homeProjectDto.ProjectName,
                ClientName = homeProjectDto.ClientName,
                StreetAddress = homeProjectDto.StreetAddress,
                StartDate = homeProjectDto.StartDate,
                EstimatedCompletionDate = homeProjectDto.EstimatedCompletionDate,
                ProjectStatus = homeProjectDto.ProjectStatus,
                Budget = homeProjectDto.Budget
            };
        }

        /// <summary>
        /// Converts a HomeProject to a HomeProjectDTO
        /// </summary>
        /// <param name="homeProject">HomeProject</param>
        /// <returns>HomeProjectDTO</returns>
        public static HomeProjectDTO ToHomeProjectDTO(this HomeProject homeProject)
        {
            return new HomeProjectDTO(homeProject.GetHomeProjectId(),
                homeProject.ProjectName,
                homeProject.ClientName,
                homeProject.StreetAddress,
                homeProject.StartDate,
                homeProject.EstimatedCompletionDate,
                homeProject.ProjectStatus,
                homeProject.Budget);
        }

        /// <summary>
        /// Converst a List of HomeProjects to a List of HomeProjectDTOs.
        /// </summary>
        /// <param name="homeProjects">List&lt;HomeProject&gt;</param>
        /// <returns>List&lt;HomeProjectDTO&gt;</returns>
        public static List<HomeProjectDTO> ToHomeProjectDTOList(this List<HomeProject> homeProjects)
        {
            List<HomeProjectDTO> homeProjectDtos = new List<HomeProjectDTO>();
            foreach (HomeProject homeProject in homeProjects)
            {
                homeProjectDtos.Add(homeProject.ToHomeProjectDTO());
            }
            return homeProjectDtos;
        }

        /// <summary>
        /// Creates a HomeProjectId by concatenating the address number and project name.
        /// </summary>
        /// <param name="homeProject">HomeProject</param>
        /// <returns>string</returns>
        public static string CreateHomeProjectId(this HomeProject homeProject)
        {
            string addressNumber = StringUtil.CreateAddressSubstring(homeProject.StreetAddress);
            string projectId = $"{addressNumber}-{homeProject.ProjectName}";
            return StringUtil.RemoveWhitespace(projectId);
        }

        /// <summary>
        /// Removes all whitespace from a HomeProject's ProjectId.
        /// </summary>
        /// <param name="homeProject">homeProject</param>
        public static void RemoveWhitespaceFromProjectId(this HomeProject homeProject)
        {
            if(homeProject.ProjectId == null)
            {
                return;
            }
            homeProject.ProjectId = StringUtil.RemoveWhitespace(homeProject.ProjectId);
        }

        /// <summary>
        /// Gets the HomeProjectId. If the HomeProjectId is null or empty, it is created.
        /// </summary>
        /// <param name="homeProject">HomeProject</param>
        /// <returns>string</returns>
        private static string GetHomeProjectId(this HomeProject homeProject)
        {
            if (homeProject.ProjectId == null || homeProject.ProjectId.Length == 0)
            {
                homeProject.ProjectId = CreateHomeProjectId(homeProject);
            }
            return homeProject.ProjectId;
        }

        /// <summary>
        /// Validates a HomeProject's required properties.
        /// </summary>
        /// <param name="homeProject">HomeProject</param>
        /// <returns>bool</returns>
        public static bool IsValid(this HomeProject homeProject)
        {
            if (homeProject == null)
            {
                return false;
            }
            if (homeProject.ProjectName == null || homeProject.ProjectName.Length == 0)
            {
                return false;
            }
            if (homeProject.ClientName == null || homeProject.ClientName.Length == 0)
            {
                return false;
            }
            if (homeProject.StreetAddress == null || homeProject.StreetAddress.Length == 0)
            {
                return false;
            }
            if (homeProject.Budget <= 0)
            {
                return false;
            }
            return true;
        }
    }
}
