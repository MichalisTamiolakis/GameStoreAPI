using GameStore.Api.Data;
using GameStore.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Services
{
    public class GameService : IGameService
    {
        private readonly GameStoreContext _db;

        public GameService(GameStoreContext db)
        {
            this._db = db;
        }

        public async Task<IEnumerable<GameResponse>> GetAllGames()
        {
            return await _db.Games.Select(g => new GameResponse(

                g.Id,
                g.Name,
                g.Price,
                g.Genre
            )).ToArrayAsync();
        }


        public async Task<GameResponse?> TryGetGameById(int id)
        {
            return await _db.Games.Where(g => g.Id == id).Select(g => new GameResponse(

                g.Id,
                g.Name,
                g.Price,
                g.Genre
            )).FirstOrDefaultAsync();
        }

        public async Task<GameResponse> CreateGame(CreateGameRequest req)
        {
            var g = new Game { Name = req.Name.Trim(), Price = req.Price.Value, Genre = req.Genre.Trim() };

            await _db.AddAsync(g);
            
            await _db.SaveChangesAsync();

            return new GameResponse(g.Id, g.Name, g.Price, g.Genre);
        }

        public async Task<bool> UpdateGame(int gameId, UpdateGameRequest req)
        {
            var game = await _db.Games.FirstOrDefaultAsync(g => g.Id == gameId);

            if(game == null)
            {
                return false;
            }

            // Update only given fields.

            if(!string.IsNullOrWhiteSpace(req.Name))
            {
                game.Name = req.Name;
            }

            if(req.Price is not null)
            {
                game.Price = req.Price.Value;
            }

            if (!string.IsNullOrWhiteSpace(req.Genre))
            {
                game.Genre = req.Genre;
            }

            await _db.SaveChangesAsync();

            return true;
        }

        public  async Task<GameResponse?> TryDeleteGame(int gameId)
        {
            var gameToBeDeleted = await _db.Games.FirstOrDefaultAsync(g => g.Id == gameId);

            if (gameToBeDeleted == null)
                return null;

            _db.Games.Remove(gameToBeDeleted);

            await _db.SaveChangesAsync();

            return new GameResponse(gameToBeDeleted.Id, gameToBeDeleted.Name, gameToBeDeleted.Price, gameToBeDeleted.Genre);
        }
    }
}
