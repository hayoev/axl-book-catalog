using AutoMapper;
using Domain.Entities.Books;

namespace Application.Client.Features.Books.Queries.GetBookDetail
{
    public class GetBookDetailMapper : Profile
    {
        public GetBookDetailMapper()
        {
            
            CreateMap<BookCategory, CategoryDto>()
                .ForMember(x => x.Name, opt =>
                    opt.MapFrom(x => x.Category.Name));
            
            CreateMap<Book, GetBookDetailViewModel>()
                .ForMember(dto => dto.Categories, opt => opt.MapFrom(x => x.BookCategories))
                .ForMember(x => x.AuthorName, opt =>
                    opt.MapFrom(x => x.Author.Fullname));
        }
    }
}