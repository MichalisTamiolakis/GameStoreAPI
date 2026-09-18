using GameStore.Api.Models;

namespace GameStore.Api.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsers();

        Task<User?> TryGetUserById(int id);

        Task<User> CreateUser(CreateUserRequest req);

        Task<bool> UpdateUser(int userId, UpdateUserRequest req);

        Task<User?> TryDeleteUser(int userId);
    }
}
