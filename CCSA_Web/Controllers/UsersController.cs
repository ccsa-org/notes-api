using CCSANoteApp.Domain;
using CCSANoteApp.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace CCSA_Web.Controllers
{
    [ApiController]
    [Route("users")]
    public class UsersController : ControllerBase
    {
        public IUserService UserService { get; }
        public UsersController(IUserService databaseService)
        {
            UserService = databaseService;
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
