using GameStore.Api.Models;
using GameStore.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Api.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Game>>> GetAllUsers()
        {
            return Ok(await _userService.GetAllUsers());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUserById([FromRoute] int id)
        {
            var u = await _userService.TryGetUserById(id);

            if (u != null)
            {
                return Ok(u);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult> CreateUser([FromBody] CreateUserRequest reqParams)
        {
            var u = await _userService.CreateUser(reqParams);

            return CreatedAtAction(nameof(GetUserById),
                new { id = u.Id },
                u);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateUser([FromRoute] int id, [FromBody] UpdateUserRequest reqParams)
        {
            if (await _userService.UpdateUser(id, reqParams))
            {
                return Ok();
            }

            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteGame([FromRoute] int id)
        {
            var u = await _userService.TryDeleteUser(id);

            if (u != null)
            {
                return NoContent();
            }

            return NotFound();
        }
    }
}
