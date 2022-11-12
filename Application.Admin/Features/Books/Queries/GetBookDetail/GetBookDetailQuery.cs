using System;
using MediatR;

namespace Application.Admin.Features.Books.Queries.GetBookDetail
{
    public class GetBookDetailQuery : IRequest<GetBookDetailViewModel>
    {
        public Guid Id { get; set; }
    }
}