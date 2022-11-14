using System;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Application.Admin.Features.Books.Commands.CreateBook
{
    public class CreateBookCommand : IRequest<Guid>
    {
        public string Name { get; set; }
        
        [FromForm(Name = "cover")]
        public IFormFile Cover { get; set; }
        //public string Cover { get; set; }
        public string Description { get; set; }
        public Guid AuthorId { get; set; }
        public short PublishYear { get; set; }
        public int PageCount { get; set; }
    }
}