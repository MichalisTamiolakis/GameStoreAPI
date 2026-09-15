using GameStore.Api.Data;
using GameStore.Api.Models;

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


        public bool TryGetGameById(int id, out Game game)
        {
            game = _db.Games.FirstOrDefault(g => g.Id == id);

            return game != null;
        }

        public Game CreateGame(CreateGameRequest req)
        {
            var g = new Game { Name = req.Name, Price = req.Price, Genre = req.Genre };

            _db.Add(g);
            _db.SaveChanges();

            return g;
        }

        public bool UpdateGame(int gameId, UpdateGameRequest req)
        {
            var game = _db.Games.FirstOrDefault(g => g.Id == gameId);

            if(game == null)
            {
                return false;
            }

            game.Price = req.Price;
            game.Genre = req.Genre;
            game.Name = req.Name;

            _db.SaveChanges();

            return true;
        }

        public  bool TryDeleteGame(int gameId, out Game g)
        {
            g = _db.Games.FirstOrDefault(g => g.Id == gameId);

            if (g == null)
                return false;

            _db.Games.Remove(g);

            _db.SaveChanges();

            return true;
        }
    }
}
