using EfDb.Models;
using Microsoft.EntityFrameworkCore;

namespace EfDb.Repositories
{
    public class UserProfRepo
    {
        private readonly AppEfContext db;
        public UserProfRepo()
        {
            db = new AppEfContext();
        }
        public async Task CreateProfileAsync(CreateProfileModel model)
        {
            var uProfile = new UserProfile()
            {
                UserId = model.UserId,
                Country = model.Country,
                City = model.City,
                Citizenship = model.Citizenship
            };
           await db.UserProfiles.AddAsync(uProfile);
           await db.SaveChangesAsync();
        }

        public async Task<UserProfile> ReadProfileAsync(int userId)
        {
            return await db.UserProfiles.Include(p => p.User).FirstOrDefaultAsync(p => p.UserId == userId);
        }
        public async Task UppdateProfile(int userId)
        {
            var profile = db.UserProfiles.FirstOrDefault(p => p.UserId == userId);
            if (profile != null)
            {
                //Изменения 
                db.SaveChangesAsync();
            }
        }

        public async Task DeleteProfileAsync(int userId)
        {
            var profile = await db.UserProfiles.FirstOrDefaultAsync(p => p.UserId == userId);
            if (profile != null)
            {
               db.UserProfiles.Remove(profile);
               await db.SaveChangesAsync();
            }
        }
    }
}
