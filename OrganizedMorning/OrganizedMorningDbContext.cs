using Microsoft.EntityFrameworkCore;
using OrganizedMorning.Entities;
using OrganizedMorning.Models;

namespace OrganizedMorning.OrganizedMorning
{
    public class OrganizedMorningDbContext : DbContext
    {
        public DbSet<MorningPlan> MorningPlans { get; set; }
        public DbSet<Times> Times { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=OrganizedMorningDb;Trusted_Connection=True;");
        }
    }
}
