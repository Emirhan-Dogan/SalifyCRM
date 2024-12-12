using MediatR;
using Moq;
using SalifyCRM.IdentityServer.Application.Handlers.Groups.Commands;
using SalifyCRM.IdentityServer.Application.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.IdentityServer.Tests.Application.Handlers.Groups.Commands
{
    [TestFixture]
    [Category("Command")]
    [Category("Create")]
    [Category("Group")]
    public class CreateGroupCommandHandlerTests
    {
        private Mock<IGroupClaimRepository> _groupClaimRepositoryMock;
        private Mock<IUserGroupRepository> _userGroupRepositoryMock;
        private Mock<IGroupRepository> _groupRepositoryMock;
        private Mock<IMediator> _mediatorMock;

        private CreateGroupCommand.CreateGroupCommandHandler _createGroupCommandHandler;

        [SetUp]
        public void SetUp()
        {
            _groupClaimRepositoryMock = new Mock<IGroupClaimRepository>();
            _userGroupRepositoryMock = new Mock<IUserGroupRepository>();
            _groupRepositoryMock = new Mock<IGroupRepository>();
            _mediatorMock = new Mock<IMediator>();

            this._createGroupCommandHandler = new CreateGroupCommand
                .CreateGroupCommandHandler(
                _groupClaimRepositoryMock.Object,
                _userGroupRepositoryMock.Object,
                _groupRepositoryMock.Object,
                _mediatorMock.Object);
        }
    }
}
