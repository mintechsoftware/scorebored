using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scorebored.API.Models;
using Scorebored.API.Requests;
using Scorebored.API.Responses;
using Scorebored.API.Services;

namespace Scorebored.API.Controllers
{
    [Authorize]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("/users/authenticate")]
        public IActionResult Authenticate([FromBody]UserRequest request)
        {
            (User user, string accessToken) = _userService.Authenticate(request.Email, request.Password);

            if (user == null || accessToken == null)
                return Unauthorized();

            return Ok(new AuthenticateUserResponse
            {
                UserId = user.UserId,
                Email = user.Email,
                Name = user.Name,
                AccessToken = accessToken,
                CurrentGroupId = user.CurrentGroupId
            });
        }

        [AllowAnonymous]
        [HttpPost("users/register")]
        public ActionResult<UserResponse> Register([FromBody]UserRequest request)
        {
            (User user, string accessToken) = _userService.Register(request.Email, request.Password);

            if (user == null)
            {
                return BadRequest();
            }

            if (string.IsNullOrEmpty(accessToken))
            {
                return Conflict();
            }

            return Ok(new UserResponse
            {
                UserId = user.UserId,
                Email = user.Email,
                AccessToken = accessToken,
                CurrentGroupId = user.CurrentGroupId
            });
        }

        [HttpGet("user/{userId}")]
        public ActionResult<UserResponse> GetUserData(long userId)
        {
            var user = _userService.GetUser(userId);
            return Ok(new UserResponse
            {
                UserId = user.UserId,
                Email = user.Email,
                CurrentGroupId = user.CurrentGroupId
            });
        }
    }
}
