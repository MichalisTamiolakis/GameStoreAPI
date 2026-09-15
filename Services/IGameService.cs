using GameStore.Api.Models;

namespace GameStore.Api.Services
{
    public interface IGameService
    {
        IEnumerable<Game> GetAllGames();

        bool TryGetGameById(int id, out Game game);

        Game CreateGame(CreateGameRequest req);

        bool UpdateGame(int gameId, UpdateGameRequest req);

        bool TryDeleteGame(int gameId, out Game g);
    }
}
