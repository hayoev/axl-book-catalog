using AutoMapper;
using Domain.Entities.Books;

namespace Application.Admin.Features.Books.Commands.AddBookToCategory
{
    public class AddBookToCategoryMapper: Profile
    {
        public AddBookToCategoryMapper()
        {
            CreateMap<AddBookToCategoryCommand, Book>();
        }
    }
}