using Application.Client.Features.Authors.Queries.GetAuthors;
using Application.Client.Features.Books.Queries.GetBooks;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Client.Common.Extensions
{
    public static class SieveProcessorExtension
    {
        public static IServiceCollection AddSieveProcessors(this IServiceCollection sieveProcessors)
        {
            sieveProcessors.AddScoped<GetBooksSieveProcessor>();
            sieveProcessors.AddScoped<GetAuthorsSieveProcessor>();

            return sieveProcessors;
        }
    }
}
