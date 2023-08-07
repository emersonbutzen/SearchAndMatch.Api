using Microsoft.EntityFrameworkCore;
using SearchAndMatch.Domain.Entities;

namespace SearchAndMatch.DAL.Context
{
    public class SearchAndMatchContext: DbContext
    {
        public SearchAndMatchContext(DbContextOptions<SearchAndMatchContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("wmda");

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Engine> Engines { get; set; }
    }
}
