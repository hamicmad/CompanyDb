using EfDb.Enums;

namespace EfDb.Models
{
    public class CreateTaskModel
    {
        public TaskComplexity Complexity { get; set; }
        public int Hours { get; set; }
        public Status Status { get; set; }
        public string? Description { get; set; }
    }
}
