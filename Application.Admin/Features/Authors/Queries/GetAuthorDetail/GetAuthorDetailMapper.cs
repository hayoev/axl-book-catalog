using AutoMapper;
using Domain.Entities.Authors;

namespace Application.Admin.Features.Authors.Queries.GetAuthorDetail
{
    public class GetAuthorDetailMapper : Profile
    {
        public GetAuthorDetailMapper()
        {
            CreateMap<Author, GetAuthorDetailViewModel>();
        }
    }
}