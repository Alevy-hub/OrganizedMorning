
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
            optionsBuilder.UseNpgsql("Server=psql01.mikr.us;Port=5432;Database=db_o184;User Id=o184;Password=B58C_812fad;Integrated Security=false;");
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
