using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using tutorial_backend_dotnet.Application.Interfaces;
using tutorial_backend_dotnet.Domain.Dtos;
using tutorial_backend_dotnet.Domain.Entities;

namespace tutorial_backend_dotnet.Application.Services
{
    public class TutorialGroupService : ITutorialGroupService
    {
        private readonly ITutorialGroupRepository _repository;
        private readonly IMapper _mapper;

        public TutorialGroupService(ITutorialGroupRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TutorialGroupDto>> GetAllActiveGroupsAsync()
        {
            var groups = await _repository.GetAllActiveGroupsAsync();
            return _mapper.Map<IEnumerable<TutorialGroupDto>>(groups);
        }

        public async Task<TutorialGroupDto> GetGroupWithStepsAsync(int groupId)
        {
            var group = await _repository.GetGroupWithStepsAsync(groupId);
            return _mapper.Map<TutorialGroupDto>(group);
        }

        public async Task<IEnumerable<TutorialGroupDto>> GetGroupsByRoleAsync(int roleId)
        {
            var groups = await _repository.GetGroupsByRoleAsync(roleId);
            return _mapper.Map<IEnumerable<TutorialGroupDto>>(groups);
        }
    }
}
