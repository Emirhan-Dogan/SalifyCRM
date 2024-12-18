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
    [Category("Create")]
    [Category("Address")]
    public class CreateAddressCommandHandlerTests
    {
        private Mock<IAddressRepository> _addressRepositoryMock;
        private Mock<IAddressTypeRepository> _addressTypeRepositoryMock;
        private Mock<ICountryRepository> _countryRepositoryMock;
        private Mock<ICityRepository> _cityRepositoryMock;
        private Mock<IDistrictRepository> _districtRepositoryMock;
        private Mock<IUserRepository> _userRepositoryMock;
        private Mock<IMediator> _mediatorMock;

        private IFixture _fixture;
        private CreateAddressCommand.CreateAddressCommandHandler _createAddressCommandHandler;

        [SetUp]
        public void SetUp()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());

            _addressRepositoryMock = _fixture.Freeze<Mock<IAddressRepository>>();
            _addressTypeRepositoryMock = _fixture.Freeze<Mock<IAddressTypeRepository>>();
            _countryRepositoryMock = _fixture.Freeze<Mock<ICountryRepository>>();
            _cityRepositoryMock = _fixture.Freeze<Mock<ICityRepository>>();
            _districtRepositoryMock = _fixture.Freeze<Mock<IDistrictRepository>>();
            _userRepositoryMock = _fixture.Freeze<Mock<IUserRepository>>();
            _mediatorMock = _fixture.Freeze<Mock<IMediator>>();

            _createAddressCommandHandler = new CreateAddressCommand
                .CreateAddressCommandHandler(
                _addressRepositoryMock.Object,
                _addressTypeRepositoryMock.Object,
                _countryRepositoryMock.Object,
                _cityRepositoryMock.Object,
                _districtRepositoryMock.Object,
                _userRepositoryMock.Object,
                _mediatorMock.Object);
        }
    }
}
