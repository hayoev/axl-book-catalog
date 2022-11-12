using System;
using MediatR;

namespace Application.Admin.Features.Books.Queries.GetBookEdit
{
    public class GetBookEditQuery : IRequest<GetBookEditViewModel>
    {
        public Guid Id { get; set; }
    }
}