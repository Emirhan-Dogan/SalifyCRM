using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalifyCRM.IdentityServer.Application.Handlers.UserGroups.Commands;
using SalifyCRM.IdentityServer.Application.Handlers.UserGroups.Queries;

namespace SalifyCRM.IdentityServer.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserGroupsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserGroupsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetUserGroups()
        {
            var result = await _mediator.Send(new GetUserGroupsQuery());
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserGroup([FromHeader] int id)
        {
            var result = await _mediator.Send(new GetUserGroupQuery() { Id = id });
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUserGroup([FromBody] CreateUserGroupCommand createUserGroupCommand)
        {
            var result = await _mediator.Send(createUserGroupCommand);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUserGroup([FromBody] DeleteUserGroupCommand deleteUserGroupCommand)
        {
            var result = await _mediator.Send(deleteUserGroupCommand);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }
    }
}
