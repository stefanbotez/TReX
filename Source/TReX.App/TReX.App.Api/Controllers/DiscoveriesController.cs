using System;
using System.Threading.Tasks;
using EnsureThat;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TReX.App.Api.Models;
using TReX.App.Business.Discovery.Commands;
using TReX.Kernel.Shared.Domain;

namespace TReX.App.Api.Controllers
{
    [Route("api/v1/discoveries")]
    public sealed class DiscoveriesController : ControllerBase
    {
        private Behalf mockBehalf = Behalf.Create(new Guid("B8F42E6C-3EB1-4A2B-A600-0EA7730909A5")).Value;
        private readonly IMediator mediator;

        public DiscoveriesController(IMediator mediator)
        {
            EnsureArg.IsNotNull(mediator);
            this.mediator = mediator;
        }

        [HttpPost("")]
        public async Task<IActionResult> StartDiscovery([FromBody] StartDiscoveryModel model)
        {
            var command = new StartDiscoveryCommand(model.Topic, mockBehalf);
            var result = await this.mediator.Send(command);

            if (result.IsSuccess)
            {
                return StatusCode(StatusCodes.Status201Created, result);
            }

            return BadRequest(result);
        }
    }
}