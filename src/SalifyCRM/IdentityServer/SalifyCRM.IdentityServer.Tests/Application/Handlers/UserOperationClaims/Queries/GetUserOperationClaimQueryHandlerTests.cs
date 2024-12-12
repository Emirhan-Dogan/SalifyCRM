using MediatR;
using Moq;
using SalifyCRM.IdentityServer.Application.Handlers.UserOperationClaims.Queries;
using SalifyCRM.IdentityServer.Application.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.IdentityServer.Tests.Application.Handlers.UserOperationClaims.Queries
{
    [TestFixture]
    [Category("Query")]
    [Category("UserOperationClaim")]
    public class GetUserOperationClaimQueryHandlerTests
    {
        private Mock<IUserOperationClaimRepository> _userOperationClaimRepositoryMock;
        private Mock<IUserRepository> _userRepositoryMock;
        private Mock<IOperationClaimRepository> _operationClaimRepositoryMock;
        private Mock<IMediator> _mediatorMock;

        private GetUserOperationClaimQuery.GetUserOperationClaimQueryHandler _getUserOperationClaimQueryHandler;

        [SetUp]
        public void SetUp()
        {
            _userOperationClaimRepositoryMock = new Mock<IUserOperationClaimRepository>();
            _userRepositoryMock = new Mock<IUserRepository>();
            _operationClaimRepositoryMock = new Mock<IOperationClaimRepository>();
            _mediatorMock = new Mock<IMediator>();

            this._getUserOperationClaimQueryHandler = new GetUserOperationClaimQuery
                .GetUserOperationClaimQueryHandler(
                _userOperationClaimRepositoryMock.Object,
                _userRepositoryMock.Object,
                _operationClaimRepositoryMock.Object,
                _mediatorMock.Object);
        }
    }
}
