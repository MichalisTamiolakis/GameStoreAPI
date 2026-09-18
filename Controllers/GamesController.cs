using Microsoft.AspNetCore.Mvc;
using GameStore.Api.Models;
using GameStore.Api.Services;

namespace GameStore.Api.Controllers
{
    [ApiController]
    [Route("api/games")]
    public class GamesController : ControllerBase
    {
        private IGameService _gameService;

        public GamesController(IGameService gameService) 
        {
            _gameService = gameService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Game>> GetAllGames()
        {
            return Ok(_gameService.GetAllGames());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Game>> GetGameById([FromRoute] int id)
        {
            var g = await _gameService.TryGetGameById(id);

            if (g != null)
            {
                return Ok(g);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult> CreateGame([FromBody] CreateGameRequest reqParams)
        {
            var g = await _gameService.CreateGame(reqParams);

            return CreatedAtAction(nameof(GetGameById),
                new { id = g.Id },
                g);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateGame([FromRoute] int id, [FromBody] UpdateGameRequest reqParams)
        {
            if(await _gameService.UpdateGame(id, reqParams))
            {
                return Ok();
            }

            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteGame([FromRoute] int id)
        {
            var g = await _gameService.TryDeleteGame(id);

            if(g != null)
            {
                return NoContent();
            }
            
            return NotFound();
        }

    }
}
