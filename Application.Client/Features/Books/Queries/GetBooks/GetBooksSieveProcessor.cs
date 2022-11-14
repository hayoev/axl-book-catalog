using Domain.Entities.Books;
using Microsoft.Extensions.Options;
using Sieve.Models;
using Sieve.Services;

namespace Application.Client.Features.Books.Queries.GetBooks
{
    public class GetBooksSieveProcessor : SieveProcessor
    {
        public GetBooksSieveProcessor(IOptions<SieveOptions> options) : base(options)
        {
        }

        protected override SievePropertyMapper MapProperties(SievePropertyMapper mapper)
        {
            mapper.Property<Book>(p => p.Id)
                .CanFilter().CanSort();
            mapper.Property<Book>(p => p.Name)
                .CanFilter().CanSort();
            mapper.Property<Book>(p => p.Author.Fullname)
                .CanFilter().CanSort().HasName("AuthorName");

            return mapper;
        }
    }
}