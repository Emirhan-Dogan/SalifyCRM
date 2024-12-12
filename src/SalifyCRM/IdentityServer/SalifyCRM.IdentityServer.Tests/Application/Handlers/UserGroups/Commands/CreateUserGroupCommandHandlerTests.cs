using MediatR;
using Moq;
using SalifyCRM.IdentityServer.Application.Handlers.UserGroups.Commands;
using SalifyCRM.IdentityServer.Application.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.IdentityServer.Tests.Application.Handlers.UserGroups.Commands
{
    [TestFixture]
    [Category("Command")]
    [Category("Create")]
    [Category("UserGroup")]
    public class CreateUserGroupCommandHandlerTests
    {
        private Mock<IUserGroupRepository> _userGroupRepositoryMock;
        private Mock<IMediator> _mediatorMock;

        private CreateUserGroupCommand.CreateUserGroupCommandHandler _createUserGroupCommandHandler;

        [SetUp]
        public void SetUp()
        {
            _userGroupRepositoryMock = new Mock<IUserGroupRepository>();
            _mediatorMock = new Mock<IMediator>();

            this._createUserGroupCommandHandler = new CreateUserGroupCommand
                .CreateUserGroupCommandHandler(
                _userGroupRepositoryMock.Object,
                _mediatorMock.Object);
        }
    }
}
