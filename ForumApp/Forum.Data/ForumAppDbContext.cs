using ForumApp.Data.Configuration;
using ForumApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumApp.Models
{
	public class ForumAppDbContext : DbContext
	{
		protected ForumAppDbContext()
		{
		}

		public ForumAppDbContext(DbContextOptions<ForumAppDbContext> options) : base(options)
		{
		}

		

		public DbSet<Post> Posts { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration(new PostEntityConfiguration());

			base.OnModelCreating(modelBuilder);
		}
	}
}
