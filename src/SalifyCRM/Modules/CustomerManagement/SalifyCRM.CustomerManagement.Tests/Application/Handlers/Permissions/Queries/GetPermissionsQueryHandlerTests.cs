using AutoFixture;
using AutoFixture.AutoMoq;
using FluentAssertions;
using MediatR;
using Moq;
using SalifyCRM.CustomerManagement.Application.Handlers.Permissions.Queries;
using SalifyCRM.CustomerManagement.Application.RepositoryInterfaces;
using SalifyCRM.CustomerManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.CustomerManagement.Tests.Application.Handlers.Permissions.Queries
{
    [TestFixture]
    [Category("Query")]
    [Category("Permission")]
    public class GetPermissionsQueryHandlerTests
    {
        private Mock<IPermissionRepository> _permissionRepositoryMock;
        private Mock<IMediator> _mediatorMock;

        private GetPermissionsQuery.GetPermissionsQueryHandler _getPermissionsQueryHandler;
        private IFixture _fixture;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());

            _permissionRepositoryMock = _fixture.Freeze<Mock<IPermissionRepository>>();
            _mediatorMock = _fixture.Freeze<Mock<IMediator>>();

            _getPermissionsQueryHandler = new GetPermissionsQuery
                .GetPermissionsQueryHandler(
                _permissionRepositoryMock.Object,
                _mediatorMock.Object);
        }

        [Test]
        public void GetPermissionsQueryHandler_ShouldReturnSuccessWithData_WhenPermissionsExist()
        {
            // Arrange
            int dataCount = 15;

            _fixture.Customize<Permission>(
                p => p.With(x => x.IsDeleted, false));

            var permissions = _fixture.CreateMany<Permission>(dataCount);

            _permissionRepositoryMock.Setup(
                p=>p.GetListAsync(It.IsAny<Expression<Func<Permission, bool>>>()))
                .ReturnsAsync(permissions);

            GetPermissionsQuery query = new GetPermissionsQuery();

            // Action
            var result = _getPermissionsQueryHandler.Handle(query, CancellationToken.None).Result;

            // Assertion
            result.Success.Should()
                .BeTrue();
            result.Data.Should()
                .NotBeNull();
            result.Data.Count.Should()
                .Be(dataCount);
        }

        [Test]
        public void GetPermissionsQueryHandler_ShouldReturnSuccessAndEmptyData_WhenEmptyPermissionsInRepository()
        {
            // Arrange
            _permissionRepositoryMock.Setup(
                p => p.GetListAsync(It.IsAny<Expression<Func<Permission, bool>>>()))
                .ReturnsAsync(new List<Permission>());

            GetPermissionsQuery query = new GetPermissionsQuery();

            // Action
            var result = _getPermissionsQueryHandler.Handle(query, CancellationToken.None).Result;

            // Assertion
            result.Success.Should()
                .BeTrue();

            result.Data.Should()
                .BeNull();

            result.Data.Count.Should()
                .Be(0);
        }

        [Test]
        public void GetPermissionsQueryHandler_ShouldMapPermissionToPermissionResponse_WhenCorrectly_GivenMultipleData()
        {
            // Arrange
            int dataCount = 15;

            _fixture.Customize<Permission>(
                p => p.With(x => x.IsDeleted, false));

            var permissions = _fixture.CreateMany<Permission>(dataCount).ToList();

            _permissionRepositoryMock.Setup(
                p => p.GetListAsync(It.IsAny<Expression<Func<Permission, bool>>>()))
                .ReturnsAsync(permissions);

            GetPermissionsQuery query = new GetPermissionsQuery();

            // Action
            var result = _getPermissionsQueryHandler.Handle(query, CancellationToken.None).Result;

            // Assertion
            for (int i = 0; i < permissions.Count; i++)
            {
                result.Data[i].Id.Should()
                    .Be(permissions[i].Id);

                result.Data[i].Name.Should()
                    .Be(permissions[i].Name);

                result.Data[i].Descriptions.Should()
                    .Be(permissions[i].Descriptions);

                result.Data[i].Status.Should()
                    .Be(permissions[i].Status);

                result.Data[i].DocumentPath.Should()
                    .Be(permissions[i].DocumentPath);
            }
        }
    }
}
