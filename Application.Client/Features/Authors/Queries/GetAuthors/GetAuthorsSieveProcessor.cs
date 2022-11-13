using Domain.Entities.Authors;
using Microsoft.Extensions.Options;
using Sieve.Models;
using Sieve.Services;

namespace Application.Client.Features.Authors.Queries.GetAuthors
{
    public class GetAuthorsSieveProcessor : SieveProcessor
    {
        public GetAuthorsSieveProcessor(IOptions<SieveOptions> options) : base(options)
        {
        }

        protected override SievePropertyMapper MapProperties(SievePropertyMapper mapper)
        {
            mapper.Property<Author>(p => p.Id)
                .CanFilter().CanSort();
            mapper.Property<Author>(p => p.Fullname)
                .CanFilter().CanSort();
            return mapper;
        }
    }
}