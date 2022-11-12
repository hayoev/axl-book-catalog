using AutoMapper;
using Domain.Entities.Books;

namespace Application.Admin.Features.Books.Commands.CreateBook
{
    public class CreateBookMapper: Profile
    {
        public CreateBookMapper()
        {
            CreateMap<CreateBookCommand, Book>();
        }
    }
}