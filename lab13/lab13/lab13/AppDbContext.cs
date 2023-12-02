using System;
using Microsoft.EntityFrameworkCore;


namespace lab13
{
	public class AppDbContext : DbContext
	{
		public DbSet<Magazine> Magazines { get; set; }
		public DbSet<Statie> Staties { get; set; }
		public DbSet<Author> Authors { get; set; }

		public AppDbContext()
		{
		}

		protected override void OnConfiguring(DbContextOptionsBuilder options)
		{
			options.UseSqlServer(@"Data Source=.\SQLEXPRESS;Initial Catalog=task1;User Id = Kat; Password = qwerty123");
		}
	}
}

