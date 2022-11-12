using System;
using MediatR;

namespace Application.Admin.Features.Authors.Commands.UpdateAuthor
{
    public class UpdateAuthorCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public string Fullname { get; set; }
        public string Bio { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime? DeathDate { get; set; }
    }
}