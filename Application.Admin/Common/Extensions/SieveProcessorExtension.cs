using Application.Admin.Features.Authors.Queries.GetAuthors;
using Application.Admin.Features.Books.Queries.GetBooks;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Admin.Common.Extensions
{
    public static class SieveProcessorExtension
    {
        public static IServiceCollection AddSieveProcessors(this IServiceCollection sieveProcessors)
        {
            sieveProcessors.AddScoped<GetAuthorsSieveProcessor>();
            sieveProcessors.AddScoped<GetBooksSieveProcessor>();

            return sieveProcessors;
        }
    }
}
