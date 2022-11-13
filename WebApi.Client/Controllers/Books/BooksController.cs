using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Client.Features.Books.Queries.GetBookDetail;
using Application.Client.Features.Books.Queries.GetBooks;
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
        [Authorize]
        [ProducesResponseType(typeof(SuccessResponse<GetBookDetailViewModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetById(Guid bookId)
        {
            var data = await Mediator.Send(new GetBookDetailQuery()
            {
                Id = bookId
            });
            return Ok(new SuccessResponse<GetBookDetailViewModel>(data));
        }
    }
}