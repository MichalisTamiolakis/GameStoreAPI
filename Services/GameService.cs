using GameStore.Api.Models;
using System.Collections.Concurrent;

namespace GameStore.Api.Services
{
    public class GameService : IGameService
    {
        int currId = 0;

        private readonly ConcurrentDictionary<int, Game> _games = new();


        public IEnumerable<Game> GetAllGames()
        {
            return _games.Values;
        }


        public bool TryGetGameById(int id, out Game game)
        {
            return _games.TryGetValue(id, out game);
        }

        public Game CreateGame(CreateGameRequest req)
        {
            // Generate new id.
            var id = Interlocked.Increment(ref currId);

            var g = new Game { Id = id, Name = req.Name, Price = req.Price, Genre = req.Genre };

            _games[id] = g;

            return g;
        }

        public bool UpdateGame(int gameId, UpdateGameRequest req)
        {
            if(_games.TryGetValue(gameId, out var g))
            {
                g.Name = req.Name;
                g.Price = req.Price;
                g.Genre = req.Genre;
                
                return true;
            }

            return false;
        }

        public  bool TryDeleteGame(int gameId, out Game g)
        {
            if(_games.TryRemove(gameId, out g))
            {
                return true;
            }

            g = null;
            return false;
        }
    }
}
