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

        public async Task<IEnumerable<Game>> GetAllGames()
        {
            return await _db.Games.ToArrayAsync();
        }


        public async Task<Game?> TryGetGameById(int id)
        {
            return await _db.Games.FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task<Game> CreateGame(CreateGameRequest req)
        {
            var g = new Game { Name = req.Name, Price = req.Price, Genre = req.Genre };

            await _db.AddAsync(g);
            
            await _db.SaveChangesAsync();

            return g;
        }

        public async Task<bool> UpdateGame(int gameId, UpdateGameRequest req)
        {
            var game = await _db.Games.FirstOrDefaultAsync(g => g.Id == gameId);

            if(game == null)
            {
                return false;
            }

            // Update only given fields.

            if(req.Name is not null)
            {
                game.Name = req.Name;
            }

            if(req.Price is not null)
            {
                game.Price = req.Price.Value;
            }

            if (req.Genre is not null)
            {
                game.Genre = req.Genre;
            }

            await _db.SaveChangesAsync();

            return true;
        }

        public  async Task<Game?> TryDeleteGame(int gameId)
        {
            var gameToBeDeleted = await _db.Games.FirstOrDefaultAsync(g => g.Id == gameId);

            if (gameToBeDeleted == null)
                return null;

            _db.Games.Remove(gameToBeDeleted);

            await _db.SaveChangesAsync();

            return gameToBeDeleted;
        }
    }
}
