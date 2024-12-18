using AutoFixture.AutoMoq;
using AutoFixture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Moq;
using SalifyCRM.CustomerManagement.Application.Handlers.CustomerPermissions.Commands;
using SalifyCRM.CustomerManagement.Application.RepositoryInterfaces;

namespace SalifyCRM.CustomerManagement.Tests.Application.Handlers.CustomerPermissions.Commands
{
    [TestFixture]
    [Category("Command")]
    [Category("Delete")]
    [Category("CustomerPermission")]
    public class DeletePermissionToCustomerCommandHandlerTests
    {
        private Mock<ICustomerPermissionRepository> _customerPermissionRepositoryMock;
        private Mock<ICustomerRepository> _customerRepositoryMock;
        private Mock<IPermissionRepository> _permissionRepositoryMock;
        private Mock<IUserRepository> _userRepositoryMock;
        private Mock<IMediator> _mediatorMock;

        private IFixture _fixture;
        private DeletePermissionToCustomerCommand.DeletePermissionToCustomerCommandHandler _deletePermissionToCustomerCommandHandler;

        [SetUp]
        public void SetUp()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());

            _customerPermissionRepositoryMock = _fixture.Freeze<Mock<ICustomerPermissionRepository>>();
            _customerRepositoryMock = _fixture.Freeze<Mock<ICustomerRepository>>();
            _permissionRepositoryMock = _fixture.Freeze<Mock<IPermissionRepository>>();
            _userRepositoryMock = _fixture.Freeze<Mock<IUserRepository>>();
            _mediatorMock = _fixture.Freeze<Mock<IMediator>>();

            _deletePermissionToCustomerCommandHandler = new DeletePermissionToCustomerCommand
                .DeletePermissionToCustomerCommandHandler(
                _customerPermissionRepositoryMock.Object,
                _customerRepositoryMock.Object,
                _permissionRepositoryMock.Object,
                _userRepositoryMock.Object,
                _mediatorMock.Object);
        }
    }
}
