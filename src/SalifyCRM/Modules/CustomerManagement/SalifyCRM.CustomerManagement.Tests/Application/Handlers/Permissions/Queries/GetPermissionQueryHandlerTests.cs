using AutoFixture;
using AutoFixture.AutoMoq;
using MediatR;
using Moq;
using SalifyCRM.CustomerManagement.Application.Handlers.Permissions.Queries;
using SalifyCRM.CustomerManagement.Application.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.CustomerManagement.Tests.Application.Handlers.Permissions.Queries
{
    [TestFixture]
    [Category("Query")]
    [Category("Permission")]
    public class GetPermissionQueryHandlerTests
    {
        private Mock<IPermissionRepository> _permissionRepositoryMock;
        private Mock<IMediator> _mediatorMock;

        private GetPermissionQuery.GetPermissionQueryHandler _getPermissionQueryHandler;
        private IFixture _fixture;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());

            _permissionRepositoryMock = _fixture.Freeze<Mock<IPermissionRepository>>();
            _mediatorMock = _fixture.Freeze<Mock<IMediator>>();

            _getPermissionQueryHandler = new GetPermissionQuery
                .GetPermissionQueryHandler(
                _permissionRepositoryMock.Object,
                _mediatorMock.Object);
        }

        [Test]
        public void GetCustomerTagQueryHandler_ShouldReturnSuccessWithData_WhenCustomerTagExist()
        {

        }
    }
}
