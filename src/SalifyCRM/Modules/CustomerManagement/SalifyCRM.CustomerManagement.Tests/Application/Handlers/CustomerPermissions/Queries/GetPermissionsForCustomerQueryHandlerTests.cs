using AutoFixture.AutoMoq;
using AutoFixture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalifyCRM.CustomerManagement.Application.Handlers.CustomerPermissions.Queries;
using Moq;
using SalifyCRM.CustomerManagement.Application.RepositoryInterfaces;
using MediatR;

namespace SalifyCRM.CustomerManagement.Tests.Application.Handlers.CustomerPermissions.Queries
{
    [TestFixture]
    [Category("Query")]
    [Category("CustomerPermission")]
    public class GetPermissionsForCustomerQueryHandlerTests
    {
        private Mock<ICustomerPermissionRepository> _customerPermissionRepositoryMock;
        private Mock<ICustomerRepository> _customerRepositoryMock;
        private Mock<IMediator> _mediatorMock;

        private IFixture _fixture;
        private GetPermissionsForCustomerQuery.GetPermissionsForCustomerQueryHandler _getPermissionsForCustomerQueryHandler;

        [SetUp]
        public void SetUp()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());

            _customerPermissionRepositoryMock = _fixture.Freeze<Mock<ICustomerPermissionRepository>>();
            _customerRepositoryMock = _fixture.Freeze<Mock<ICustomerRepository>>();
            _mediatorMock = _fixture.Freeze<Mock<IMediator>>();

            _getPermissionsForCustomerQueryHandler = new GetPermissionsForCustomerQuery
                .GetPermissionsForCustomerQueryHandler(
                _customerPermissionRepositoryMock.Object,
                _customerRepositoryMock.Object,
                _mediatorMock.Object);
        }
    }
}
