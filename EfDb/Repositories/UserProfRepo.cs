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
        public void CreateProfile(CreateProfileModel model)
        {
            var uProfile = new UserProfile()
            {
                User = db.Users.FirstOrDefault(u => u.Id == model.UserId),
                Country = model.Country,
                City = model.City,
                Citizenship = model.Citizenship
            };
            db.UserProfiles.Add(uProfile);
            db.SaveChangesAsync();
        }

        public UserProfile ReadProfile(int userId)
        {
            var uProfile = db.UserProfiles.Include(p => p.User).FirstOrDefault(p => p.User.Id == userId);
            if (uProfile == null)
                return new UserProfile();

            return uProfile;
        }
        public void UppdateProfile(string sName)
        {
            var profile = db.UserProfiles.FirstOrDefault(p => p.User.SecondName == sName);
            if (profile != null)
            {
                //Изменения 
                db.SaveChangesAsync();
            }
        }

        public void DeleteProfile(string sName)
        {
            var profile = db.UserProfiles.FirstOrDefault(p => p.User.SecondName == sName);
            if (profile != null)
            {
                db.UserProfiles.Remove(profile);
                db.SaveChangesAsync();
            }
        }
    }
}
