using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalifyCRM.IdentityServer.Application.Handlers.UserOperationClaims.Commands;
using SalifyCRM.IdentityServer.Application.Handlers.UserOperationClaims.Queries;

namespace SalifyCRM.IdentityServer.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserOperationClaimsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserOperationClaimsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetUserOperationClaims()
        {
            var result = await _mediator.Send(new GetUserOperationClaimsQuery());
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserOperationClaim([FromHeader] int id)
        {
            var result = await _mediator.Send(new GetUserOperationClaimQuery() { Id = id });
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUserOperationClaim([FromBody] CreateUserOperationClaimCommand createUserOperationClaimCommand)
        {
            var result = await _mediator.Send(createUserOperationClaimCommand);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUserOperationClaim([FromBody] DeleteUserOperationClaimCommand deleteUserOperationClaimCommand)
        {
            var result = await _mediator.Send(deleteUserOperationClaimCommand);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }
    }
}
