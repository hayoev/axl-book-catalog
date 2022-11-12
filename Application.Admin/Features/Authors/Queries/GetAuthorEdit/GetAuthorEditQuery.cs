using System;
using MediatR;

namespace Application.Admin.Features.Authors.Queries.GetAuthorEdit
{
    public class GetAuthorEditQuery : IRequest<GetAuthorEditViewModel>
    {
        public Guid Id { get; set; }
    }
}