using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Admin.Features.Authors.Commands.CreateAuthor;
using Application.Admin.Features.Authors.Commands.DeleteAuthor;
using Application.Admin.Features.Authors.Commands.UpdateAuthor;
using Application.Admin.Features.Authors.Queries.GetAuthorDetail;
using Application.Admin.Features.Authors.Queries.GetAuthorEdit;
using Application.Admin.Features.Authors.Queries.GetAuthors;
using Domain.Enums.AdminUsers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sieve.Models;
using WebApi.Admin.Common.Responses;

namespace WebApi.Admin.Controllers.Authors
{
    public class AuthorsController : CustomControllerBase
    {
        [HttpGet]
        [Authorize(nameof(AdminPermissionEnum.AuthorList))]
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
        [Authorize(nameof(AdminPermissionEnum.AuthorDetail))]
        [ProducesResponseType(typeof(SuccessResponse<GetAuthorDetailViewModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetById(Guid authorId)
        {
            var data = await Mediator.Send(new GetAuthorDetailQuery()
            {
                Id = authorId
            });
            return Ok(new SuccessResponse<GetAuthorDetailViewModel>(data));
        }

        [HttpPost("create")]
        [Authorize(nameof(AdminPermissionEnum.AuthorCreate))]
        public async Task<IActionResult> Create(CreateAuthorCommand request)
        {
            var id = await Mediator.Send(request);

            return Ok(new SuccessResponse<Guid>(id));
        }

        [HttpGet("{authorId:guid}/edit")]
        [Authorize(nameof(AdminPermissionEnum.AuthorEdit))]
        [ProducesResponseType(typeof(SuccessResponse<GetAuthorEditViewModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetForEdit(Guid authorId)
        {
            var data = await Mediator.Send(new GetAuthorEditQuery()
            {
                Id = authorId
            });
            return Ok(new SuccessResponse<GetAuthorEditViewModel>(data));
        }

        [HttpPut("edit")]
        [Authorize(nameof(AdminPermissionEnum.AuthorEdit))]
        [ProducesResponseType(typeof(SuccessResponse<Guid>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Update(UpdateAuthorCommand request)
        {
            var id = await Mediator.Send(request);
            return Ok(new SuccessResponse<Guid>(id));
        }

        [HttpDelete("delete")]
        [Authorize(nameof(AdminPermissionEnum.AuthorDelete))]
        [ProducesResponseType(typeof(SuccessResponse<Guid>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(DeleteAuthorCommand request)
        {
            var id = await Mediator.Send(request);
            return Ok(new SuccessResponse<Guid>(id));
        }
    }
}