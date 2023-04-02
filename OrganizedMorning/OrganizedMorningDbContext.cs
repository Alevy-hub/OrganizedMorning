
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OrganizedMorning.Entities;
using OrganizedMorning.Models;

namespace OrganizedMorning.OrganizedMorning
{
    public class OrganizedMorningDbContext : IdentityDbContext
    {
        public DbSet<MorningPlan> MorningPlans { get; set; }
        public DbSet<Times> Times { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=OrganizedMorning;User ID=appuser;Password=user#app;Trusted_Connection=False;Encrypt=True;TrustServerCertificate=True;");
            optionsBuilder.UseSqlServer("Server=tcp:appdatabase.database.windows.net,1433;Initial Catalog=testapp;Persist Security Info=False;User ID=appuser;Password=Aplikacja#Marzec;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }

        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    base.OnModelCreating(builder);
        //}

        public OrganizedMorningDbContext(DbContextOptions<OrganizedMorningDbContext> options) : base(options) { }

		public OrganizedMorningDbContext()
		{
		}
	}
}
