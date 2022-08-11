using Microsoft.EntityFrameworkCore;

namespace EfDb
{
    public class AppEfContext : DbContext
    {
        public DbSet<User> Users => Set<User>();
        public DbSet<UserProfile> UserProfiles => Set<UserProfile>();
        public DbSet<Team> Teams => Set<Team>();
        public DbSet<Task> Tasks => Set<Task>();

        public AppEfContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=AppEFdb;Trusted_Connection=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasOne(u => u.UserProfile).WithOne(up => up.User).OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(u => u.Teams).WithMany(t => t.Users);

                entity.HasMany(u => u.Tasks).WithOne(t => t.User).OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<UserProfile>().HasOne(up => up.User).WithOne(u => u.UserProfile);

            modelBuilder.Entity<Team>().HasMany(t => t.Users).WithMany(u => u.Teams);

            modelBuilder.Entity<Task>(entity =>
            {
                entity.HasOne(t => t.User).WithMany(u => u.Tasks);

                entity.Property(t => t.Complexity).HasConversion<string>();

                entity.Property(t => t.Status).HasConversion<string>();
            });
        }
    }
}
