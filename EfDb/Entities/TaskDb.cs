using EfDb.Enums;

namespace EfDb
{
    public class TaskDb
    {
        public int Id { get; set; }
        public TaskComplexity Complexity { get; set; }
        public int Hours { get; set; }
        public Status Status { get; set; }
        public string? Description { get; set; }

        public int? UserId { get; set; }
        public User? User { get; set; }

    }
}
