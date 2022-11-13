using System;
using MediatR;

namespace Application.Client.Features.Authors.Queries.GetAuthorDetail
{
    public class GetAuthorDetailQuery : IRequest<GetAuthorDetailViewModel>
    {
        public Guid Id { get; set; }
    }
}