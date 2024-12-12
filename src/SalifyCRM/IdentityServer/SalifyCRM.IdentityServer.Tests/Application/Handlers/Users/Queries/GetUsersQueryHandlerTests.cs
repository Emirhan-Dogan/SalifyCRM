using Core.Utilities.Results;
using MediatR;
using Moq;
using SalifyCRM.IdentityServer.Application.Handlers.Users.Queries;
using SalifyCRM.IdentityServer.Application.RepositoryInterfaces;
using SalifyCRM.IdentityServer.Application.Responses.Users;
using SalifyCRM.IdentityServer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.IdentityServer.Tests.Application.Handlers.Users.Queries
{
    [TestFixture]
    [Category("Query")]
    [Category("User")]
    public class GetUsersQueryHandlerTests
    {
        private Mock<IUserRepository> _userRepositoryMock;
        private Mock<IMediator> _mediatorMock;

        private GetUsersQuery.GetUsersQueryHandler _getUsersQueryHandler;

        [SetUp]
        public void SetUp()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _mediatorMock = new Mock<IMediator>();

            this._getUsersQueryHandler = new GetUsersQuery
                .GetUsersQueryHandler(
                _userRepositoryMock.Object,
                _mediatorMock.Object);
        }

        [Test]
        public async Task Handle_ShouldReturnSuccess_WhenUsersAreFound()
        {
            // Arrange
            var users = new List<User>
    {
        new User { Id = 1, FirstName = "John", LastName = "Doe", Status = true }
    };

            _userRepositoryMock.Setup(repo => repo.GetListAsync(It.IsAny<Expression<Func<User, bool>>>()))
                               .ReturnsAsync(users);

            // Act
            var result = await _getUsersQueryHandler.Handle(new GetUsersQuery(), CancellationToken.None);

            // Assert
            Assert.IsInstanceOf<SuccessDataResult<List<UserResponse>>>(result);
            Assert.AreEqual(1, result.Data.Count);
        }

        [Test]
        public async Task Handle_ShouldReturnError_WhenRepositoryThrowsException()
        {
            // Arrange
            _userRepositoryMock.Setup(repo => repo.GetListAsync(It.IsAny<Expression<Func<User, bool>>>()))
                               .ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await _getUsersQueryHandler.Handle(new GetUsersQuery(), CancellationToken.None);

            // Assert
            Assert.IsInstanceOf<ErrorDataResult<List<UserResponse>>>(result);
            Assert.AreEqual("Database error", result.Message);
        }

    }
}
