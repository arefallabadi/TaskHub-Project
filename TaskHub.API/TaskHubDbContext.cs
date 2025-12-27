using Microsoft.EntityFrameworkCore;
using TaskHub.API.Entities; 

namespace TaskHub.API.Data
{
        public class TaskHubDbContext : DbContext
    {
        public TaskHubDbContext(DbContextOptions<TaskHubDbContext> options) : base(options) { }

        public DbSet<TaskItem> Tasks { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<User> Users { get; set; }

    }
    
}

