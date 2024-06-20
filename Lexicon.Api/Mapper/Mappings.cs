using AutoMapper;
using Lexicon.Api.Dtos.DocumentDtos;
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
        }

    }
}
