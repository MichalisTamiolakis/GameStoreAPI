using GameStore.Api.Models;

namespace GameStore.Api.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserResponse>> GetAllUsers();

        Task<UserResponse?> TryGetUserById(int id);

        Task<UserResponse?> CreateUser(CreateUserRequest req);

        Task<bool> UpdateUser(int userId, UpdateUserRequest req);

        Task<UserResponse?> TryDeleteUser(int userId);

        // User Library of Games
        Task<bool> AddGameToUserLibrary(int userId, int gameId);

        Task<IEnumerable<GameResponse>> GetUserLibrary(int userId);
    }
}
