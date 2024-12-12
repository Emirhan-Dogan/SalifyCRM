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
    [Category("Delete")]
    [Category("GroupClaim")]
    public class DeleteGroupClaimCommandHandlerTests
    {
        private Mock<IGroupClaimRepository> _groupClaimRepositoryMock;
        private Mock<IMediator> _mediatorMock;

        private DeleteGroupClaimCommand.DeleteGroupClaimCommandHandler _deleteGroupClaimCommandHandler;

        [SetUp]
        public void Setup()
        {
            _groupClaimRepositoryMock = new Mock<IGroupClaimRepository>();
            _mediatorMock = new Mock<IMediator>();

            this._deleteGroupClaimCommandHandler = new DeleteGroupClaimCommand
                .DeleteGroupClaimCommandHandler(
                _groupClaimRepositoryMock.Object,
                _mediatorMock.Object);
        }
    }
}
