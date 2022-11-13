using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Client.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CustomControllerBase : ControllerBase
    {
        private IMediator _mediator;

        protected IMediator Mediator =>
            _mediator ??= (IMediator)HttpContext.RequestServices.GetService(typeof(IMediator));
    }
}