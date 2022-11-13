using System;

namespace Application.Client.Common.Extensions
{
    public class PaginateParam
    {
        public PaginateParam(int totalItems, int pageNumber, int pageSize)
        {
            TotalItems = totalItems;
            PageNumber = pageNumber;
            PageSize = pageSize;
        }

        public int PageNumber { get; }

        public int TotalPages => (int)Math.Ceiling(this.TotalItems / (double)this.PageSize);

        public int PageSize { get; }

        public int TotalItems { get; }

        public bool HasPreviousPage => PageNumber > 1;
        public bool HasNextPage => PageNumber < TotalPages;

        // public int NextPageNumber => this.HasNextPage ? this.PageNumber + 1 : this.TotalPages;

        // public int PreviousPageNumber => this.HasPreviousPage ? this.PageNumber - 1 : 1;
    }
}