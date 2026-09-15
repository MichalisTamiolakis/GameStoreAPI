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
        public ActionResult<Game> GetGameById([FromRoute] int id)
        {
            if (_gameService.TryGetGameById(id, out var game))
            {
                return Ok(game);
            }

            return NotFound();
        }

        [HttpPost]
        public ActionResult CreateGame([FromBody] CreateGameRequest reqParams)
        {
            var g = _gameService.CreateGame(reqParams);

            return CreatedAtAction(nameof(GetGameById),
                new { id = g.Id },
                g);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateGame([FromRoute] int id, [FromBody] UpdateGameRequest reqParams)
        {
            if(_gameService.UpdateGame(id, reqParams))
            {
                return Ok();
            }

            return NotFound();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteGame([FromRoute] int id)
        {
            if(_gameService.TryDeleteGame(id, out _))
            {
                return NoContent();
            }
            
            
            return NotFound();
        }

    }
}
