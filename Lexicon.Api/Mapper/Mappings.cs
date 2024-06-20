using AutoMapper;
using Lexicon.Api.Dtos.ActivityDtos;
using Lexicon.Api.Dtos.CourseDtos;
using Lexicon.Api.Dtos.DocumentDtos;
using Lexicon.Api.Dtos.ModuleDtos;
using Lexicon.Api.Dtos.UserDtos;
using Lexicon.Api.Entities;

namespace Lexicon.Api.Mapper
{
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

            // Course mapper
            CreateMap<CourseDto, Course>();
            CreateMap<Course, CourseDto>();
            CreateMap<Course, CoursePostDto>();
            CreateMap<CoursePostDto, Course>();

            // Activity mapper
            CreateMap<ActivityDto, Activity>();
            CreateMap<Activity, ActivityDto>();
            CreateMap<Activity, ActivityPostDto>();
            CreateMap<ActivityPostDto, Activity>();


            // Module mapper
            CreateMap<ModuleDto, Module>();
            CreateMap<Module, ModuleDto>();
            CreateMap<Module, ModulePostDto>();
            CreateMap<ModulePostDto, Module>();
        }

    }
}
