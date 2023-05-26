
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



        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    //optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=OrganizedMorning;User ID=appuser;Password=user#app;Trusted_Connection=False;Encrypt=True;TrustServerCertificate=True;");
        //    optionsBuilder.UseMySQL("Server=mws02.mikr.us;Port=50005;Database=organizedmorningdb;Uid=root;Pwd=NeFezZxsUW;Charset=utf8mb4;");
        //}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

		public OrganizedMorningDbContext(DbContextOptions<OrganizedMorningDbContext> options) : base(options) { }

	}
}
