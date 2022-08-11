using EfDb.Enums;
using EfDb.Models;
using Microsoft.EntityFrameworkCore;

namespace EfDb.Repositories
{
    public class TaskRepo
    {
        public static void CreateTask(AppEfContext db, CreateTaskModel model)
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

        public static List<Task> ReadTasks(AppEfContext db)
        {
            return db.Tasks.Include(t => t.User).ToList();
        }

        public static void UpdateTask(AppEfContext db, int id)
        {
            var task =  db.Tasks.FirstOrDefaultAsync(t => t.Id == id);
            if (task != null)
            {
                //Изменения
                db.SaveChangesAsync();
            }
        }

        public static async void DeleteTask(AppEfContext db, int id)
        {
            var task = db.Tasks.FirstOrDefault(t => t.Id == id);
            if (task != null)
            {
                db.Tasks.Remove(task);
                db.SaveChangesAsync();
            }
        }

        public static void AddUserToTask(AppEfContext db, string UsName, int taskId)
        {
            var task = db.Tasks.FirstOrDefault(t => t.Id == taskId);

            task.Status = (Status)2;
            task.User = db.Users.FirstOrDefault(u => u.SecondName == UsName);
            db.SaveChangesAsync();
        }
    }
}
