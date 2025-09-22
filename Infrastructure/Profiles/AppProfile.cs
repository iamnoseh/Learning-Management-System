using AutoMapper;
using Domain.Dtos.Courses;
using Domain.Dtos.Users;
using Domain.Entities;

namespace Infrastructure.Profiles;

public class AppProfile : Profile
{
    public AppProfile()
    {
        CreateMap<UserCreateDto, User>()
            .ForMember(d => d.Birthday, o => o.MapFrom(s => DateOnly.FromDateTime(s.Birthday)))
            .ForMember(d => d.CreatedAt, o => o.Ignore())
            .ForMember(d => d.UpdatedAt, o => o.Ignore())
            .ForMember(d => d.Id, o => o.Ignore());

        CreateMap<UserUpdateDto, User>()
            .ForMember(d => d.Birthday, o => o.MapFrom(s => DateOnly.FromDateTime(s.Birthday)))
            .ForMember(d => d.UserName, o => o.Ignore())
            .ForMember(d => d.Id, o => o.Ignore());
        
        CreateMap<User, UserResultDto>();


        CreateMap<CourseCreateDto, Course>()
            .ForMember(d => d.Id, o => o.Ignore())
            .ForMember(d => d.CreatedById, o => o.Ignore())
            .ForMember(d => d.CreatedAt, o => o.Ignore());

        CreateMap<CourseUpdateDto, Course>()
            .ForMember(d => d.Id, o => o.Ignore())
            .ForMember(d => d.CreatedById, o => o.Ignore())
            .ForMember(d => d.CreatedAt, o => o.Ignore());

        CreateMap<Course, CourseResultDto>();
    }
}


