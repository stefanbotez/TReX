using System;
using System.Threading.Tasks;
using EnsureThat;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TReX.App.Api.Models;
using TReX.App.Business.Resources.Queries;

namespace TReX.App.Api.Controllers
{
    [Route("/api/v1/resources")]
    public sealed class ResourcesController : ControllerBase
    {
        private readonly IMediator mediator;

        public ResourcesController(IMediator mediator)
        {
            EnsureArg.IsNotNull(mediator);
            this.mediator = mediator;
        }

        [HttpGet("list")]
        public async Task<IActionResult> ListResources()
        {
            var query = new ListResourcesQuery();
            var result = await this.mediator.Send(query);

            return Ok(result);
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetResource(Guid id)
        {
            var query = new GetResourceQuery(id.ToString());
            var result = await this.mediator.Send(query);

            if (result.HasNoValue)
            {
                return NotFound();
            }

            return Ok(result.Value);
        }

        [HttpGet("")]
        public async Task<IActionResult> FindResources([FromQuery] FindResourcesModel model)
        {
            var query = new FindResourcesQuery(model.Topic, model.Page, model.OrderBy);
            var result = await this.mediator.Send(query);

            return Ok(result);
        }
    }
}