using GameStore.Api.Data;
using GameStore.Api.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Services
{
    public class UserService : IUserService
    {
        private readonly GameStoreContext _db;

        public UserService(GameStoreContext db, IGameService gameService)
        {
            this._db = db;
        }

        public async Task<IEnumerable<UserResponse>> GetAllUsers()
        {
            return await _db.Users.Select(u => new UserResponse(u.Id, u.Name, u.Surname, u.DisplayName, u.Email)).ToArrayAsync();
        }


        public async Task<UserResponse?> TryGetUserById(int id)
        {
            return await _db.Users.Select(u => new UserResponse(u.Id, u.Name, u.Surname, u.DisplayName, u.Email)).FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<UserResponse> CreateUser(CreateUserRequest req)
        {
            var u = new User { Name = req.Name, Surname = req.Surname, DisplayName = req.DisplayName, Email = req.Email};

            await _db.AddAsync(u);

            await _db.SaveChangesAsync();

            return new UserResponse(u.Id, u.Name, u.Surname, u.DisplayName, u.Email);
        }

        public async Task<bool> UpdateUser(int userId, UpdateUserRequest req)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                return false;
            }

            // Update only given fields.
            if (req.Email is not null)
            {
                user.Email = req.Email;
            }

            if (req.DisplayName is not null)
            {
                user.DisplayName = req.DisplayName;
            }

            await _db.SaveChangesAsync();

            return true;
        }

        public async Task<UserResponse?> TryDeleteUser(int userId)
        {
            var userToBeDeleted = await _db.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if (userToBeDeleted == null)
                return null;

            _db.Users.Remove(userToBeDeleted);

            await _db.SaveChangesAsync();

            return new UserResponse(userToBeDeleted.Id, userToBeDeleted.Name, userToBeDeleted.Surname, userToBeDeleted.DisplayName, userToBeDeleted.Email);
        }

        public async Task<bool> AddGameToUserLibrary(int userId, int gameId)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Id == userId);
            var game = await _db.Games.FirstOrDefaultAsync(g => g.Id == gameId);

            if (user != null && game != null)
            {
                // Add the game, if it already exists, we will catch the exception
                user.Games.Add(game);

                try
                {
                    await _db.SaveChangesAsync();
                }
                catch(DbUpdateException e)
                {
                    // Unique constraint/index violation means this user already owns the game, so we return true again.
                    if(e.InnerException is SqlException sql && (sql.Number == 2627 || sql.Number == 2601))
                    {
                        return true;
                    }

                    throw;
                }

                return true;
            }

            return false;
            
        }

        public async Task<IEnumerable<GameResponse>> GetUserLibrary(int userId)
        {
            return await _db.Users.Where(u => u.Id == userId).SelectMany(u => u.Games).Select(g => new GameResponse(g.Id, g.Name, g.Price, g.Genre)).ToArrayAsync();
        }
    }
}
