

namespace EfDb
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; } 
        public string SecondName { get; set; } 
        public DateTime DateOfBirth { get; set; }

        public UserProfile UserProfile { get; set; }
        public List<Team> Teams { get; set; } = new();
        public List<Task> Tasks { get; set; } = new();

    }
}
