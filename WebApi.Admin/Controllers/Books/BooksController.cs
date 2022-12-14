using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Admin.Features.Books.Commands.AddBookToCategory;
using Application.Admin.Features.Books.Commands.CreateBook;
using Application.Admin.Features.Books.Commands.DeleteBook;
using Application.Admin.Features.Books.Commands.UpdateBook;
using Application.Admin.Features.Books.Queries.GetBookDetail;
using Application.Admin.Features.Books.Queries.GetBookEdit;
using Application.Admin.Features.Books.Queries.GetBooks;
using Domain.Enums.AdminUsers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sieve.Models;
using WebApi.Admin.Common.Responses;

namespace WebApi.Admin.Controllers.Books
{
    public class BooksController : CustomControllerBase
    {
        [HttpGet]
        [Authorize(nameof(AdminPermissionEnum.BookList))]
        [ProducesResponseType(typeof(SuccessResponse<List<GetBooksViewModel>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Index([FromQuery] SieveModel request)
        {
            var data = await Mediator.Send(new GetBooksQuery()
            {
                SieveModel = request,
                Page = request.Page ?? 0
            });

            var meta = new Meta()
            {
                Pagination = data.Pagination
            };

            return Ok(new SuccessResponse<List<GetBooksViewModel>>(data.Items, meta: meta));
        }

        [HttpGet("{bookId:guid}")]
        [Authorize(nameof(AdminPermissionEnum.BookDetail))]
        [ProducesResponseType(typeof(SuccessResponse<GetBookDetailViewModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetById(Guid bookId)
        {
            var data = await Mediator.Send(new GetBookDetailQuery()
            {
                Id = bookId
            });
            return Ok(new SuccessResponse<GetBookDetailViewModel>(data));
        }


        [HttpPost("create")]
        [Authorize(nameof(AdminPermissionEnum.BookCreate))]
        public async Task<IActionResult> Create([FromForm] CreateBookCommand request)
        {
            var id = await Mediator.Send(request);

            return Ok(new SuccessResponse<Guid>(id));
        }

        [HttpGet("{bookId:guid}/edit")]
        [Authorize(nameof(AdminPermissionEnum.BookEdit))]
        [ProducesResponseType(typeof(SuccessResponse<GetBookEditViewModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetForEdit(Guid bookId)
        {
            var data = await Mediator.Send(new GetBookEditQuery()
            {
                Id = bookId
            });
            return Ok(new SuccessResponse<GetBookEditViewModel>(data));
        }

        [HttpPut("edit")]
        [Authorize(nameof(AdminPermissionEnum.BookEdit))]
        [ProducesResponseType(typeof(SuccessResponse<Guid>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Update(UpdateBookCommand request)
        {
            var id = await Mediator.Send(request);
            return Ok(new SuccessResponse<Guid>(id));
        }

        [HttpDelete("delete")]
        //[Authorize(nameof(AdminPermissionEnum.BookEdit))]
        [ProducesResponseType(typeof(SuccessResponse<Guid>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(DeleteBookCommand request)
        {
            var id = await Mediator.Send(request);
            return Ok(new SuccessResponse<Guid>(id));
        }
        
        
        [HttpPost("add-to-category")]
        [Authorize(nameof(AdminPermissionEnum.BookEdit))]
        [ProducesResponseType(typeof(SuccessResponse<Guid>), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddToCategory(AddBookToCategoryCommand request)
        {
            var id = await Mediator.Send(request);
            return Ok(new SuccessResponse<Guid>(id));
        }

    }
}