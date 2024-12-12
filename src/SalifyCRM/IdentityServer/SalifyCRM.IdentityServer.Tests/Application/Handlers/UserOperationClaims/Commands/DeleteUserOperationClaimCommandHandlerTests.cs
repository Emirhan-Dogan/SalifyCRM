using MediatR;
using Moq;
using SalifyCRM.IdentityServer.Application.Handlers.UserOperationClaims.Commands;
using SalifyCRM.IdentityServer.Application.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.IdentityServer.Tests.Application.Handlers.UserOperationClaims.Commands
{
    [TestFixture]
    [Category("Command")]
    [Category("Delete")]
    [Category("UserOperationClaim")]
    public class DeleteUserOperationClaimCommandHandlerTests
    {
        private Mock<IUserOperationClaimRepository> _userOperationClaimRepositoryMock;
        private Mock<IMediator> _mediatorMock;

        DeleteUserOperationClaimCommand.DeleteUserOperationClaimCommandHandler _deleteUserOperationClaimCommandHandler;

        [SetUp]
        public void SetUp()
        {
            _userOperationClaimRepositoryMock = new Mock<IUserOperationClaimRepository>();
            _mediatorMock = new Mock<IMediator>();

            this._deleteUserOperationClaimCommandHandler = new DeleteUserOperationClaimCommand
                .DeleteUserOperationClaimCommandHandler(
                _userOperationClaimRepositoryMock.Object,
                _mediatorMock.Object);
        }
    }
}
