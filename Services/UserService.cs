using GameStore.Api.Data;
using GameStore.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Services
{
    public class UserService : IUserService
    {
        private readonly GameStoreContext _db;

        public UserService(GameStoreContext db)
        {
            this._db = db;
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _db.Users.ToArrayAsync();
        }


        public async Task<User?> TryGetUserById(int id)
        {
            return await _db.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User> CreateUser(CreateUserRequest req)
        {
            var u = new User { Name = req.Name, Surname = req.Surname, DisplayName = req.DisplayName, Email = req.Email};

            await _db.AddAsync(u);

            await _db.SaveChangesAsync();

            return u;
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

        public async Task<User?> TryDeleteUser(int userId)
        {
            var userToBeDeleted = await _db.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if (userToBeDeleted == null)
                return null;

            _db.Users.Remove(userToBeDeleted);

            await _db.SaveChangesAsync();

            return userToBeDeleted;
        }
    }
}
