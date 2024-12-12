using Core.Utilities.Results;
using MediatR;
using Moq;
using SalifyCRM.IdentityServer.Application.Handlers.UserOperationClaims.Queries;
using SalifyCRM.IdentityServer.Application.RepositoryInterfaces;
using SalifyCRM.IdentityServer.Application.Responses.UserOperationClaims;
using SalifyCRM.IdentityServer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.IdentityServer.Tests.Application.Handlers.UserOperationClaims.Queries
{
    [TestFixture]
    [Category("Query")]
    [Category("UserOperationClaim")]
    public class GetUserOperationClaimsQueryHandlerTests
    {
        private Mock<IUserOperationClaimRepository> _userOperationClaimRepositoryMock;
        private Mock<IMediator> _mediatorMock;

        private GetUserOperationClaimsQuery.GetUserOperationClaimsQueryHandler _getUserOperationClaimsQueryHandler;

        [SetUp]
        public void SetUp()
        {
            _userOperationClaimRepositoryMock = new Mock<IUserOperationClaimRepository>();
            _mediatorMock = new Mock<IMediator>();

            this._getUserOperationClaimsQueryHandler = new GetUserOperationClaimsQuery
                .GetUserOperationClaimsQueryHandler(
                _userOperationClaimRepositoryMock.Object,
                _mediatorMock.Object);
        }

        [Test]
        public async Task Handle_ShouldReturnSuccess_WhenUserOperationClaimsAreFound()
        {
            // Arrange
            var userOperationClaims = new List<UserOperationClaim>
    {
        new UserOperationClaim { Id = 1, OperationClaimId = 1, Status = true, UserId = 1 }
    };

            _userOperationClaimRepositoryMock.Setup(repo => repo.GetListAsync(It.IsAny<Expression<Func<UserOperationClaim, bool>>>()))
                                             .ReturnsAsync(userOperationClaims);

            // Act
            var result = await _getUserOperationClaimsQueryHandler.Handle(new GetUserOperationClaimsQuery(), CancellationToken.None);

            // Assert
            Assert.IsInstanceOf<SuccessDataResult<List<UserOperationClaimResponse>>>(result);
            Assert.AreEqual(1, result.Data.Count);
        }

        [Test]
        public async Task Handle_ShouldReturnError_WhenRepositoryThrowsException()
        {
            // Arrange
            _userOperationClaimRepositoryMock.Setup(repo => repo.GetListAsync(It.IsAny<Expression<Func<UserOperationClaim, bool>>>()))
                                             .ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await _getUserOperationClaimsQueryHandler.Handle(new GetUserOperationClaimsQuery(), CancellationToken.None);

            // Assert
            Assert.IsInstanceOf<ErrorDataResult<List<UserOperationClaimResponse>>>(result);
            Assert.AreEqual("Database error", result.Message);
        }

    }
}
