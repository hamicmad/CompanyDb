using EfDb.Enums;
using EfDb.Models;
using Microsoft.EntityFrameworkCore;

namespace EfDb.Repositories
{
    public class TaskRepo
    {
        private readonly AppEfContext db;
        public TaskRepo()
        {
            db = new AppEfContext();
        }
        public void CreateTask(CreateTaskModel model)
        {
            var task = new Task()
            {
                Complexity = model.Complexity,
                Hours = model.Hours,
                Status = model.Status,
                Description = model.Description
            };
            db.Tasks.Add(task);
            db.SaveChangesAsync();
        }

        public List<Task> ReadTasks()
        {
            return db.Tasks.Include(t => t.User).ToList();
        }

        public void UpdateTask(int id)
        {
            var task = db.Tasks.FirstOrDefault(t => t.Id == id);
            if (task != null)
            {
                //Изменения
                db.SaveChangesAsync();
            }
        }

        public void DeleteTask(int id)
        {
            var task = db.Tasks.FirstOrDefault(t => t.Id == id);
            if (task != null)
            {
                db.Tasks.Remove(task);
                db.SaveChangesAsync();
            }
        }

        public void AddUserToTask(int UserId, int taskId)
        {
            var task = db.Tasks.FirstOrDefault(t => t.Id == taskId);
            if (task != null)
            {
                task.User = db.Users.FirstOrDefault(u => u.Id == UserId);
                task.Status = Status.InProcess;
                db.SaveChangesAsync();
            }
        }
    }
}
