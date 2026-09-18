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

        public IEnumerable<Game> GetAllGames()
        {
            return _db.Games;
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

            game.Price = req.Price;
            game.Genre = req.Genre;
            game.Name = req.Name;

            await _db.SaveChangesAsync();

            return true;
        }

        public  async Task<Game?> TryDeleteGame(int gameId)
        {
            var gameToBeDeleted = await _db.Games.FirstOrDefaultAsync(g => g.Id == gameId);

            if (gameToBeDeleted == null)
                return gameToBeDeleted;

            _db.Games.Remove(gameToBeDeleted);

            await _db.SaveChangesAsync();

            return gameToBeDeleted;
        }
    }
}
