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
        public UserTutorialProgressRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<UserCompletedTutorial>> GetUserProgressAsync(int userId)
        {
            return await _dbSet
                .Where(progress => progress.UserId == userId)
                .ToListAsync();
        }

        public async Task<bool> MarkTutorialsAsCompletedAsync(IEnumerable<UserCompletedTutorial> entities)
        {
            if (entities == null || !entities.Any())
                return false;

            _dbSet.AddRange(entities);
            await _context.SaveChangesAsync();
            return true;
        }


        public async Task<bool> ResetUserProgressAsync(int userId)
        {
            var progress = _dbSet.Where(p => p.UserId == userId);
            _dbSet.RemoveRange(progress);
            return await _context.SaveChangesAsync() > 0;
        }
        
        public async Task ResetProgressByGroupAsync(int userId, int groupId)
        {
            var progressToDelete = _dbSet.Where(uct => uct.UserId == userId && uct.StepGroupId == groupId);
            _dbSet.RemoveRange(progressToDelete);
            await _context.SaveChangesAsync();
        }
    }
}
