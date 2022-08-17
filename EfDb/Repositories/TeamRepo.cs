using Microsoft.EntityFrameworkCore;

namespace EfDb.Repositories
{
    public class TeamRepo
    {
        private readonly AppEfContext db;
        public TeamRepo()
        {
         db = new AppEfContext();
        }
        public void CreateTeam(string name, int managersAmount)
        {
            var team = new Team() { Name = name, ManagersAmount = managersAmount };

            db.Teams.Add(team);
            db.SaveChangesAsync();
        }

        public  List<Team> ReadTeams()
        {
            return db.Teams.Include(t => t.Users).ToList();
        }

        public void UpdateTeam(int teamId)
        {
            var team = db.Teams.FirstOrDefault(t => t.Id == teamId);
            if (team != null)
            {
                //Изменения
                db.SaveChangesAsync();
            }
        }

        public void DeleteTeam(int teamId)
        {
            var team = db.Teams.FirstOrDefault(t => t.Id == teamId);
            if (team != null)
            {
                db.Teams.Remove(team);
                db.SaveChangesAsync();
            }
        }

        public void AddToTeam(List<int> usersId, int teamId)
        {
            db.Teams.FirstOrDefault(t => t.Id == teamId).Users.AddRange(new List<User>(db.Users.Where(u => usersId.Contains(u.Id))));
            db.SaveChangesAsync();
        }

        public void RemoveFromTeam(List<int> usersId, int teamId)
        {
            var team =  db.Teams.FirstOrDefault(t => t.Id == teamId);
            
            var users = db.Users.Where(u => usersId.Contains(u.Id)).ToList();
            if (users != null && team != null)
            {
                for(int i = 0; i < users.Count; i++)
                {
                    team.Users.Remove(users[i]); // TODO: проверить удаление из тимы
                }
            }
            db.SaveChangesAsync();
        }
    }
}
