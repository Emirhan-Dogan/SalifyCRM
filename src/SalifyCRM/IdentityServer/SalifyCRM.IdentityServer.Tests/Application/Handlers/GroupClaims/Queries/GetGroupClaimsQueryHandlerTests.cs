using Core.Utilities.Results;
using MediatR;
using Moq;
using SalifyCRM.IdentityServer.Application.Handlers.GroupClaims.Queries;
using SalifyCRM.IdentityServer.Application.RepositoryInterfaces;
using SalifyCRM.IdentityServer.Application.Responses.GroupClaims;
using SalifyCRM.IdentityServer.Domain.Entities;
using System.Linq.Expressions;

[TestFixture]
[Category("Query")]
[Category("GroupClaims")]
public class GetGroupClaimsQueryHandlerTests
{
    private Mock<IGroupClaimRepository> _groupClaimRepositoryMock;
    private Mock<IMediator> _mediatorMock;

    private GetGroupClaimsQuery.GetGroupClaimsQueryHandler _getGroupClaimsQueryHandler;

    [SetUp]
    public void Setup()
    {
        _groupClaimRepositoryMock = new Mock<IGroupClaimRepository>();
        _mediatorMock = new Mock<IMediator>();

        _getGroupClaimsQueryHandler = new GetGroupClaimsQuery
            .GetGroupClaimsQueryHandler(
                _groupClaimRepositoryMock.Object,
                _mediatorMock.Object);
    }

    [Test]
    public async Task Handle_ShouldReturnSuccess_WhenGroupClaimsAreFound()
    {
        // Arrange
        var groupClaims = new List<GroupClaim>
    {
        new GroupClaim { GroupId = 1, Id = 1, OperationClaimId = 1, Status = true }
    };

        _groupClaimRepositoryMock.Setup(repo => repo.GetListAsync(It.IsAny<Expression<Func<GroupClaim, bool>>>()))
                                  .ReturnsAsync(groupClaims);

        // Act
        var result = await _getGroupClaimsQueryHandler.Handle(new GetGroupClaimsQuery(), CancellationToken.None);

        // Assert
        Assert.IsInstanceOf<SuccessDataResult<List<GroupClaimResponse>>>(result);
        Assert.AreEqual(1, result.Data.Count);
    }

    [Test]
    public async Task Handle_ShouldReturnError_WhenRepositoryThrowsException()
    {
        // Arrange
        _groupClaimRepositoryMock.Setup(repo => repo.GetListAsync(It.IsAny<Expression<Func<GroupClaim, bool>>>()))
                                  .ThrowsAsync(new Exception("Database error"));

        // Act
        var result = await _getGroupClaimsQueryHandler.Handle(new GetGroupClaimsQuery(), CancellationToken.None);

        // Assert
        Assert.IsInstanceOf<ErrorDataResult<List<GroupClaimResponse>>>(result);
        Assert.AreEqual("Database error", result.Message);
    }

}
