using System.Linq;
using AutoMapper;
using tutorial_backend_dotnet.Domain.Dtos;
using tutorial_backend_dotnet.Domain.Entities;

namespace tutorial_backend_dotnet.Utils.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Map TutorialGroupRole to UserTutorialRoleDto
            CreateMap<TutorialGroupRole, UserTutorialRoleDto>()
                .ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.RoleId))
                .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.UserTutorialRole.RoleName));

            // Map UserTutorialRole to UserTutorialRoleDto
            CreateMap<UserTutorialRole, UserTutorialRoleDto>();

            // Map TutorialGroup to TutorialGroupDto
            CreateMap<TutorialGroup, TutorialGroupDto>()
                .ForMember(dest => dest.RoleIds, opt => opt.MapFrom(src => src.TutorialGroupRoles.Select(role => new UserTutorialRoleDto
                {
                    RoleId = role.RoleId,
                    RoleName = role.UserTutorialRole.RoleName
                }))) // Default to an empty list if null
                .ForMember(dest => dest.TutorialSteps,
                    opt => opt.MapFrom(src => src.TutorialSteps));

            // Map TutorialStep to TutorialStepDto
            CreateMap<TutorialStep, TutorialStepDto>();

            // Map UserCompletedTutorial to UserTutorialProgressDto
            CreateMap<UserTutorialProgressDto, UserCompletedTutorial>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.RoleId))
                .ForMember(dest => dest.StepId, opt => opt.MapFrom(src => src.StepId))
                .ForMember(dest => dest.StepGroupId, opt => opt.MapFrom(src => src.StepGroupId))
                .ForMember(dest => dest.StepGroupName, opt => opt.MapFrom(src => src.StepGroupName ?? null))
                .ForMember(dest => dest.CompletedAt, opt => opt.MapFrom(src => src.CompletedAt))
                .ForMember(dest => dest.IsReset, opt => opt.MapFrom(src => src.IsReset))
                .ReverseMap();
        }
    }
}
