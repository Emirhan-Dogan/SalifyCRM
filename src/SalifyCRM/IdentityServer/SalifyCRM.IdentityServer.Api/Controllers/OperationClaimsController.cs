using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalifyCRM.IdentityServer.Application.Handlers.OperationClaims.Queries;

namespace SalifyCRM.IdentityServer.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationClaimsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OperationClaimsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetOperationClaims()
        {
            var result = await _mediator.Send(new GetOperationClaimsQuery());
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOperationClaims([FromHeader] int id)
        {
            var result = await _mediator.Send(new GetOperationClaimQuery() { Id = id });
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }
    }
}
