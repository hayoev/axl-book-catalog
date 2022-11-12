using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Application.Admin.Common.Extensions
{
    public static class QueryableExtension
    {
        public static async Task<PaginateResult<T>> PaginateResultAsync<T>(this IQueryable<T> query,
            int currentPage = 1, int pageSize = 15, CancellationToken cancellationToken = default(CancellationToken))
            where T : class
        {
            currentPage = currentPage <= 0 ? 1 : currentPage;

            var count = await query.CountAsync(cancellationToken);
            var pagination = new PaginateParam(count, currentPage, pageSize);

            var skip = pageSize * (currentPage - 1);
            var list = await query.Skip(skip).Take(pageSize).ToListAsync(cancellationToken);
            return new PaginateResult<T>() { Pagination = pagination, Items = list };
        }
    }
}