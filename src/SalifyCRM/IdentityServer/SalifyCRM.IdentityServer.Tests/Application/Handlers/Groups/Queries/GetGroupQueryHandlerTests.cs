using MediatR;
using Moq;
using SalifyCRM.IdentityServer.Application.Handlers.Groups.Queries;
using SalifyCRM.IdentityServer.Application.RepositoryInterfaces;
using SalifyCRM.IdentityServer.Application.Responses.Groups;
using Core.Utilities.Results;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalifyCRM.IdentityServer.Domain.Entities;
using System.Linq.Expressions;
using SalifyCRM.IdentityServer.Application.Responses.OperationClaims;
using SalifyCRM.IdentityServer.Application.Responses.Users;

namespace SalifyCRM.IdentityServer.Tests.Application.Handlers.Groups.Queries
{
    [TestFixture]
    [Category("Query")]
    [Category("Group")]
    public class GetGroupQueryHandlerTests
    {
        private Mock<IGroupRepository> _groupRepositoryMock;
        private Mock<IUserGroupRepository> _userGroupRepositoryMock;
        private Mock<IGroupClaimRepository> _groupClaimRepositoryMock;
        private Mock<IMediator> _mediatorMock;

        private GetGroupQuery.GetGroupQueryHandler _getGroupQueryHandler;

        [SetUp]
        public void SetUp()
        {
            _groupRepositoryMock = new Mock<IGroupRepository>();
            _userGroupRepositoryMock = new Mock<IUserGroupRepository>();
            _groupClaimRepositoryMock = new Mock<IGroupClaimRepository>();
            _mediatorMock = new Mock<IMediator>();

            this._getGroupQueryHandler = new GetGroupQuery
                .GetGroupQueryHandler(
                _groupRepositoryMock.Object,
                _userGroupRepositoryMock.Object,
                _groupClaimRepositoryMock.Object,
                _mediatorMock.Object);
        }

        
    }
}
