using System;

namespace Application.Client.Features.Books.Queries.GetBooks
{
    public class GetBooksViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Cover { get; set; }
        public string Description { get; set; }
        public string AuthorName { get; set; }
        public short PublishYear { get; set; }
        public int PageCount { get; set; }
    }
}