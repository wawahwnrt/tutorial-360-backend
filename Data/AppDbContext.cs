using Microsoft.EntityFrameworkCore;
using tutorial_backend_dotnet.Models;

namespace tutorial_backend_dotnet.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<TutorialGroup> TutorialGroups { get; set; }
        public DbSet<TutorialStep> TutorialSteps { get; set; }
        public DbSet<UserCompletedGroups> UserCompletedGroups { get; set; }
        public DbSet<UserCompletedSteps> UserCompletedSteps { get; set; }
        public DbSet<UserTutorialProgress> UserTutorialProgress { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TutorialGroup>()
                .ToTable("tutorialgroup")
                .HasKey(t => t.StepGroupName);
            
            modelBuilder.Entity<TutorialStep>()
                .ToTable("tutorialstep")
                .HasKey(t => t.StepId);
            
            modelBuilder.Entity<UserCompletedGroups>()
                .ToTable("usercompletedgroups")
                .HasKey(ucg => new { ucg.UserId, ucg.StepGroupName });
            
            modelBuilder.Entity<UserCompletedSteps>().ToTable("usercompletedsteps").HasNoKey();
            modelBuilder.Entity<UserTutorialProgress>().ToTable("usertutorialprogress").HasNoKey();
        }
    }
}
