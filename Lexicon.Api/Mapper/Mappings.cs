using AutoMapper;
using Lexicon.Api.Dtos.ActivityDtos;
using Lexicon.Api.Dtos.CourseDtos;
using Lexicon.Api.Dtos.DocumentDtos;
using Lexicon.Api.Dtos.ModuleDtos;
using Lexicon.Api.Dtos.UserDtos;
using Lexicon.Api.Entities;

namespace Lexicon.Api.Mapper;

public class Mappings : Profile
{
    public Mappings()
    {
            // Document mapper
            CreateMap<DocumentDto, Document>();
            CreateMap<Document, DocumentDto>();
            CreateMap<Document, DocumentPostDto>();
            CreateMap<DocumentPostDto, Document>();

            // User mapper
            CreateMap<UserDto, User>();
            CreateMap<User, UserDto>();
            CreateMap<User, UserPostDto>();
            CreateMap<UserPostDto, User>();
            CreateMap<UserWithIdDto, User>();
            CreateMap<User, UserWithIdDto>();

            // Course mapper
            CreateMap<Course, CourseDto>()
                .ForMember(dest => dest.UserIds, opt => opt.MapFrom(src => src.Users.Select(u => u.UserId)))
                .ForMember(dest => dest.DocumentIds, opt => opt.MapFrom(src => src.Documents.Select(d => d.DocumentId)))
                .ForMember(dest => dest.ModuleIds, opt => opt.MapFrom(src => src.Modules.Select(d => d.ModuleId)));
            CreateMap<CourseDto, Course>()
                .ForMember(dest => dest.Users, opt => opt.Ignore())
                .ForMember(dest => dest.Documents, opt => opt.Ignore())
                .ForMember(dest => dest.Modules, opt => opt.Ignore());  
            CreateMap<Course, CoursePostDto>();
            CreateMap<CoursePostDto, Course>();
            CreateMap<Course, CourseWithIdDto>();
            CreateMap<CourseWithIdDto, Course>();

        // Activity mapper
        CreateMap<ActivityDto, Activity>();
            CreateMap<Activity, ActivityDto>();
            CreateMap<Activity, ActivityPostDto>();
            CreateMap<ActivityPostDto, Activity>();

            // Module mapper
            CreateMap<Module, ModuleDto>()
                .ForMember(dest => dest.ActivityIds, opt => opt.MapFrom(src => src.Activities.Select(a => a.ActivityId)))
                .ForMember(dest => dest.DocumentIds, opt => opt.MapFrom(src => src.Documents.Select(a => a.DocumentId)));
            CreateMap<ModuleDto, Module>()
                .ForMember(dest => dest.Activities, opt => opt.Ignore());
            CreateMap<Module, ModulePostDto>();
            CreateMap<ModulePostDto, Module>();
        }

}