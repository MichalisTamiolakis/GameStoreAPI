using Microsoft.AspNetCore.Mvc;
using GameStore.Api.Models;
using System.Collections.Concurrent;


namespace GameStore.Api.Controllers
{
    [ApiController]
    [Route("api/games")]
    public class GamesController : ControllerBase
    {
        int currId = 0;

        private readonly ConcurrentDictionary<int, Game> _games = new ();

        public record GameDTO(string Name, decimal Price, string Genre);

        public GamesController() { }

        [HttpGet]
        public ActionResult<IEnumerable<Game>> GetAllGames()
        {
            return Ok(_games.Values);
        }

        [HttpGet("{id}")]
        public ActionResult<Game> GetGameById([FromRoute] int id)
        {
            if (_games.TryGetValue(id, out var game))
            {
                return Ok(game);
            }

            return NotFound();
        }

        [HttpPost]
        public ActionResult CreateGame([FromBody] GameDTO reqParams)
        {
            int generatedId = Interlocked.Increment(ref currId);

            Game game = new Game { Id = generatedId, Name = reqParams.Name, Price = reqParams.Price, Genre = reqParams.Genre };

            _games[game.Id] = game;

            return CreatedAtAction(nameof(GetGameById),
                new { id = game.Id },
                game);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateGame([FromRoute] int id, [FromBody] GameDTO reqParams)
        {
            if(_games.TryGetValue(id, out var game))
            {
                game.Price = reqParams.Price;
                game.Name = reqParams.Name;
                game.Genre = reqParams.Genre;

                return Ok();
            }


            return NotFound();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteGame([FromRoute] int id)
        {
            if(!_games.TryRemove(id, out _))
            {
                return NotFound();
            }

            return NoContent();
        }

    }
}
