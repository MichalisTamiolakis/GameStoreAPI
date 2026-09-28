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
        public async Task<ActionResult<IEnumerable<UserResponse>>> GetAllUsers()
        {
            return Ok(await _userService.GetAllUsers());
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<UserResponse>> GetUserById([FromRoute] int id)
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

            if(u == null)
            {
                ModelState.AddModelError("Email", "Email already exists");
                return Conflict(ModelState);
            }

            return CreatedAtAction(nameof(GetUserById),
                new { id = u.Id },
                u);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateUser([FromRoute] int id, [FromBody] UpdateUserRequest reqParams)
        {
            if (await _userService.UpdateUser(id, reqParams))
            {
                return Ok();
            }

            return NotFound();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<UserResponse?>> DeleteUser([FromRoute] int id)
        {
            var u = await _userService.TryDeleteUser(id);

            if (u != null)
            {
                return NoContent();
            }

            return NotFound();
        }

        // Games of Users
        [HttpPost("{userId:int}/games/{gameId:int}")]
        public async Task<ActionResult> AddGameToUserLibrary([FromRoute] int userId, [FromRoute] int gameId)
        {
            if (await _userService.AddGameToUserLibrary(userId, gameId))
            {
                return Ok();
            }

            return NotFound();
        }

        [HttpGet("{userId:int}/games")]
        public async Task<ActionResult<IEnumerable<GameResponse>>> GetUserLibrary([FromRoute] int userId)
        {
            return Ok(await _userService.GetUserLibrary(userId));
        }
    }
}
