using Core.Utilities.Results;
using MediatR;
using Moq;
using SalifyCRM.IdentityServer.Application.Handlers.UserGroups.Queries;
using SalifyCRM.IdentityServer.Application.RepositoryInterfaces;
using SalifyCRM.IdentityServer.Application.Responses.UserGroups;
using SalifyCRM.IdentityServer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.IdentityServer.Tests.Application.Handlers.UserGroups.Queries
{
    [TestFixture]
    [Category("Query")]
    [Category("UserGroup")]
    public class GetUserGroupsQueryHandlerTests
    {
        private Mock<IUserGroupRepository> _userGroupRepositoryMock;
        private Mock<IMediator> _mediatorMock;

        private GetUserGroupsQuery.GetUserGroupsQueryHandler _getUserGroupsQueryHandler;

        [SetUp]
        public void SetUp()
        {
            _userGroupRepositoryMock = new Mock<IUserGroupRepository>();
            _mediatorMock = new Mock<IMediator>();

            this._getUserGroupsQueryHandler = new GetUserGroupsQuery
                .GetUserGroupsQueryHandler(
                _userGroupRepositoryMock.Object,
                _mediatorMock.Object);
        }
        [Test]
        public async Task Handle_ShouldReturnSuccess_WhenUserGroupsAreFound()
        {
            // Arrange
            var userGroups = new List<UserGroup>
    {
        new UserGroup { GroupId = 1, Id = 1, Status = true, UserId = 1 }
    };

            _userGroupRepositoryMock.Setup(repo => repo.GetListAsync(It.IsAny<Expression<Func<UserGroup, bool>>>()))
                                    .ReturnsAsync(userGroups);

            // Act
            var result = await _getUserGroupsQueryHandler.Handle(new GetUserGroupsQuery(), CancellationToken.None);

            // Assert
            Assert.IsInstanceOf<SuccessDataResult<List<UserGroupResponse>>>(result);
            Assert.AreEqual(1, result.Data.Count);
        }

        [Test]
        public async Task Handle_ShouldReturnError_WhenRepositoryThrowsException()
        {
            // Arrange
            _userGroupRepositoryMock.Setup(repo => repo.GetListAsync(It.IsAny<Expression<Func<UserGroup, bool>>>()))
                                    .ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await _getUserGroupsQueryHandler.Handle(new GetUserGroupsQuery(), CancellationToken.None);

            // Assert
            Assert.IsInstanceOf<ErrorDataResult<List<UserGroupResponse>>>(result);
            Assert.AreEqual("Database error", result.Message);
        }

    }
}
