using AutoMapper;
using Domain.Entities.Authors;

namespace Application.Admin.Features.Authors.Queries.GetAuthorEdit
{
    public class GetAuthorEditMapper : Profile
    {
        public GetAuthorEditMapper()
        {
            CreateMap<Author, GetAuthorEditViewModel>();
        }
    }
}