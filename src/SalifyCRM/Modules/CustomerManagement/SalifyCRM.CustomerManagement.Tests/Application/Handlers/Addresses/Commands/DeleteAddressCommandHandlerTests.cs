using AutoFixture.AutoMoq;
using AutoFixture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Moq;
using SalifyCRM.CustomerManagement.Application.Handlers.Addresses.Commands;
using SalifyCRM.CustomerManagement.Application.RepositoryInterfaces;

namespace SalifyCRM.CustomerManagement.Tests.Application.Handlers.Addresses.Commands
{
    [TestFixture]
    [Category("Command")]
    [Category("Delete")]
    [Category("Address")]
    public class DeleteAddressCommandHandlerTests
    {
        private Mock<IAddressRepository> _addressRepositoryMock;
        private Mock<IUserRepository> _userRepositoryMock;
        private Mock<IMediator> _mediatorMock;

        private IFixture _fixture;
        private DeleteAddressCommand.DeleteAddressCommandHandler _deleteAddressCommandHandler;

        [SetUp]
        public void SetUp()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());

            _addressRepositoryMock = _fixture.Freeze<Mock<IAddressRepository>>();
            _userRepositoryMock = _fixture.Freeze<Mock<IUserRepository>>();
            _mediatorMock = _fixture.Freeze<Mock<IMediator>>();

            _deleteAddressCommandHandler = new DeleteAddressCommand
                .DeleteAddressCommandHandler(
                _addressRepositoryMock.Object,
                _userRepositoryMock.Object,
                _mediatorMock.Object);
        }
    }
}
