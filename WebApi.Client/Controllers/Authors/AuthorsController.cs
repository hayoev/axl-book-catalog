using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Client.Features.Authors.Queries.GetAuthorDetail;
using Application.Client.Features.Authors.Queries.GetAuthors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sieve.Models;
using WebApi.Client.Common.Responses;

namespace WebApi.Client.Controllers.Authors
{
    public class AuthorsController : CustomControllerBase
    {
        [HttpGet]
        [Authorize]
        [ProducesResponseType(typeof(SuccessResponse<List<GetAuthorsViewModel>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Index([FromQuery] SieveModel request)
        {
            var data = await Mediator.Send(new GetAuthorsQuery()
            {
                SieveModel = request,
                Page = request.Page ?? 0
            });

            var meta = new Meta()
            {
                Pagination = data.Pagination
            };

            return Ok(new SuccessResponse<List<GetAuthorsViewModel>>(data.Items, meta: meta));
        }

        [HttpGet("{authorId:guid}")]
        [Authorize]
        [ProducesResponseType(typeof(SuccessResponse<GetAuthorDetailViewModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetById(Guid authorId)
        {
            var data = await Mediator.Send(new GetAuthorDetailQuery()
            {
                Id = authorId
            });
            return Ok(new SuccessResponse<GetAuthorDetailViewModel>(data));
        }
    }
}