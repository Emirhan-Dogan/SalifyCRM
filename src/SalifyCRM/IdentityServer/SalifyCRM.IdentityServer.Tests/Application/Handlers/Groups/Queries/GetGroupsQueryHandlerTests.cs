using Core.Utilities.Results;
using MediatR;
using Moq;
using NUnit.Framework;
using SalifyCRM.IdentityServer.Application.Handlers.Groups.Queries;
using SalifyCRM.IdentityServer.Application.RepositoryInterfaces;
using SalifyCRM.IdentityServer.Application.Responses.Groups;
using SalifyCRM.IdentityServer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace SalifyCRM.IdentityServer.Tests.Application.Handlers.Groups.Queries
{
    [TestFixture]
    [Category("Query")]
    [Category("Group")]
    public class GetGroupsQueryHandlerTests
    {
        private Mock<IGroupRepository> _groupRepositoryMock;
        private Mock<IMediator> _mediatorMock;
        private GetGroupsQuery.GetGroupsQueryHandler _getGroupsQueryHandler;

        [SetUp]
        public void SetUp()
        {
            _groupRepositoryMock = new Mock<IGroupRepository>();
            _mediatorMock = new Mock<IMediator>();

            _getGroupsQueryHandler = new GetGroupsQuery
                .GetGroupsQueryHandler(
                _groupRepositoryMock.Object,
                _mediatorMock.Object);
        }

        [Test]
        public async Task Handle_ShouldReturnSuccess_WhenGroupsAreFound()
        {
            // Arrange
            var groups = new List<Group>
    {
        new Group { Id = 1, Name = "Group1", Status = true, Descriptions = "Group 1" }
    };

            _groupRepositoryMock.Setup(repo => repo.GetListAsync(It.IsAny<Expression<Func<Group, bool>>>()))
                                .ReturnsAsync(groups);

            // Act
            var result = await _getGroupsQueryHandler.Handle(new GetGroupsQuery(), CancellationToken.None);

            // Assert
            Assert.IsInstanceOf<SuccessDataResult<List<GroupResponse>>>(result);
            Assert.AreEqual(1, result.Data.Count);
        }

        [Test]
        public async Task Handle_ShouldReturnError_WhenRepositoryThrowsException()
        {
            // Arrange
            _groupRepositoryMock.Setup(repo => repo.GetListAsync(It.IsAny<Expression<Func<Group, bool>>>()))
                                .ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await _getGroupsQueryHandler.Handle(new GetGroupsQuery(), CancellationToken.None);

            // Assert
            Assert.IsInstanceOf<ErrorDataResult<List<GroupResponse>>>(result);
            Assert.AreEqual("Database error", result.Message);
        }

    }
}
