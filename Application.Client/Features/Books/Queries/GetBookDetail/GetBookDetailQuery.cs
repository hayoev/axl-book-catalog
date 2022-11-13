using System;
using MediatR;

namespace Application.Client.Features.Books.Queries.GetBookDetail
{
    public class GetBookDetailQuery : IRequest<GetBookDetailViewModel>
    {
        public Guid Id { get; set; }
    }
}