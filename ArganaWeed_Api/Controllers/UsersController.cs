using ArganaWeed_Api.Data;
using ArganaWeed_Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace ArganaWeed_Api.Controllers
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
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var users = await _context.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpGet("search/{searchString}")]
        public async Task<ActionResult<IEnumerable<User>>> SearchUsers(string searchString)
        {
            var users = await _context.SearchUsersAsync(searchString);
            return Ok(users);
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
