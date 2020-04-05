using AutoMapper;
using Book.API.Resources;
using Book.Core.Models;

namespace Book.API.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Domain to Resource
            CreateMap<BookModel, BookResource>();
            CreateMap<AuthorModel, AuthorResource>();
            
            // Resource to Domain
            CreateMap<BookResource, BookModel>();
            CreateMap<AuthorResource, AuthorModel>();
        }
    }
}