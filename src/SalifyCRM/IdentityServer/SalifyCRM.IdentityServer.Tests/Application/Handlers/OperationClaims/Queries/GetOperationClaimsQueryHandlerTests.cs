using Core.Utilities.Results;
using MediatR;
using Moq;
using SalifyCRM.IdentityServer.Application.Handlers.GroupClaims.Queries;
using SalifyCRM.IdentityServer.Application.Handlers.OperationClaims.Queries;
using SalifyCRM.IdentityServer.Application.RepositoryInterfaces;
using SalifyCRM.IdentityServer.Application.Responses.OperationClaims;
using SalifyCRM.IdentityServer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.IdentityServer.Tests.Application.Handlers.OperationClaims.Queries
{
    [TestFixture]
    [Category("Query")]
    [Category("OperationClaim")]
    public class GetOperationClaimsQueryHandlerTests
    {
        private Mock<IOperationClaimRepository> _operationClaimRepositoryMock;
        private Mock<IMediator> _mediatorMock;

        private GetOperationClaimsQuery.GetOperationClaimsQueryHandler _getOperationClaimsQueryHandler;

        [SetUp]
        public void SetUp()
        {
            _operationClaimRepositoryMock = new Mock<IOperationClaimRepository>();
            _mediatorMock = new Mock<IMediator>();

            this._getOperationClaimsQueryHandler = new GetOperationClaimsQuery
                .GetOperationClaimsQueryHandler(
                _operationClaimRepositoryMock.Object,
                _mediatorMock.Object);
        }

        [Test]
        public async Task Handle_ShouldReturnSuccess_WhenOperationClaimsAreFound()
        {
            // Arrange
            var operationClaims = new List<OperationClaim>
    {
        new OperationClaim { Id = 1, Name = "Claim1", Descriptions = "Claim Description" }
    };

            _operationClaimRepositoryMock.Setup(repo => repo.GetListAsync(It.IsAny<Expression<Func<OperationClaim, bool>>>()))
                                         .ReturnsAsync(operationClaims);

            // Act
            var result = await _getOperationClaimsQueryHandler.Handle(new GetOperationClaimsQuery(), CancellationToken.None);

            // Assert
            Assert.IsInstanceOf<SuccessDataResult<List<OperationClaimResponse>>>(result);
            Assert.AreEqual(1, result.Data.Count);
        }

        [Test]
        public async Task Handle_ShouldReturnError_WhenRepositoryThrowsException()
        {
            // Arrange
            _operationClaimRepositoryMock.Setup(repo => repo.GetListAsync(It.IsAny<Expression<Func<OperationClaim, bool>>>()))
                                         .ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await _getOperationClaimsQueryHandler.Handle(new GetOperationClaimsQuery(), CancellationToken.None);

            // Assert
            Assert.IsInstanceOf<ErrorDataResult<List<OperationClaimResponse>>>(result);
            Assert.AreEqual("Database error", result.Message);
        }

    }
}
