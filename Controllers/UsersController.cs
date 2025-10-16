using Microsoft.AspNetCore.Mvc;
using BangKokBank.Models;
using BangKokBank.Services;

namespace BangKokBank.Controllers
{
    [ApiController]
    [Route("users")]
    [Produces("application/json")] // บังคับ response เป็น JSON
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;

        public UsersController(UserService userService)
        {
            _userService = userService;
        }

        // GET /users
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAllAsync();
            return Ok(users); // 200 OK + JSON
        }

        // GET /users/{userid}
        [HttpGet("{userid}")]
        public async Task<IActionResult> GetById(long userid)
        {
            var user = await _userService.GetByIdAsync(userid);
            if (user == null)
                return NotFound(new { message = "User not found" }); // 404 + JSON

            return Ok(user); // 200 OK + JSON
        }

        // POST /users
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] User user)
        {
            if (string.IsNullOrWhiteSpace(user.Name) ||
                string.IsNullOrWhiteSpace(user.Username) ||
                string.IsNullOrWhiteSpace(user.Email))
            {
                return BadRequest(new { message = "Name, Username, and Email are required" }); // 400 + JSON
            }

            var createdUser = await _userService.CreateAsync(user);
            return CreatedAtAction(
                nameof(GetById),
                new { userid = createdUser.Id },
                createdUser // 201 Created + JSON
            );
        }

        // PUT /users/{userid}
        [HttpPut("{userid}")]
        public async Task<IActionResult> Update(long userid, [FromBody] User user)
        {
            if (string.IsNullOrWhiteSpace(user.Name) ||
                string.IsNullOrWhiteSpace(user.Username) ||
                string.IsNullOrWhiteSpace(user.Email))
            {
                return BadRequest(new { message = "Name, Username, and Email are required" }); // 400 + JSON
            }

            var updated = await _userService.UpdateAsync(userid, user);
            if (!updated)
                return NotFound(new { message = "User not found" }); // 404 + JSON

            return Ok(user); // 200 OK + JSON
        }

        // DELETE /users/{userid}
        [HttpDelete("{userid}")]
        public async Task<IActionResult> Delete(long userid)
        {
            var deleted = await _userService.DeleteAsync(userid);
            if (!deleted)
                return NotFound(new { message = "User not found" }); // 404 + JSON

            return NoContent(); // 204 No Content
        }
    }
}
