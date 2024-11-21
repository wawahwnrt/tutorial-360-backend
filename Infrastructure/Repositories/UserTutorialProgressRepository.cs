using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using tutorial_backend_dotnet.Application.Interfaces;
using tutorial_backend_dotnet.Domain.Entities;
using tutorial_backend_dotnet.Infrastructure.Data;

namespace tutorial_backend_dotnet.Infrastructure.Repositories
{
    public class UserTutorialProgressRepository : BaseRepository<UserCompletedTutorial>, IUserTutorialProgressRepository
    {
        public UserTutorialProgressRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<UserCompletedTutorial>> GetUserProgressAsync(int userId)
        {
            var progress = await DbSet
                .Where(progress => 
                    progress.UserId == userId &&
                    progress.IsReset == false)
                .ToListAsync();
            return progress;
        }

        public async Task<UserCompletedTutorial> GetUserLastCompletedStepAsync(int userId, int roleId)
        {
            return await DbSet
                .Where(progress => 
                    progress.UserId == userId &&
                    progress.RoleId == roleId &&
                    progress.IsReset == false)
                .OrderByDescending(progress => progress.CompletedAt)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> MarkTutorialsAsCompletedAsync(IEnumerable<UserCompletedTutorial> entities)
        {
            var trackProgress = entities.ToList();
            if (
                trackProgress.Count == 0 ||
                trackProgress.Any(entity => entity.UserId == 0) ||
                trackProgress.Any(entity => entity.RoleId == 0) ||
                trackProgress.Any(entity => entity.StepGroupId == 0) ||
                trackProgress.Any(entity => entity.StepId == 0)
            )
            {
                return false;
            }

            foreach (var entity in trackProgress)
            {
                // Checked if the step has already been completed
                var existingEntity = await DbSet
                    .FirstOrDefaultAsync(
                        uct => uct.UserId == entity.UserId &&
                               uct.RoleId == entity.RoleId &&
                               uct.StepGroupId == entity.StepGroupId &&
                               uct.StepId == entity.StepId);

                if (existingEntity != null && existingEntity.CompletedAt >= entity.CompletedAt)
                {
                    continue;
                }

                if (existingEntity != null)
                {
                    existingEntity.CompletedAt = entity.CompletedAt;
                    existingEntity.IsReset = false;
                    await Context.SaveChangesAsync();
                }
                else
                {
                    DbSet.Add(entity);
                    await Context.SaveChangesAsync();
                }
            }

            return true;
        }


        public async Task<bool> ResetUserProgressAsync(int userId)
        {
            var progress = await DbSet.Where(p => p.UserId == userId).ToListAsync();
            foreach (var tutorial in progress)
            {
                tutorial.IsReset = true;
                tutorial.CompletedAt = DateTime.MinValue;
            }
            return await Context.SaveChangesAsync() > 0;
        }

        public async Task ResetProgressByGroupAsync(int userId, int groupId)
        {
            // var progressToDelete = DbSet
            //     .Where(p => p.UserId == userId && p.StepGroupId == groupId)
            //     .ToList();

            var existingEntities = await DbSet.Where(uct =>
                    uct.UserId == userId &&
                    uct.StepGroupId == groupId &&
                    uct.IsReset == false)
                .ToListAsync();

            existingEntities.ForEach(entity =>
            {
                entity.IsReset = true;
                entity.CompletedAt = DateTime.MinValue;
            });
            await Context.SaveChangesAsync();
        }
    }
}
