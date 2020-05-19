using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infra.Data.Context
{
	public class SQLContext : IdentityDbContext
	{
		public SQLContext(DbContextOptions<SQLContext> options) : base(options)
		{
			Database.EnsureCreated();
		}

		public DbSet<Autor> Autor { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
				optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=TesteFranciscoBibliotecaDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			//Mock de dados
			modelBuilder.Entity<Autor>().HasData(
				new Autor { Id = 1, Nome = "Guimaraes", },
				new Autor { Id = 2, Nome = "Joao Silva Neto" },
				new Autor { Id = 3, Nome = "Joao Neto" },
				new Autor { Id = 4, Nome = "ANTONIA SILVA DE AZEVEDO" },
				new Autor { Id = 5, Nome = "ANDRADE" },
				new Autor { Id = 6, Nome = "PEDRO SILVA FILHO" },
				new Autor { Id = 7, Nome = "NETO neto Filho" },
				new Autor { Id = 8, Nome = "neto FilHO" });

		}
	}
}
