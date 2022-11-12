using System;

namespace Application.Admin.Features.Authors.Queries.GetAuthorEdit
{
    public class GetAuthorEditViewModel
    {
        public Guid Id { get; set; }
        public string Fullname { get; set; }
        public string Bio { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime? DeathDate { get; set; }
    }
}