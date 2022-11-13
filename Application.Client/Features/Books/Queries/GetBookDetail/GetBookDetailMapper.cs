using AutoMapper;
using Domain.Entities.Books;

namespace Application.Client.Features.Books.Queries.GetBookDetail
{
    public class GetBookDetailMapper : Profile
    {
        public GetBookDetailMapper()
        {
            CreateMap<Book, GetBookDetailViewModel>()
                .ForMember(x => x.AuthorName, opt =>
                    opt.MapFrom(x => x.Author.Fullname));
        }
    }
}