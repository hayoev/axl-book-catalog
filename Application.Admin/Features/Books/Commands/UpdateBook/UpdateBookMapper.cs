using AutoMapper;
using Domain.Entities.Books;

namespace Application.Admin.Features.Books.Commands.UpdateBook
{
    public class UpdateBookMapper: Profile
    {
        public UpdateBookMapper()
        {
            CreateMap<UpdateBookCommand, Book>();
        }
    }
}