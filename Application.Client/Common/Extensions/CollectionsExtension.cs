using System.Collections.Generic;
using System.Linq;

namespace Application.Client.Common.Extensions
{
    public static class CollectionsExtension
    {
        public static PaginateResult<T> PaginateResult<T>(this ICollection<T> collection,
            int currentPage = 1, int pageSize = 15)
            where T : class
        {
            currentPage = currentPage <= 0 ? 1 : currentPage;

            var count = collection.Count;
            var pagination = new PaginateParam(count, currentPage, pageSize);

            var skip = pageSize * (currentPage - 1);
            var list = collection.Skip(skip).Take(pageSize).ToList();
            return new PaginateResult<T>() {Pagination = pagination, Items = list};
        }
    }
}