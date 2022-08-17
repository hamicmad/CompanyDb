
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
        public void CreateUser(CreateUserModel model)
        {
            var user = new User()
            {
                FirstName = model.FirstName,
                SecondName = model.SecondName,
                DateOfBirth = model.DateOfBirth,
            };
            db.Users.Add(user);
            db.SaveChanges();
        }

        public List<User> ReadUsers()
        {
            return db.Users.Include(u => u.UserProfile).ToList();
        }

        public void UpdateUser(string sName)
        {
            var user = db.Users.FirstOrDefault(u => u.SecondName == sName);
            if (user != null)
            {
                //Изменения
                db.SaveChanges();
            }
        }

        public void DeleteUser(string sName)
        {
            var user = db.Users.FirstOrDefault(u => u.SecondName == sName);
            if (user != null)
            {
                db.Users.Remove(user);
                db.SaveChanges();
            }
        }
    }
}
