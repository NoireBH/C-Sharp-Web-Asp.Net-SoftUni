using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task = TaskBoardApp.Data.Models.Task;

namespace TaskBoardApp.Data.Configurations
{
    internal class TaskEntityConfiguration : IEntityTypeConfiguration<Task>
    {
        public void Configure(EntityTypeBuilder<Task> builder)
        {
            builder
                .HasOne(t => t.Board)
                .WithMany(b => b.Tasks)
                .HasForeignKey(t => t.BoardId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasData(GenerateTasks());
        }

        private ICollection<Task> GenerateTasks()
        {
            ICollection<Task> tasks = new HashSet<Task>();

            Task newBoard;

            newBoard = new Task()
            {
                Id = 1,
                Title = "Improve HTML and CSS",
                Description = "Implement more suitable HTML attributes and improve CSS styling for all public pages",
                CreatedOn = DateTime.Now.AddDays(-200),
                OwnerId = "61e96ed0-d94d-40c5-a0fc-a52a6ee97628",
                BoardId = 2
            };
            tasks.Add(newBoard);

            newBoard = new Task()
            {
                Id = 2,
                Title = "Android App",
                Description = "Create an Android client app for the TaskBoardApp RESTful API",
                CreatedOn = DateTime.Now.AddMonths(-5),
                OwnerId = "61e96ed0-d94d-40c5-a0fc-a52a6ee97628",
                BoardId = 2
            };
            tasks.Add(newBoard);

            newBoard = new Task()
            {
                Id = 3,
                Title = "Desktop Client App",
                Description = "Create Windows Forms desktop app",
                CreatedOn = DateTime.Now.AddMonths(-1),
                OwnerId = "88a30ac8-323e-4471-93a5-389a1af17546",
                BoardId = 2
            };
            tasks.Add(newBoard);

            newBoard = new Task()
            {
                Id = 4,
                Title = "Create Tasks",
                Description = "Implement [Create Task] page that adds new tasks",
                CreatedOn = DateTime.Now.AddYears(-1),
                OwnerId = "88a30ac8-323e-4471-93a5-389a1af17546",
                BoardId = 2
            };
            tasks.Add(newBoard);

            return tasks;
        }
    }
}
