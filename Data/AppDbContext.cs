using Microsoft.EntityFrameworkCore;
using tutorial_backend_dotnet.Models;

namespace tutorial_backend_dotnet.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<TutorialGroup> TutorialGroups { get; set; }
        public DbSet<TutorialStep> TutorialSteps { get; set; }
        public DbSet<UserCompletedTutorials> UserCompletedTutorials { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TutorialGroup>()
                .ToTable("tutorialgroup")
                .HasKey(t => t.StepGroupName);
            
            modelBuilder.Entity<TutorialStep>()
                .ToTable("tutorialstep")
                .HasKey(t => t.StepId);
            
            modelBuilder.Entity<UserCompletedTutorials>().ToTable("usercompletedtutorial").HasNoKey();
        }
    }
}
