using System.Collections.Generic;

namespace Application.Client.Common.Extensions
{
    public class PaginateResult<T>
    {
        public List<T> Items { get; set; }
        public PaginateParam Pagination { get; set; }
    }
}