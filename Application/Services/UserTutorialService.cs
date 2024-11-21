using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using tutorial_backend_dotnet.Application.Interfaces;
using tutorial_backend_dotnet.Domain.Dtos;
using tutorial_backend_dotnet.Domain.Entities;

namespace tutorial_backend_dotnet.Application.Services
{
    public class UserTutorialService : IUserTutorialService
    {
        private readonly IUserTutorialProgressRepository _repository;
        private readonly ITutorialGroupRepository _tutorialGroupRepository;
        private readonly IMapper _mapper;

        public UserTutorialService(
            IUserTutorialProgressRepository repository,
            ITutorialGroupRepository tutorialGroupRepository,
            IMapper mapper)
        {
            _repository = repository;
            _tutorialGroupRepository = tutorialGroupRepository;
            _mapper = mapper;
        }

        // Retrieves user progress and maps it to DTOs
        public async Task<IEnumerable<UserTutorialProgressDto>> GetUserProgressAsync(int userId)
        {
            var progress = await _repository.GetUserProgressAsync(userId);
            return _mapper.Map<IEnumerable<UserTutorialProgressDto>>(progress);
        }

        public async Task<UserTutorialProgressDto> GetUserLastCompletedStepAsync(int userId, int roleId)
        {
            var progress = await _repository.GetUserLastCompletedStepAsync(userId, roleId);
            return _mapper.Map<UserTutorialProgressDto>(progress);
        }

        // Marks a tutorial as completed
        public async Task<bool> CompleteTutorialAsync(int userId, IEnumerable<UserTutorialProgressDto> tutorialProgress)
        {
            if (!tutorialProgress.Any())
            {
                return false;
            }

            var entities = _mapper.Map<IEnumerable<UserCompletedTutorial>>(tutorialProgress)
                .Select(entity =>
                {
                    entity.UserId = userId;
                    if (entity.CompletedAt == DateTime.MinValue)
                    {
                        entity.CompletedAt = DateTime.Now;
                    }
                    entity.IsReset = false;

                    return entity;
                }).ToList();

            // Fetch active groups and create a dictionary for fast lookup
            var activeGroups = await _tutorialGroupRepository.GetAllActiveGroupsAsync();
            var tutorialGroups = activeGroups.ToList();
            var activeGroupsLookup = tutorialGroups.ToDictionary(
                group => group.StepGroupId,
                group => group.TutorialGroupRoles.Select(r => r.RoleId).ToHashSet()
            );

            foreach (var entity in entities)
            {
                if (!activeGroupsLookup.TryGetValue(entity.StepGroupId, out var roleIds) ||
                    !roleIds.Contains(entity.RoleId))
                {
                    continue;
                }

                var stepGroup = tutorialGroups.FirstOrDefault(g => g.StepGroupId == entity.StepGroupId);
                if (stepGroup != null)
                {
                    entity.StepGroupName = stepGroup.StepGroupName;
                }
            }

            return await _repository.MarkTutorialsAsCompletedAsync(entities);
        }


        // Resets all progress for the user
        public async Task<bool> ResetUserProgressAsync(int userId)
        {
            return await _repository.ResetUserProgressAsync(userId);
        }

        // Retrieves completed tutorials filtered by role
        public async Task<IEnumerable<UserTutorialProgressDto>> GetCompletedTutorialsByUserAsync(int userId, int roleId)
        {
            var progress = await _repository.GetUserProgressAsync(userId);
            var filteredProgress = progress
                .Where(p => p.RoleId == roleId) // Filter by role
                .ToList();

            return _mapper.Map<IEnumerable<UserTutorialProgressDto>>(filteredProgress);
        }

        // Resets progress for a specific group
        public async Task ResetProgressByGroupAsync(int userId, int groupId)
        {
            await _repository.ResetProgressByGroupAsync(userId, groupId);
        }
    }
}
