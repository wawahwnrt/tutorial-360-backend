using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using tutorial_backend_dotnet.Application.Interfaces;
using tutorial_backend_dotnet.Domain.Dtos;

namespace tutorial_backend_dotnet.Application.Services
{
    public class TutorialStepService : ITutorialStepService
    {
        private readonly ITutorialStepRepository _repository;
        private readonly IMapper _mapper;

        public TutorialStepService(ITutorialStepRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TutorialStepDto>> GetAllActiveStepsAsync()
        {
            var steps = await _repository.GetAllActiveStepsAsync();
            return _mapper.Map<IEnumerable<TutorialStepDto>>(steps);
        }

        public async Task<IEnumerable<TutorialStepDto>> GetStepsByGroupIdAsync(int groupId)
        {
            var steps = await _repository.GetStepsByGroupAsync(groupId);
            return _mapper.Map<IEnumerable<TutorialStepDto>>(steps);
        }

        public async Task<TutorialStepDto> GetStepByIdAsync(int stepId)
        {
            var step = await _repository.GetStepByIdAsync(stepId);
            return _mapper.Map<TutorialStepDto>(step);
        }
    }
}
