
namespace EfDb
{
    public class UserProfile
    {
        public int Id { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Citizenship { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
