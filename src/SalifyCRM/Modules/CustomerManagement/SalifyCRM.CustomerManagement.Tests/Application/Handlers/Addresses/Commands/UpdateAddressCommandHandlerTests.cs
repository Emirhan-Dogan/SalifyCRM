using AutoFixture.AutoMoq;
using AutoFixture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalifyCRM.CustomerManagement.Application.Handlers.Addresses.Commands;
using Moq;
using SalifyCRM.CustomerManagement.Application.RepositoryInterfaces;
using MediatR;

namespace SalifyCRM.CustomerManagement.Tests.Application.Handlers.Addresses.Commands
{
    [TestFixture]
    [Category("Command")]
    [Category("Update")]
    [Category("Address")]
    public class UpdateAddressCommandHandlerTests
    {
        private Mock<IAddressRepository> _addressRepositoryMock;
        private Mock<IAddressTypeRepository> _addressTypeRepositoryMock;
        private Mock<ICountryRepository> _countryRepositoryMock;
        private Mock<ICityRepository> _cityRepositoryMock;
        private Mock<IDistrictRepository> _districtRepositoryMock;
        private Mock<IUserRepository> _userRepositoryMock;
        private Mock<IMediator> _mediatorMock;

        private IFixture _fixture;
        private UpdateAddressCommand.UpdateAddressCommandHandler _updateAddressCommandHandler;

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

            _updateAddressCommandHandler = new UpdateAddressCommand
                .UpdateAddressCommandHandler(
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
