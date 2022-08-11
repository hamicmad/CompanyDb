
using Microsoft.EntityFrameworkCore;

namespace EfDb.Repositories
{
    public class TeamRepo
    {
        public static void CreateTeam(AppEfContext db, string name, int managersAmount)
        {
            var team = new Team() { Name = name, ManagersAmount = managersAmount };

            db.Teams.Add(team);
            db.SaveChangesAsync();
        }

        public static List<Team> ReadTeams(AppEfContext db)
        {
            return db.Teams.Include(t => t.Users).ToList();
        }

        public static void UpdateTeam(AppEfContext db, string name)
        {
            var team = db.Teams.FirstOrDefault(t => t.Name == name);
            if (team != null)
            {
                //Изменения
                db.SaveChangesAsync();
            }
        }

        public static void DeleteTeam(AppEfContext db, string name)
        {
            var team = db.Teams.FirstOrDefault(t => t.Name == name);
            if (team != null)
            {
                db.Teams.Remove(team);
                db.SaveChangesAsync();
            }
        }

        public static void AddToTeam(AppEfContext db, string UsName, int teamId)
        {
            db.Teams.FirstOrDefault(t => t.Id == teamId).Users.Add(db.Users.FirstOrDefault(u => u.SecondName == UsName));
            db.SaveChangesAsync();
        }

        public static void RemoveFromTeam(AppEfContext db, string UsName, int teamId)
        {
            db.Teams.FirstOrDefault(t => t.Id == teamId).Users.Remove(db.Users.FirstOrDefault(u => u.SecondName == UsName));
            db.SaveChangesAsync();
        }
    }
}
