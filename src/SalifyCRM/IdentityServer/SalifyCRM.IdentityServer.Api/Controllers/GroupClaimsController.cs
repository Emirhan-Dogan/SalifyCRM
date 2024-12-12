using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalifyCRM.IdentityServer.Application.Handlers.GroupClaims.Commands;
using SalifyCRM.IdentityServer.Application.Handlers.GroupClaims.Queries;

namespace SalifyCRM.IdentityServer.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupClaimsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GroupClaimsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetGroupClaims()
        {
            var result = await _mediator.Send(new GetGroupClaimsQuery());
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGroupClaim([FromHeader] int id)
        {
            var result = await _mediator.Send(new GetGroupClaimQuery() { Id = id });
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpPost]
        public async Task<IActionResult> CreateGroupClaim([FromBody] CreateGroupClaimCommand createGroupClaimCommand)
        {
            var result = await _mediator.Send(createGroupClaimCommand);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteGroupClaim([FromBody] DeleteGroupClaimCommand deleteGroupClaimCommand)
        {
            var result = await _mediator.Send(deleteGroupClaimCommand);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }
    }
}
