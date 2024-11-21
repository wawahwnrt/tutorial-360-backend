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
        private readonly IMapper _mapper;

        public UserTutorialService(IUserTutorialProgressRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // Retrieves user progress and maps it to DTOs
        public async Task<IEnumerable<UserTutorialProgressDto>> GetUserProgressAsync(int userId)
        {
            var progress = await _repository.GetUserProgressAsync(userId);

            // Handle the case where the repository returns null
            if (progress == null)
                return Enumerable.Empty<UserTutorialProgressDto>();

            return _mapper.Map<IEnumerable<UserTutorialProgressDto>>(progress);
        }

        // Marks a tutorial as completed
        public async Task<bool> CompleteTutorialAsync(int userId, IEnumerable<UserTutorialProgressDto> tutorialProgress)
        {
            if (tutorialProgress == null || !tutorialProgress.Any())
                return false; // Return false if there is no progress to process

            // Map DTOs to entities
            var entities = _mapper.Map<IEnumerable<UserCompletedTutorial>>(tutorialProgress);

            // Assign the userId to each entity
            foreach (var entity in entities)
            {
                entity.UserId = userId;
            }

            // Save the completed tutorials
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

            // Handle null progress and filter by role
            if (progress == null)
                return Enumerable.Empty<UserTutorialProgressDto>();

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
