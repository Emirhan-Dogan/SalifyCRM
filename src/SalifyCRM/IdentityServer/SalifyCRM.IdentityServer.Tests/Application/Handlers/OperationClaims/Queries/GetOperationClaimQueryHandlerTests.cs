using MediatR;
using Moq;
using SalifyCRM.IdentityServer.Application.Handlers.OperationClaims.Queries;
using SalifyCRM.IdentityServer.Application.RepositoryInterfaces;
using SalifyCRM.IdentityServer.Domain.Entities;
using System.Linq.Expressions;

[TestFixture]
[Category("Query")]
[Category("OperationClaim")]
public class GetOperationClaimQueryHandlerTests
{
    private Mock<IOperationClaimRepository> _operationClaimRepositoryMock;
    private Mock<IUserOperationClaimRepository> _userOperationClaimRepositoryMock;
    private Mock<IGroupClaimRepository> _groupClaimRepositoryMock;
    private Mock<IMediator> _mediatorMock;

    private GetOperationClaimQuery.GetOperationClaimQueryHandler _getOperationClaimQueryHandler;

    [SetUp]
    public void SetUp()
    {
        _operationClaimRepositoryMock = new Mock<IOperationClaimRepository>();
        _userOperationClaimRepositoryMock = new Mock<IUserOperationClaimRepository>();
        _groupClaimRepositoryMock = new Mock<IGroupClaimRepository>();
        _mediatorMock = new Mock<IMediator>();

        _getOperationClaimQueryHandler = new GetOperationClaimQuery
            .GetOperationClaimQueryHandler(
            _operationClaimRepositoryMock.Object,
            _userOperationClaimRepositoryMock.Object,
            _groupClaimRepositoryMock.Object,
            _mediatorMock.Object);
    }

    
}
