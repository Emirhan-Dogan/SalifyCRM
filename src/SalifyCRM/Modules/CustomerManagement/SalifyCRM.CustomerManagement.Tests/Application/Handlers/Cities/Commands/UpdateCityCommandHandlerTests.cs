using AutoFixture.AutoMoq;
using AutoFixture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Moq;
using SalifyCRM.CustomerManagement.Application.Handlers.Cities.Commands;
using SalifyCRM.CustomerManagement.Application.RepositoryInterfaces;

namespace SalifyCRM.CustomerManagement.Tests.Application.Handlers.Cities.Commands
{
    [TestFixture]
    [Category("Command")]
    [Category("Update")]
    [Category("City")]
    public class UpdateCityCommandHandlerTests
    {
        private Mock<ICityRepository> _cityRepositoryMock;
        private Mock<ICountryRepository> _countryRepositoryMock;
        private Mock<IUserRepository> _userRepositoryMock;
        private Mock<IMediator> _mediatorMock;

        private IFixture _fixture;
        private UpdateCityCommand.UpdateCityCommandHandler _updateCityCommandHandler;

        [SetUp]
        public void SetUp()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());

            _cityRepositoryMock = _fixture.Freeze<Mock<ICityRepository>>();
            _countryRepositoryMock = _fixture.Freeze<Mock<ICountryRepository>>();
            _userRepositoryMock = _fixture.Freeze<Mock<IUserRepository>>();
            _mediatorMock = _fixture.Freeze<Mock<IMediator>>();

            _updateCityCommandHandler = new UpdateCityCommand
                .UpdateCityCommandHandler(
                _cityRepositoryMock.Object,
                _countryRepositoryMock.Object,
                _userRepositoryMock.Object,
                _mediatorMock.Object);
        }
    }
}
