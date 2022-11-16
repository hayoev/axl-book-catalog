using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Admin.Common.Interfaces;
using Infrastructure.Persistence.Seeders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Admin.Common.Responses;

namespace WebApi.Admin.Controllers.Seeds
{
    public class SeedsController : CustomControllerBase
    {
        private readonly ISeeder _seeder;

        public SeedsController(ISeeder seeder)
        {
            _seeder = seeder;
        }
        
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(SuccessResponse<Dictionary<string,string>>),StatusCodes.Status200OK)]
        public async Task<IActionResult> Start(SeedersRequest seedersRequest)
        {
            await _seeder.Start(seedersRequest.IsForceUpdate);
            return Ok(new SuccessResponse<Dictionary<string,string>>(_seeder.GetMessages()));
        }
    }
}