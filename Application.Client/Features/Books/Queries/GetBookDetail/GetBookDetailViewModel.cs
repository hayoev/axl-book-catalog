using System;

namespace Application.Client.Features.Books.Queries.GetBookDetail
{
    public class GetBookDetailViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Cover { get; set; }
        public string Description { get; set; }
        public string AuthorName { get; set; }
        public short PublishYear { get; set; }
        public int PageCount { get; set; }
        public string CategoryName { get; set; }
    }
}