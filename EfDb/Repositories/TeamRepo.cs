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
        public async Task CreateTeamAsync(string name, int managersAmount)
        {
            var team = new Team() { Name = name, ManagersAmount = managersAmount };

            await db.Teams.AddAsync(team);
            await db.SaveChangesAsync();
        }

        public async Task<List<Team>> ReadTeamsAsync()
        {
            return await db.Teams.Include(t => t.Users).ToListAsync();
        }

        public async Task UpdateTeamAsync(int teamId)
        {
            var team = await db.Teams.FirstOrDefaultAsync(t => t.Id == teamId);
            if (team != null)
            {
                //Изменения
                await db.SaveChangesAsync();
            }
        }

        public async Task DeleteTeam(int teamId)
        {
            var team = await db.Teams.FirstOrDefaultAsync(t => t.Id == teamId);
            if (team != null)
            {
                db.Teams.Remove(team);
                await db.SaveChangesAsync();
            }
        }

        public async Task AddToTeamAsync(List<int> usersId, int teamId)
        {
            var team = await db.Teams.FirstOrDefaultAsync(t => t.Id == teamId);
            var users = await db.Users.Where(u => usersId.Contains(u.Id)).ToListAsync();
            if (team != null)
            {
                team.Users.AddRange(users);
                db.SaveChangesAsync();
            }
        }

        public async Task RemoveFromTeamAsync(List<int> usersId, int teamId)
        {
            var team = await db.Teams.Include(t => t.Users).FirstOrDefaultAsync(t => t.Id == teamId);
            var users = await db.Users.Where(u => usersId.Contains(u.Id)).ToListAsync();

            if (team != null)
            {
                for (int i = 0; i < users.Count; i++)
                {
                    team.Users.Remove(users[i]); // TODO: проверить удаление из тимы
                }
            }
           await db.SaveChangesAsync();
        }
    }
}
