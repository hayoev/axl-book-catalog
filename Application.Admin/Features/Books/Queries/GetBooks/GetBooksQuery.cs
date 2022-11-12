using Application.Admin.Common.Extensions;
using MediatR;
using Sieve.Models;

namespace Application.Admin.Features.Books.Queries.GetBooks
{
    public class GetBooksQuery: IRequest<PaginateResult<GetBooksViewModel>>
    {
        public int Page { get; set; }
        public SieveModel SieveModel { get; set; }
    }
}