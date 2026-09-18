using GameStore.Api.Models;

namespace GameStore.Api.Services
{
    public interface IGameService
    {
        IEnumerable<Game> GetAllGames();

        Task<Game?> TryGetGameById(int id);

        Task<Game> CreateGame(CreateGameRequest req);

        Task<bool> UpdateGame(int gameId, UpdateGameRequest req);

        Task<Game?> TryDeleteGame(int gameId);
    }
}
