using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Admin.Features.Authors.Commands.CreateAuthor;
using Application.Admin.Features.Authors.Queries.GetAuthors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sieve.Models;
using WebApi.Admin.Common.Responses;

namespace WebApi.Admin.Controllers.Authors
{
    public class AuthorsController : CustomControllerBase
    {
        [HttpGet]
        //[Authorize(nameof(AdminPermissionEnum.AuthorList))]
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

        [HttpPost("create")]
        //[Authorize(nameof(AdminPermissionEnum.CreateAuthor))]
        public async Task<IActionResult> Create(CreateAuthorCommand request)
        {
            var id = await Mediator.Send(request);

            return Ok(new SuccessResponse<Guid>(id));
        }
    }
}