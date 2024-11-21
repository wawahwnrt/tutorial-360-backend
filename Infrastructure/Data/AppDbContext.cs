using Microsoft.EntityFrameworkCore;
using tutorial_backend_dotnet.Domain.Entities;

namespace tutorial_backend_dotnet.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // TutorialGroupRole Entity
            modelBuilder.Entity<TutorialGroupRole>()
                .ToTable("tutorial_group_roles") // Matches table name in PostgreSQL
                .HasKey(tgr => new { tgr.StepGroupId, tgr.RoleId });

            // TutorialGroup Entity
            modelBuilder.Entity<TutorialGroup>()
                .ToTable("tutorial_group") // Matches table name in PostgreSQL
                .HasKey(t => t.StepGroupId);

            modelBuilder.Entity<TutorialGroup>()
                .HasMany(tg => tg.TutorialSteps)
                .WithOne(ts => ts.TutorialGroup)
                .HasForeignKey(ts => ts.StepGroupId)
                .OnDelete(DeleteBehavior.Cascade);

            // TutorialStep Entity
            modelBuilder.Entity<TutorialStep>()
                .ToTable("tutorial_step") // Matches table name in PostgreSQL
                .HasKey(t => t.StepId);

            modelBuilder.Entity<TutorialStep>()
                .HasOne(ts => ts.TutorialGroup)
                .WithMany(tg => tg.TutorialSteps)
                .HasForeignKey(ts => ts.StepGroupId);

            // UserCompletedTutorial Entity
            modelBuilder.Entity<UserCompletedTutorial>()
                .ToTable("user_completed_tutorial") // Matches table name in PostgreSQL
                .HasKey(uct => new { uct.UserId, uct.StepId, uct.StepGroupId });

            modelBuilder.Entity<UserCompletedTutorial>()
                .HasOne(uct => uct.TutorialStep)
                .WithMany()
                .HasForeignKey(uct => uct.StepId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserCompletedTutorial>()
                .HasOne(uct => uct.TutorialGroup)
                .WithMany()
                .HasForeignKey(uct => uct.StepGroupId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
