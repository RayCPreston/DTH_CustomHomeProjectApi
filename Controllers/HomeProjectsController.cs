using DTH.API.Exceptions;
using DTH.API.Models;
using DTH.API.Services.HomeProjectServices;
using DTH.API.Utility;
using Microsoft.AspNetCore.Mvc;

namespace DTH.API.Controllers
{
    [ApiController]
    [Route("dth/homeprojects/v1.0")]
    public class HomeProjectsController : ControllerBase
    {
        private readonly ILogger<HomeProjectsController> _logger;
        private readonly HomeProjectsService _homeProjectsService;

        public HomeProjectsController(ILogger<HomeProjectsController> logger, HomeProjectsService homeProjectsService)
        {
            _logger = logger;
            _homeProjectsService = homeProjectsService;
        }

        [HttpPost]
        [Route("create/homeproject")]
        public IActionResult CreateHomeProject([FromBody] HomeProjectDTO homeProjectDTO)
        {
            try
            {
                HomeProject homeProject = homeProjectDTO.ToHomeProject();
                return Ok(_homeProjectsService.CreateHomeProject(homeProject).ToHomeProjectDTO());
            }
            catch (ValidationFailedException ex)
            {
                return BadRequest(ErrorUtil.CreateError(ex.Code, ex.Message));
            }
            catch (AlreadyExistsException ex)
            {
                return BadRequest(ErrorUtil.CreateError(ex.Code, ex.Message));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Exception occurred in HomeProjectsController.CreateHomeProject {ex.Message}");
                return StatusCode(500, ErrorUtil.CreateError("005", ex.Message));
            }
        }

        [HttpDelete]
        [Route("delete/homeproject/{projectId}")]
        public IActionResult DeleteHomeProject(string projectId)
        {
            try
            {
                return Ok(_homeProjectsService.DeleteHomeProject(projectId).ToHomeProjectDTO());
            }
            catch (NotFoundException ex)
            {
                return BadRequest(ErrorUtil.CreateError(ex.Code, ex.Message));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Exception occurred in HomeProjectsController.DeleteHomeProject {ex.Message}");
                return StatusCode(500, ErrorUtil.CreateError("005", ex.Message));
            }
        }

        [HttpGet]
        [Route("get/homeprojects")]
        public IActionResult GetAllHomeProjects()
        {
            try
            {
                return Ok(_homeProjectsService.GetAllHomeProjects().ToHomeProjectDTOList());
            }
            catch (NotFoundException)
            {
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Exception occurred in HomeProjectsController.GetAllHomeProjects {ex.Message}");
                return StatusCode(500, ErrorUtil.CreateError("005", ex.Message));
            }
        }

        [HttpGet]
        [Route("get/homeproject/projectid/{projectId}")]
        public IActionResult GetHomeProject(string projectId)
        {
            try
            {
                HomeProject? homeProject = _homeProjectsService.GetHomeProjectByProjectId(projectId);
                if (homeProject == null)
                {
                    return NoContent();
                }
                return Ok(homeProject);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Exception occurred in HomeProjectsController.GetHomeProjectByProjectId {ex.Message}");
                return StatusCode(500, ErrorUtil.CreateError("005", ex.Message));
            }
        }


        [HttpGet]
        [Route("get/homeprojects/clientname/{clientName}")]
        public IActionResult GetHomeProjectsByClientName(string clientName)
        {
            try
            {
                return Ok(_homeProjectsService.GetHomeProjectsByClientName(clientName).ToHomeProjectDTOList());
            }
            catch (NotFoundException ex)
            {
                return NotFound(ErrorUtil.CreateError(ex.Code, ex.Message));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Exception occurred in HomeProjectsController.GetHomeProjectsByClientName {ex.Message}");
                return StatusCode(500, ErrorUtil.CreateError("005", ex.Message));
            }
        }

        [HttpGet]
        [Route("get/homeprojects/projectname/{projectName}")]
        public IActionResult GetHomeProjectsByProjectName(string projectName)
        {
            try
            {
                return Ok(_homeProjectsService.GetHomeProjectsByProjectName(projectName).ToHomeProjectDTOList());
            }
            catch (NotFoundException ex)
            {
                return NotFound(ErrorUtil.CreateError(ex.Code, ex.Message));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Exception occurred in HomeProjectsController.GetHomeProjectsByProjectName {ex.Message}");
                return StatusCode(500, ErrorUtil.CreateError("005", ex.Message));
            }
        }

        [HttpGet]
        [Route("get/homeprojects/status/{status}")]
        public IActionResult GetHomeProjectsByStatus(string status)
        {
            try
            {
                ProjectStatus projectStatus = Enum.Parse<ProjectStatus>(status, true);
                return Ok(_homeProjectsService.GetHomeProjectsByStatus(projectStatus).ToHomeProjectDTOList());
            }
            catch (NotFoundException ex)
            {
                return NotFound(ErrorUtil.CreateError(ex.Code, ex.Message));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Exception occurred in HomeProjectsController.GetHomeProjectsByStatus {ex.Message}");
                return StatusCode(500, ErrorUtil.CreateError("005", ex.Message));
            }
        }

        [HttpPut]
        [Route("update/homeproject")]
        public IActionResult UpdateHomeProject([FromBody] HomeProjectDTO homeProjectDTO)
        {
            try
            {
                HomeProject homeProject = homeProjectDTO.ToHomeProject();
                return Ok(_homeProjectsService.UpdateHomeProject(homeProject).ToHomeProjectDTO());
            }
            catch (ValidationFailedException ex)
            {
                return BadRequest(ErrorUtil.CreateError(ex.Code, ex.Message));
            }
            catch (NotFoundException ex)
            {
                return BadRequest(ErrorUtil.CreateError(ex.Code, ex.Message));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Exception occurred in HomeProjectsController.UpdateHomeProject {ex.Message}");
                return StatusCode(500, ErrorUtil.CreateError("005", ex.Message));
            }
        }
    }
}
