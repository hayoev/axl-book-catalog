using AutoMapper;
using Domain.Entities.Authors;

namespace Application.Client.Features.Authors.Queries.GetAuthors
{
    public class GetAuthorsMapper : Profile
    {
        public GetAuthorsMapper()
        {
            CreateMap<Author, GetAuthorsViewModel>();
        }
    }
}