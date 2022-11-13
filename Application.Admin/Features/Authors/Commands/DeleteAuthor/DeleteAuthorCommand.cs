using System;
using MediatR;

namespace Application.Admin.Features.Authors.Commands.DeleteAuthor
{
    public class DeleteAuthorCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
    }
}