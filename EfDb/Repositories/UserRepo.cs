
using EfDb.Models;
using Microsoft.EntityFrameworkCore;

namespace EfDb.Repositories
{
    public class UserRepo
    {
        private readonly AppEfContext db;
        public UserRepo()
        {
            db = new AppEfContext();
        }
        public async Task CreateUserAsync(CreateUserModel model)
        {
            var user = new User()
            {
                FirstName = model.FirstName,
                SecondName = model.SecondName,
                DateOfBirth = model.DateOfBirth,
            };
            await db.Users.AddAsync(user);
            await db.SaveChangesAsync();
        }

        public async Task<List<User>> ReadUsersAsync()
        {
            return await db.Users.Include(u => u.UserProfile).Include(u => u.Teams).Include(u => u.Tasks).ToListAsync();
        }

        public async Task UpdateUserAsync(int uId)
        {
            var user = await db.Users.FirstOrDefaultAsync(u => u.Id == uId);
            if (user != null)
            {
                //Изменения
                await db.SaveChangesAsync();
            }
        }

        public async Task DeleteUserAsync(int uId)
        {
            var user = await db.Users.FirstOrDefaultAsync(u => u.Id == uId);
            if (user != null)
            {
                db.Users.Remove(user);
                await db.SaveChangesAsync();
            }
        }
    }
}
