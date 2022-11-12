using AutoMapper;
using Domain.Entities.Books;

namespace Application.Admin.Features.Books.Queries.GetBookEdit
{
    public class GetBookEditMapper : Profile
    {
        public GetBookEditMapper()
        {
            CreateMap<Book, GetBookEditViewModel>();
        }
    }
}