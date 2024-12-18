using AutoFixture.AutoMoq;
using AutoFixture;
using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalifyCRM.CustomerManagement.Application.RepositoryInterfaces;
using SalifyCRM.CustomerManagement.Application.Handlers.Permissions.Commands;

namespace SalifyCRM.CustomerManagement.Tests.Application.Handlers.Permissions.Commands
{
    [TestFixture]
    [Category("Command")]
    [Category("Delete")]
    [Category("Permission")]
    public class DeletePermissionCommandHandlerTests
    {
        private Mock<IPermissionRepository> _permissionRepositoryMock;
        private Mock<IUserRepository> _userRepositoryMock;
        private Mock<IMediator> _mediatorMock;

        private IFixture _fixture;
        private DeletePermissionCommand.DeletePermissionCommandHandler _deletePermissionCommandHandler;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());

            _permissionRepositoryMock = _fixture.Freeze<Mock<IPermissionRepository>>();
            _userRepositoryMock = _fixture.Freeze<Mock<IUserRepository>>();
            _mediatorMock = _fixture.Freeze<Mock<IMediator>>();

            _deletePermissionCommandHandler = new DeletePermissionCommand
                .DeletePermissionCommandHandler(
                _permissionRepositoryMock.Object,
                _userRepositoryMock.Object,
                _mediatorMock.Object);
        }
    }
}
