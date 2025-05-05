using Microsoft.EntityFrameworkCore;
using AuthService.Models;

namespace AuthService.Data
{
    public class TaskDbContext : DbContext
    {
        public TaskDbContext(DbContextOptions<TaskDbContext> options) : base(options) { }

        public DbSet<TaskItem> Tasks { get; set; }
    }

    public class TaskItem
    {
    }
}
//TaskDbContext.cs