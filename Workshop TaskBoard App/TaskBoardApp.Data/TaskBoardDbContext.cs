using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TaskBoardApp.Data.Models;
using Task = TaskBoardApp.Data.Models.Task;

namespace TaskBoardApp.Data
{
    public class TaskBoardDbContext : IdentityDbContext
    {

        public TaskBoardDbContext()
        {
        }

        public TaskBoardDbContext(DbContextOptions<TaskBoardDbContext> options)
            : base(options)
        {
        }

        

        public DbSet<Task> Tasks { get; set; } 

        public DbSet<Board> Boards { get; set;} 

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // this way you can apply all configurations in a single line
            builder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(TaskBoardDbContext)));

            base.OnModelCreating(builder);
        }
    }
}