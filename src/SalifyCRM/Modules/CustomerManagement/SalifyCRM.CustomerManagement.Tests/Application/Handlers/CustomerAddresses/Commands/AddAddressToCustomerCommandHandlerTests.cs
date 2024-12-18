using AutoFixture.AutoMoq;
using AutoFixture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Moq;
using SalifyCRM.CustomerManagement.Application.Handlers.CustomerAddresses.Commands;
using SalifyCRM.CustomerManagement.Application.RepositoryInterfaces;

namespace SalifyCRM.CustomerManagement.Tests.Application.Handlers.CustomerAddresses.Commands
{
    [TestFixture]
    [Category("Command")]
    [Category("Create")]
    [Category("Address")]
    public class AddAddressToCustomerCommandHandlerTests
    {
        private Mock<ICustomerAddressRepository> _customerAddressRepositoryMock;
        private Mock<ICustomerRepository> _customerRepositoryMock;
        private Mock<IAddressRepository> _addressRepositoryMock;
        private Mock<IUserRepository> _userRepositoryMock;
        private Mock<IMediator> _mediatorMock;

        private IFixture _fixture;
        private AddAddressToCustomerCommand.AddAddressToCustomerCommandHandler _addAddressToCustomerCommandHandler;

        [SetUp]
        public void SetUp()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());

            _customerAddressRepositoryMock = _fixture.Freeze<Mock<ICustomerAddressRepository>>();
            _customerRepositoryMock = _fixture.Freeze<Mock<ICustomerRepository>>();
            _addressRepositoryMock = _fixture.Freeze<Mock<IAddressRepository>>();
            _userRepositoryMock = _fixture.Freeze<Mock<IUserRepository>>();
            _mediatorMock = _fixture.Freeze<Mock<IMediator>>();

            _addAddressToCustomerCommandHandler = new AddAddressToCustomerCommand
                .AddAddressToCustomerCommandHandler(
                _customerAddressRepositoryMock.Object,
                _customerRepositoryMock.Object,
                _addressRepositoryMock.Object,
                _userRepositoryMock.Object,
                _mediatorMock.Object);
        }
    }
}
