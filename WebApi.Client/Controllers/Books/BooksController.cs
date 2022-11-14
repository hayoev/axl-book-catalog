using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Application.Client.Common.Configurations;
using Application.Client.Features.Books.Queries.GetBookDetail;
using Application.Client.Features.Books.Queries.GetBooks;
using Application.Client.Features.Users.Commands.AddBookToBookshelf;
using Application.Client.Features.Users.Commands.RemoveBookFromBookshelf;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sieve.Models;
using WebApi.Client.Common.Responses;

namespace WebApi.Client.Controllers.Books
{
    public class BooksController : CustomControllerBase
    {
        [HttpGet]
        [Authorize]
        [ProducesResponseType(typeof(SuccessResponse<List<GetBooksViewModel>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Index([FromServices] FileUploadConfiguration fileUploadConfiguration,
            [FromQuery] SieveModel request)
        {
            var data = await Mediator.Send(new GetBooksQuery()
            {
                SieveModel = request,
                Page = request.Page ?? 0
            });

            var meta = new Meta()
            {
                Pagination = data.Pagination,
                BooksPath = Path.Combine(fileUploadConfiguration.HostAddress, fileUploadConfiguration.Folder)
            };

            return Ok(new SuccessResponse<List<GetBooksViewModel>>(data.Items, meta: meta));
        }

        [HttpGet("{bookId:guid}")]
        [Authorize]
        [ProducesResponseType(typeof(SuccessResponse<GetBookDetailViewModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetById([FromServices] FileUploadConfiguration fileUploadConfiguration,
            Guid bookId)
        {
            var data = await Mediator.Send(new GetBookDetailQuery()
            {
                Id = bookId
            });

            var meta = new Meta()
            {
                BooksPath = Path.Combine(fileUploadConfiguration.HostAddress, fileUploadConfiguration.Folder)
            };
            return Ok(new SuccessResponse<GetBookDetailViewModel>(data, meta: meta));
        }
        
        [HttpPost("add-to-bookshelf")]
        [Authorize]
        public async Task<IActionResult> Create(AddBookToBookshelfCommand request)
        {
            var id = await Mediator.Send(request);

            return Ok(new SuccessResponse<Guid>(id));
        } 
        
        [HttpPost("remove-from-bookshelf")]
        [Authorize]
        public async Task<IActionResult> Create(RemoveBookFromBookshelfCommand request)
        {
            var id = await Mediator.Send(request);

            return Ok(new SuccessResponse<Guid>(id));
        }
    }
}