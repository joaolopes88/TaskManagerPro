using Microsoft.EntityFrameworkCore;
using TaskService.Models;
using Shared; // Import the User class

namespace TaskService.Data
{
    public class TaskDbContext : DbContext
    {
        public TaskDbContext(DbContextOptions<TaskDbContext> options) : base(options) { }

        public DbSet<TaskItem> Tasks { get; set; }
        public DbSet<User> Users { get; set; } // Add the Users table

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaskItem>(entity =>
            {
                entity.ToTable("Tasks");

                entity.HasKey(t => t.TaskID);

                entity.Property(t => t.Title).IsRequired();
                entity.Property(t => t.StartDate).IsRequired();
                entity.Property(t => t.EndDate).IsRequired();
                entity.Property(t => t.Username).IsRequired();

                // Configure the foreign key relationship
                entity.HasOne<User>()
                      .WithMany()
                      .HasForeignKey(t => t.Username)
                      .HasPrincipalKey(u => u.Username) // Use Username as the principal key
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users");

                entity.HasKey(u => u.Id);

                entity.Property(u => u.Username).IsRequired().HasMaxLength(50);
                entity.Property(u => u.Email).IsRequired().HasMaxLength(100);
                entity.Property(u => u.PasswordHash).IsRequired();

                // Configure Username as an alternate key
                entity.HasAlternateKey(u => u.Username);
            });
        }
    }
}