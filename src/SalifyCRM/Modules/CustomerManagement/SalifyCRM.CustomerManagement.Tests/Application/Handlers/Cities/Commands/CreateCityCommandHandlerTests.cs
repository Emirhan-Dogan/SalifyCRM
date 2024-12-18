using AutoFixture.AutoMoq;
using AutoFixture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalifyCRM.CustomerManagement.Application.Handlers.Cities.Commands;
using Moq;
using SalifyCRM.CustomerManagement.Application.RepositoryInterfaces;
using MediatR;

namespace SalifyCRM.CustomerManagement.Tests.Application.Handlers.Cities.Commands
{
    [TestFixture]
    [Category("Command")]
    [Category("Create")]
    [Category("City")]
    public class CreateCityCommandHandlerTests
    {
        private Mock<ICityRepository> _cityRepositoryMock;
        private Mock<ICountryRepository> _countryRepositoryMock;
        private Mock<IUserRepository> _userRepositoryMock;
        private Mock<IMediator> _mediatorMock;

        private IFixture _fixture;
        private CreateCityCommand.CreateCityCommandHandler _createCityCommandHandler;

        [SetUp]
        public void SetUp()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());

            _cityRepositoryMock = _fixture.Freeze<Mock<ICityRepository>>();
            _countryRepositoryMock = _fixture.Freeze<Mock<ICountryRepository>>();
            _userRepositoryMock = _fixture.Freeze<Mock<IUserRepository>>();
            _mediatorMock = _fixture.Freeze<Mock<IMediator>>();

            _createCityCommandHandler = new CreateCityCommand
                .CreateCityCommandHandler(
                _cityRepositoryMock.Object,
                _countryRepositoryMock.Object,
                _userRepositoryMock.Object,
                _mediatorMock.Object);
        }
    }
}
