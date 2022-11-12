using AutoMapper;
using Domain.Entities.Authors;

namespace Application.Admin.Features.Authors.Commands.UpdateAuthor
{
    public class UpdateAuthorMapper: Profile
    {
        public UpdateAuthorMapper()
        {
            CreateMap<UpdateAuthorCommand, Author>();
        }
    }
}