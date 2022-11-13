using Application.Client.Common.Extensions;
using MediatR;
using Sieve.Models;

namespace Application.Client.Features.Authors.Queries.GetAuthors
{
    public class GetAuthorsQuery: IRequest<PaginateResult<GetAuthorsViewModel>>
    {
        public int Page { get; set; }
        public SieveModel SieveModel { get; set; }
    }
}