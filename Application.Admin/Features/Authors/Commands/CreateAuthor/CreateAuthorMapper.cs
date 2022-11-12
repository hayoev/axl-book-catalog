using AutoMapper;
using Domain.Entities.Authors;

namespace Application.Admin.Features.Authors.Commands.CreateAuthor
{
    public class CreateAuthorMapper: Profile
    {
        public CreateAuthorMapper()
        {
            CreateMap<CreateAuthorCommand, Author>();
        }
    }
}