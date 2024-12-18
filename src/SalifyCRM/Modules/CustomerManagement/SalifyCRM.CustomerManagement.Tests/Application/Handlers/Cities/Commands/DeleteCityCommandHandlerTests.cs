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
    [Category("Delete")]
    [Category("City")]
    public class DeleteCityCommandHandlerTests
    {
        private Mock<ICityRepository> _cityRepositoryMock;
        private Mock<IDistrictRepository> _districtRepositoryMock;
        private Mock<IUserRepository> _userRepositoryMock;
        private Mock<IMediator> _mediatorMock;

        private IFixture _fixture;
        private DeleteCityCommand.DeleteCityCommandHandler _deleteCityCommandHandler;

        [SetUp]
        public void SetUp()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());

            _cityRepositoryMock = _fixture.Freeze<Mock<ICityRepository>>();
            _districtRepositoryMock = _fixture.Freeze<Mock<IDistrictRepository>>();
            _userRepositoryMock = _fixture.Freeze<Mock<IUserRepository>>();
            _mediatorMock = _fixture.Freeze<Mock<IMediator>>();

            _deleteCityCommandHandler = new DeleteCityCommand
                .DeleteCityCommandHandler(
                _cityRepositoryMock.Object,
                _districtRepositoryMock.Object,
                _userRepositoryMock.Object,
                _mediatorMock.Object);
        }
    }
}
