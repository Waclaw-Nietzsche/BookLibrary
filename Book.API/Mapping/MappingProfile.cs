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
            CreateMap<BookModel, SaveBookResource>();
            CreateMap<AuthorModel, SaveAuthorResource>();
            
            // Resource to Domain
            CreateMap<BookResource, BookModel>();
            CreateMap<AuthorResource, AuthorModel>();
            CreateMap<SaveBookResource, BookModel>();
            CreateMap<SaveAuthorResource, AuthorModel>();

        }
    }
}