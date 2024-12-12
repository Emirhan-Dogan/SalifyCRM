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
    [Category("Delete")]
    [Category("UserGroup")]
    public class DeleteUserGroupCommandHandlerTests
    {
        private Mock<IUserGroupRepository> _userGroupRepositoryMock;
        private Mock<IMediator> _mediatorMock;

        private DeleteUserGroupCommand.DeleteUserGroupCommandHandler _deleteUserGroupCommandHandler;

        [SetUp]
        public void SetUp()
        {
            _userGroupRepositoryMock = new Mock<IUserGroupRepository>();
            _mediatorMock = new Mock<IMediator>();

            _deleteUserGroupCommandHandler = new DeleteUserGroupCommand
                .DeleteUserGroupCommandHandler(
                _userGroupRepositoryMock.Object,
                _mediatorMock.Object);
        }
    }
}
