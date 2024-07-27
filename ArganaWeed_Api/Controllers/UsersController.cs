using ArganaWeedApp.Data;
using ArganaWeedApp.DTOs;
using ArganaWeedApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ArganaWeedApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ArganaWeedDbContext _context;

        public UsersController(ArganaWeedDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<UsersResponse> GetUsers()
        {
            return await _context.GetAllUsersAsync();
        }

        [HttpGet("{id}")]
        public async Task<UsersResponse> GetUser(int id)
        {
            return await _context.GetUserByIdAsync(id);
        }

        [HttpGet("search/{searchString}")]
        public async Task<ActionResult<UsersResponse>> SearchUsers(string searchString)
        {
            var usersResponse = await _context.SearchUsersAsync(searchString);
            return Ok(usersResponse);
        }





        [HttpPost]
        public async Task<ActionResult<string>> PostUser(User user)
        {
            var message = await _context.AddUserAsync(user.UserName, user.UserEmail, user.HashedPassword, user.IsAdministrator, user.IsOwner, user.IsAgent, user.IsViewer, user.IsActive);
            return Ok(message);
        }

        [HttpPut("roles/{id}")]
        public async Task<ActionResult<string>> UpdateUserRoles(int id, [FromBody] UserRoleUpdateModel userRoleUpdate)
        {
            var message = await _context.UpdateUserRolesAsync(id, userRoleUpdate.IsAdministrator, userRoleUpdate.IsOwner, userRoleUpdate.IsAgent, userRoleUpdate.IsViewer);
            return Ok(message);
        }

        [HttpPut("suspend/{id}")]
        public async Task<ActionResult<string>> SuspendUser(int id)
        {
            var message = await _context.SuspendUserAsync(id);
            return Ok(message);
        }

        [HttpPut("activate/{id}")]
        public async Task<ActionResult<string>> ActivateUser(int id)
        {
            var message = await _context.ActivateUserAsync(id);
            return Ok(message);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> DeleteUser(int id)
        {
            var message = await _context.DeleteUserByIdAsync(id);
            return Ok(message);
        }

        [HttpDelete]
        public async Task<ActionResult<string>> DeleteAllUsers()
        {
            var message = await _context.DeleteAllUsersAsync();
            return Ok(message);
        }

        [HttpPut("changepassword")]
        public async Task<ActionResult<string>> ChangePassword([FromBody] ChangePasswordModel model)
        {
            var message = await _context.ChangePasswordAsync(model.UserId, model.CurrentPassword, model.NewPassword, model.ConfirmPassword, model.Role);
            return Ok(message);
        }
    }
    public class ChangePasswordModel
    {
        public int UserId { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
        public string Role { get; set; }
    }
}
