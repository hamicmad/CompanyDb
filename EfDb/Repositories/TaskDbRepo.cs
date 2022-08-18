using EfDb.Enums;
using EfDb.Models;
using Microsoft.EntityFrameworkCore;

namespace EfDb.Repositories
{
    public class TaskDbRepo
    {
        private readonly AppEfContext db;
        public TaskDbRepo()
        {
            db = new AppEfContext();
        }
        public async Task CreateTaskAsync(CreateTaskModel model)
        {
            var task = new TaskDb()
            {
                Complexity = model.Complexity,
                Hours = model.Hours,
                Status = model.Status,
                Description = model.Description
            };
           await db.Tasks.AddAsync(task);
           await db.SaveChangesAsync();
        }

        public async Task<List<TaskDb>> ReadTaskAsync()
        {
            return await db.Tasks.Include(t => t.User).ToListAsync();
        }

        public async Task UpdateTaskAsync(int id)
        {
            var task = await db.Tasks.FirstOrDefaultAsync(t => t.Id == id);
            if (task != null)
            {
                //Изменения
                await db.SaveChangesAsync();
            }
        }

        public async Task DeleteTaskAsync(int id)
        {
            var task = await db.Tasks.FirstOrDefaultAsync(t => t.Id == id);
            if (task != null)
            {
                db.Tasks.Remove(task);
                await db.SaveChangesAsync();
            }
        }

        public async Task AddUserToTaskAsync(int UserId, int taskId)
        {
            var task = await db.Tasks.FirstOrDefaultAsync(t => t.Id == taskId);
            if (task != null)
            {
                task.User = await db.Users.FirstOrDefaultAsync(u => u.Id == UserId);
                task.Status = Status.InProcess;
                await db.SaveChangesAsync();
            }
        }
    }
}
