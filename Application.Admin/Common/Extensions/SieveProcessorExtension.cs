using Application.Admin.Features.Authors.Queries.GetAuthors;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Admin.Common.Extensions
{
    public static class SieveProcessorExtension
    {
        public static IServiceCollection AddSieveProcessors(this IServiceCollection sieveProcessors)
        {
            sieveProcessors.AddScoped<GetAuthorsSieveProcessor>();

            return sieveProcessors;
        }
    }
}
