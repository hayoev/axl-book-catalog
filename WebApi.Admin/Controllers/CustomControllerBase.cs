using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Admin.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize] //TODO release Authorize
    public class CustomControllerBase : ControllerBase
    {
        private IMediator _mediator;

        protected IMediator Mediator =>
            _mediator ??= (IMediator)HttpContext.RequestServices.GetService(typeof(IMediator));
    }
}