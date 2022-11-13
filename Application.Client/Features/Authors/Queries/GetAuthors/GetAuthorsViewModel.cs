using System;

namespace Application.Client.Features.Authors.Queries.GetAuthors
{
    public class GetAuthorsViewModel
    {
        public Guid Id { get; set; }
        public string Fullname { get; set; }
        public string Bio { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime? DeathDate { get; set; }
    }
}