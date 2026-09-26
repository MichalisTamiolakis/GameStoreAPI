using GameStore.Api.Models;

namespace GameStore.Api.Services
{
    public interface IGameService
    {
        Task<IEnumerable<GameResponse>> GetAllGames();

        Task<GameResponse?> TryGetGameById(int id);

        Task<GameResponse> CreateGame(CreateGameRequest req);

        Task<bool> UpdateGame(int gameId, UpdateGameRequest req);

        Task<GameResponse?> TryDeleteGame(int gameId);
    }
}
