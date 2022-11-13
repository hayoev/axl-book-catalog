using System.Threading.Tasks;
using Application.Client.Features.Identities.Commands.Authenticate;
using Application.Client.Features.Identities.Commands.RefreshTokens;
using Application.Client.Features.Identities.Commands.RevokeRefreshToken;
using Application.Client.Features.Identities.Commands.SetPassword;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Client.Common.Responses;

namespace WebApi.Client.Controllers.Identities
{
    public class IdentitiesController : CustomControllerBase
    {
        [AllowAnonymous]
        [HttpPost("authenticate")]
        [ProducesResponseType(typeof(SuccessResponse<AuthenticateViewModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult> Authenticate(AuthenticateCommand request)
        {
            var data = await Mediator.Send(request);
            return Ok(new SuccessResponse<AuthenticateViewModel>(data));
        }

        [AllowAnonymous]
        [HttpPost("refresh")]
        [ProducesResponseType(typeof(SuccessResponse<RefreshTokensViewModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult> Refresh(RefreshTokensCommand request)
        {
            var data = await Mediator.Send(request);
            return Ok(new SuccessResponse<RefreshTokensViewModel>(data));
        }

        [HttpPost("logout")]
        [ProducesResponseType(typeof(SuccessResponse<bool>), StatusCodes.Status200OK)]
        public async Task<ActionResult> Logout(RevokeRefreshTokenCommand request)
        {
            var result = await Mediator.Send(request);
            return Ok(new SuccessResponse<bool>(result));
        }
        
        [AllowAnonymous]
        [HttpPost("set-password")]
        [ProducesResponseType(typeof(SuccessResponse<bool>), StatusCodes.Status200OK)]
        public async Task<IActionResult> SetPassword([FromBody] SetPasswordCommand request)
        {
            var data = await Mediator.Send(request);
            return Ok(new SuccessResponse<bool>(data));
        }
    }
}