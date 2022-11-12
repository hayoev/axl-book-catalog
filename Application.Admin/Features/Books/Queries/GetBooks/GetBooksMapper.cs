using AutoMapper;
using Domain.Entities.Books;

namespace Application.Admin.Features.Books.Queries.GetBooks
{
    public class GetBooksMapper : Profile
    {
        public GetBooksMapper()
        {
            CreateMap<Book, GetBooksViewModel>()
                .ForMember(x => x.AuthorName, opt =>
                    opt.MapFrom(x => x.Author.Fullname));
        }
    }
}