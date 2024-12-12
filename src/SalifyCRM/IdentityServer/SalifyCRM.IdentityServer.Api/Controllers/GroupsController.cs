using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalifyCRM.IdentityServer.Application.Handlers.Groups.Commands;
using SalifyCRM.IdentityServer.Application.Handlers.Groups.Queries;

namespace SalifyCRM.IdentityServer.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GroupsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetGroups()
        {
            var result = await _mediator.Send(new GetGroupsQuery());
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGroup([FromHeader] int id)
        {
            var result = await _mediator.Send(new GetGroupQuery() { Id = id });
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpPost]
        public async Task<IActionResult> CreateGroup([FromBody] CreateGroupCommand createGroupCommand)
        {
            var result = await _mediator.Send(createGroupCommand);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteGroup([FromBody] DeleteGroupCommand deleteGroupCommand)
        {
            var result = await _mediator.Send(deleteGroupCommand);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }
    }
}
