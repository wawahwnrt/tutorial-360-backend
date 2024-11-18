using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using tutorial_backend_dotnet.Data;
using tutorial_backend_dotnet.Models;

namespace tutorial_backend_dotnet.Repositories
{
    public class UserTutorialProgressRepository : IUserTutorialProgressRepository
    {
        private readonly AppDbContext _context;

        public UserTutorialProgressRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserCompletedTutorials>> GetAllUserCompletedTutorials()
        {
            return await _context.UserCompletedTutorials.ToListAsync();
        }
        
        public async Task<bool> CheckIfUserNewToTutorial(int userId, int roleId)
        {
            return !await _context.UserCompletedTutorials
                .AnyAsync(uct => uct.UserId == userId && uct.RoleId == roleId);
        }

        public async Task<IEnumerable<UserCompletedTutorials>> GetUserCompletedTutorials(int userId, int roleId)
        {
            return await _context.UserCompletedTutorials
                .Where(uct => uct.UserId == userId && uct.RoleId == roleId)
                .OrderByDescending(uct => uct.CompletedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<UserTutorialStatus>> GetUserTutorialStatus(int userId, int roleId)
        {
            var userCompletedTutorials = await GetUserCompletedTutorials(userId, roleId);
            var tutorials = await new TutorialGroupRepository(_context).GetAllActiveTutorials(roleId);

            return (from completedTutorial in userCompletedTutorials
                select tutorials.FirstOrDefault(t => t.StepGroupName == completedTutorial.StepGroupName)
                into tutorialGroup
                where tutorialGroup != null
                let tutorialSteps = tutorialGroup.TutorialSteps.Select(step =>
                        new TutorialStepStatus
                        {
                            StepId = step.StepId,
                            StepOrder = step.StepOrder,
                            StepName = step.StepName,
                            StepDescription = step.StepDescription,
                            StepPage = step.StepPage,
                            IsCompleted = true
                        })
                    .ToList()
                select new UserTutorialStatus
                {
                    StepGroupId = tutorialGroup.StepGroupId,
                    StepGroupName = tutorialGroup.StepGroupName,
                    StepGroupDescription = tutorialGroup.StepGroupDescription,
                    TutorialSteps = tutorialSteps,
                    IsCompleted = tutorialSteps.All(step => step.IsCompleted)
                }).ToList();
        }
        
        public async Task<UserTutorialProgress> GetLatestUserTutorialProgress(int userId, int roleId) 
        {
            var userCompletedTutorials = await GetUserCompletedTutorials(userId, roleId);
            var latestTutorial = userCompletedTutorials.FirstOrDefault();
            if (latestTutorial == null)
            {
                return null;
            }
            else
            {
                
                return new UserTutorialProgress
                {
                    UserId = latestTutorial.UserId,
                    RoleId = latestTutorial.RoleId,
                    CurrentStepGroupName = latestTutorial.StepGroupName,
                    CurrentStepId = latestTutorial.StepId,
                    UpdatedAt = latestTutorial.CompletedAt,
                    IsCompleteAll = userCompletedTutorials.All(uct => uct.StepGroupName == latestTutorial.StepGroupName)
                };
            }
        }
        
        public async Task<IEnumerable<UserTutorialStatus>> AddUserCompletedTutorials(IEnumerable<UserCompletedTutorials> tutorials)
        {
            await _context.UserCompletedTutorials.AddRangeAsync(tutorials);
            await _context.SaveChangesAsync();

            return await GetUserTutorialStatus(tutorials.First().UserId, tutorials.First().RoleId);
        }
        
        public async Task<IEnumerable<UserTutorialStatus>> ResetUserCompletedTutorials(int userId, int roleId, string groupName)
        {
            var userCompletedTutorials = await _context.UserCompletedTutorials
                .Where(uct => uct.UserId == userId && uct.RoleId == roleId && uct.StepGroupName == groupName)
                .ToListAsync();

            _context.UserCompletedTutorials.RemoveRange(userCompletedTutorials);
            await _context.SaveChangesAsync();
            
            return await GetUserTutorialStatus(userId, roleId);
        }
    }
}
