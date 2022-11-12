using System.Collections.Generic;

 namespace Application.Admin.Common.Extensions
{
    public class PaginateResult<T>
    {
        public List<T> Items { get; set; }
        public PaginateParam Pagination { get; set; }
    }
}