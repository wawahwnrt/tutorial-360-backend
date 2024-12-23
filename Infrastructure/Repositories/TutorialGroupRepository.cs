﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using tutorial_backend_dotnet.Application.Interfaces;
using tutorial_backend_dotnet.Domain.Entities;
using tutorial_backend_dotnet.Infrastructure.Data;

namespace tutorial_backend_dotnet.Infrastructure.Repositories
{
    public class TutorialGroupRepository : BaseRepository<TutorialGroup>, ITutorialGroupRepository
    {
        public TutorialGroupRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<TutorialGroup>> GetAllActiveGroupsAsync()
        {
            // Ensures all relevant navigation properties are included to avoid lazy loading issues
            return await DbSet
                .Where(group => group.IsActive)
                .Include(group => group.TutorialSteps)
                .Include(group => group.TutorialGroupRoles)
                .ThenInclude(role => role.UserTutorialRole) // Include nested navigation property
                .ToListAsync();
        }

        public async Task<IEnumerable<TutorialGroup>> GetGroupsByRoleAsync(int roleId)
        {
            // Filters active groups and ensures navigation properties are fully loaded
            return await DbSet
                .Where(group => group.IsActive &&
                                group.TutorialGroupRoles.Any(role => role.RoleId == roleId)) // Filter based on roles
                .Include(group => group.TutorialSteps)
                .Include(group => group.TutorialGroupRoles)
                .ThenInclude(role => role.UserTutorialRole) // Include nested navigation property
                .ToListAsync();
        }

        public async Task<TutorialGroup> GetGroupWithStepsAsync(int groupId)
        {
            // Fetches a single group by ID and includes all relevant navigation properties
            return await DbSet
                .Include(group => group.TutorialSteps)
                .Include(group => group.TutorialGroupRoles)
                .ThenInclude(role => role.UserTutorialRole) // Include nested navigation property
                .FirstOrDefaultAsync(group => group.StepGroupId == groupId && group.IsActive);
        }
        
        public async Task<IEnumerable<TutorialStep>> GetActiveStepsByRoleAsync(int roleId)
        {
            // Fetches all steps associated with a specific role
            var groups = await GetGroupsByRoleAsync(roleId);
            return groups.SelectMany(group => group.TutorialSteps).Where(step => step.IsActive);
        }
    }
}
