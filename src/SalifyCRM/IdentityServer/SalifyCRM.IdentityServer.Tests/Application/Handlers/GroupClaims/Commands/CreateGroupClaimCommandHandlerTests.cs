using MediatR;
using Moq;
using SalifyCRM.IdentityServer.Application.Handlers.GroupClaims.Commands;
using SalifyCRM.IdentityServer.Application.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.IdentityServer.Tests.Application.Handlers.GroupClaims.Commands
{
    [TestFixture]
    [Category("Command")]
    [Category("Create")]
    [Category("GroupClaim")]
    public class CreateGroupClaimCommandHandlerTests
    {
        private Mock<IGroupClaimRepository> _groupClaimRepositoryMock;
        private Mock<IMediator> _mediatorMock;

        private CreateGroupClaimCommand.CreateGroupClaimCommandHandler _createGroupClaimCommandHandler;

        [SetUp]
        public void Setup()
        {
            _groupClaimRepositoryMock = new Mock<IGroupClaimRepository>();
            _mediatorMock = new Mock<IMediator>();

            this._createGroupClaimCommandHandler = new CreateGroupClaimCommand
                .CreateGroupClaimCommandHandler(
                _groupClaimRepositoryMock.Object,
                _mediatorMock.Object);
        }
    }
}
