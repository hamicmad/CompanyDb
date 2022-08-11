
namespace EfDb
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ManagersAmount { get; set; }

        public List<User> Users { get; set; } = new();
    }
}
