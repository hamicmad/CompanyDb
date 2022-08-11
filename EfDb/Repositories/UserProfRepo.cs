using EfDb.Models;
using Microsoft.EntityFrameworkCore;

namespace EfDb.Repositories
{
    public class UserProfRepo
    {
        public static void CreateProfile(AppEfContext db, string sName, CreateProfileModel model)
        {
            var uProfile = new UserProfile()
            {
                User = db.Users.FirstOrDefault(u => u.SecondName == sName),
                Country = model.Country,
                City = model.City,
                Citizenship = model.Citizenship
            };
            db.UserProfiles.Add(uProfile);
            db.SaveChangesAsync();
        }

        public static UserProfile ReadProfile(AppEfContext db, string sName)
        {
            var uProfile = db.UserProfiles.FirstOrDefault(p => p.User.SecondName == sName);
            if (uProfile == null)
                return new UserProfile();

            return uProfile;
        }
        public static void UppdateProfile (AppEfContext db, string sName)
        {
            var profile = db.UserProfiles.FirstOrDefault(p => p.User.SecondName == sName);
            if (profile != null)
            {
                //Изменения 
                db.SaveChangesAsync();
            }
        }
        
        public static void DeleteProfile(AppEfContext db, string sName)
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
