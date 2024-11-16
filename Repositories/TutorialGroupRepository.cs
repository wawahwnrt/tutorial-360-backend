using System.Collections.Generic;
using System.Threading.Tasks;
using tutorial_backend_dotnet.Data;
using tutorial_backend_dotnet.Models;
using Microsoft.EntityFrameworkCore;

namespace tutorial_backend_dotnet.Repositories
{
    public class TutorialGroupRepository : ITutorialGroupRepository
    {
        private readonly AppDbContext _context;

        public TutorialGroupRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TutorialGroup>> GetAllTutorialGroups()
        {
            return await _context.TutorialGroups.ToListAsync();
        }

        public async Task<TutorialGroup> GetTutorialGroupByName(string name)
        {
            return await _context.TutorialGroups.FindAsync(name);
        }

        public async Task AddTutorialGroup(TutorialGroup group)
        {
            await _context.TutorialGroups.AddAsync(group);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTutorialGroup(TutorialGroup group)
        {
            _context.TutorialGroups.Update(group);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTutorialGroup(string name)
        {
            var group = await _context.TutorialGroups.FindAsync(name);
            if (group != null)
            {
                _context.TutorialGroups.Remove(group);
                await _context.SaveChangesAsync();
            }
        }
    }
}
