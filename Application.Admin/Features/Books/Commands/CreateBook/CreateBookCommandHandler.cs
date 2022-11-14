using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Admin.Common.Interfaces;
using Application.Admin.Services.FileUploader;
using Application.Admin.Services.FileUploader.Configuration;
using AutoMapper;
using Domain.Entities.Books;
using MediatR;

namespace Application.Admin.Features.Books.Commands.CreateBook
{
    public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, Guid>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IFileUploaderService _fileUploaderService;
        private readonly FileUploadConfiguration _fileUploadConfiguration;

        public CreateBookCommandHandler(IApplicationDbContext dbContext, IMapper mapper, IFileUploaderService fileUploaderService, FileUploadConfiguration fileUploadConfiguration)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _fileUploaderService = fileUploaderService;
            _fileUploadConfiguration = fileUploadConfiguration;
        }

        public async Task<Guid> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            var book = _mapper.Map<Book>(request);
            book.Id = Guid.NewGuid();
            book.Cover = await _fileUploaderService.UploadAsync(request.Cover, _fileUploadConfiguration.Folder);
            _dbContext.Books.Add(book);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return book.Id;
        }
    }
}