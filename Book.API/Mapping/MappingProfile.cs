using AutoMapper;
using Book.API.Resources;
using Book.Core.Models;

namespace Book.API.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Domain to Resource & Resource to Domain
            CreateMap<BookModel, BookResource>().ReverseMap();
            CreateMap<AuthorModel, AuthorResource>().ReverseMap();
            CreateMap<BookModel, SaveBookResource>().ReverseMap();
            CreateMap<AuthorModel, SaveAuthorResource>().ReverseMap();
        }
    }
}