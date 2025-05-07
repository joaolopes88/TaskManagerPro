using Microsoft.EntityFrameworkCore;
using Shared; // Import the User class from the Shared project

namespace AuthService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; } // Reference the Users table
    }
}