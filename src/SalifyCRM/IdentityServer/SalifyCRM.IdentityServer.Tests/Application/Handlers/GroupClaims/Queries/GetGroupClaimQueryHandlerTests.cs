using MediatR;
using Moq;
using SalifyCRM.IdentityServer.Application.Handlers.GroupClaims.Queries;
using SalifyCRM.IdentityServer.Application.RepositoryInterfaces;
using SalifyCRM.IdentityServer.Domain.Entities;
using System.Linq.Expressions;

[TestFixture]
[Category("Query")]
[Category("GroupClaim")]
public class GetGroupClaimQueryHandlerTests
{
    private Mock<IGroupClaimRepository> _groupClaimRepositoryMock;
    private Mock<IGroupRepository> _groupRepositoryMock;
    private Mock<IOperationClaimRepository> _operationClaimRepositoryMock;
    private Mock<IMediator> _mediatorMock;

    private GetGroupClaimQuery.GetGroupClaimQueryHandler _getGroupClaimQueryHandler;

    [SetUp]
    public void Setup()
    {
        _groupClaimRepositoryMock = new Mock<IGroupClaimRepository>();
        _groupRepositoryMock = new Mock<IGroupRepository>();
        _operationClaimRepositoryMock = new Mock<IOperationClaimRepository>();
        _mediatorMock = new Mock<IMediator>();

        _getGroupClaimQueryHandler = new GetGroupClaimQuery
            .GetGroupClaimQueryHandler(
                _groupClaimRepositoryMock.Object,
                _groupRepositoryMock.Object,
                _operationClaimRepositoryMock.Object,
                _mediatorMock.Object);
    }

}
