using DTH.API.Exceptions;
using DTH.API.Models;
using DTH.API.Services.UserServices;
using DTH.API.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DTH.API.Controllers
{
    [ApiController]
    [Route("dth/users/v1.0")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly CreateUserService _createUserService;

        public UserController(ILogger<UserController> logger, CreateUserService createUserService)
        {
            _logger = logger;
            _createUserService = createUserService;
        }

        [HttpGet]
        [Route("login")]
        public IActionResult Login()
        {
            try
            {
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Exception occurred in UserController.Login {ex.Message}");
                return StatusCode(500, ErrorUtil.CreateError("005", ex.Message));
            }
        }

            [HttpPost]
        [Route("create/user")]
        public IActionResult CreateUser([FromBody] User user)
        {
            try
            {
                return Ok(_createUserService.CreateUser(user).ToUserDTO());
            }
            catch (ValidationFailedException ex)
            {
                return BadRequest(ErrorUtil.CreateError(ex.Code, ex.Message));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Exception occurred in UserController.CreateUser {ex.Message}");
                return StatusCode(500, ErrorUtil.CreateError("005", ex.Message));
            }
        }
    }
}
