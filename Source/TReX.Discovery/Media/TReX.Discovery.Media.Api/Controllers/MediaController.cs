using System.Threading.Tasks;
using EnsureThat;
using Microsoft.AspNetCore.Mvc;
using TReX.Discovery.Media.Business.Discovery;

namespace TReX.Discovery.Media.Api.Controllers
{
    [Route("api/v1/media")]
    public sealed class MediaController : ControllerBase
    {
        private readonly IMediaDiscoveryService discoveryService;

        public MediaController(IMediaDiscoveryService discoveryService)
        {
            EnsureArg.IsNotNull(discoveryService);
            this.discoveryService = discoveryService;
        }

        [HttpPost("discovery")]
        public async Task<IActionResult> StartDiscovery([FromQuery] string topic)
        {
            var result = await this.discoveryService.Discover(topic);
            if (result.IsSuccess)
            {
                return Ok();
            }

            return BadRequest(result.Error);
        }
    }
}