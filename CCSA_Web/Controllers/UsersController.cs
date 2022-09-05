using CCSANoteApp.Auth;
using CCSANoteApp.Domain;
using CCSANoteApp.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CCSA_Web.Controllers
{
    [Authorize]
    [ApiController]
    [Route("users")]
    public class UsersController : ControllerBase
    {
        public IUserService UserService { get; }
        public IAuthService AuthService { get; }
        public UsersController(IUserService databaseService, IAuthService authService)
        {
            UserService = databaseService;
            AuthService = authService;
        }

        [HttpGet("identity")]
        public IActionResult GetUserClaims()
        {
            try
            {
                return Ok(AuthService.GetUserIdentity());
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost("refresh-token")]
        public IActionResult GetUser([FromBody] string refreshToken)
        {

            try
            {
                var model = AuthService.GetTokenModel(refreshToken);
                if (model != null)
                {
                    return Ok(model);
                }
                else
                {
                    return Unauthorized("Invalid Refresh Token");
                }
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpGet("login")]
        public IActionResult GetUser(string email, string password)
        {

            try
            {
                var model = UserService.FetchUserByLogin(email, password);
                if (model != null)
                {
                    var tokenModel = AuthService.GetTokenModel(new UserIdentityModel
                    {
                        Email = email,
                        Identifier = model.Id.ToString(),
                        Name = model.Username
                    });
                    return Ok(tokenModel);
                }
                else
                {
                    return BadRequest("Invalid email was entered");
                }
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult CreateUser(string username, string email, string password)
        {
            UserService.CreateUser(username,  email, password);
            return Ok("User Created Successfully");
        }
        [HttpPost("byUser")]
        public IActionResult CreateUser([FromBody] UserDto user)
        {
            UserService.CreateUser(user.Username, user.Email, user.Password);
            return Ok("User Created Successfully");
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(Guid id)
        {
            UserService.DeleteUser(id);
            return Ok("User Deleted Successfully");
        }
        [HttpGet("user/{userId}")]
        public IActionResult GetUser(Guid userId)
        {
            
            try
            {
                var user = UserService.GetUser(userId);
                return Ok(user);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetUsers()
        {
            return Ok(UserService.GetUsers());
        }
        [HttpPut("updateemail")]
        public IActionResult UpdateEmail(Guid id, string email)
        {
            UserService.UpdateUserEmail(id, email);
            return Ok("Updated Successfully");
        }
        [HttpPut("updatename")]
        public IActionResult UpdateName(Guid id, string name)
        {
            UserService.UpdateUserName(id, name);
            return Ok("Updated Successfully");
        }
    }
}
