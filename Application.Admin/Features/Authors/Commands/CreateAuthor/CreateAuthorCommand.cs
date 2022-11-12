using System;
using MediatR;

namespace Application.Admin.Features.Authors.Commands.CreateAuthor
{
    public class CreateAuthorCommand : IRequest<Guid>
    {
        public string Fullname { get; set; }
        public string Bio { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime? DeathDate { get; set; }
    }
}