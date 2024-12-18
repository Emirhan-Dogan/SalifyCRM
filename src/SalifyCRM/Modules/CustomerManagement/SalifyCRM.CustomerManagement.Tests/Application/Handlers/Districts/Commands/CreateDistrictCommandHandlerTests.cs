using AutoFixture.AutoMoq;
using AutoFixture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Moq;
using SalifyCRM.CustomerManagement.Application.Handlers.Districts.Commands;
using SalifyCRM.CustomerManagement.Application.RepositoryInterfaces;

namespace SalifyCRM.CustomerManagement.Tests.Application.Handlers.Districts.Commands
{
    [TestFixture]
    [Category("Command")]
    [Category("Create")]
    [Category("District")]
    public class CreateDistrictCommandHandlerTests
    {
        private Mock<IDistrictRepository> _districtRepositoryMock;
        private Mock<ICityRepository> _cityRepositoryMock;
        private Mock<IUserRepository> _userRepositoryMock;
        private Mock<IMediator> _mediatorMock;

        private IFixture _fixture;
        private CreateDistrictCommand.CreateDistrictCommandHandler _createDistrictCommandHandler;

        [SetUp]
        public void SetUp()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());

            _districtRepositoryMock = _fixture.Freeze<Mock<IDistrictRepository>>();
            _cityRepositoryMock = _fixture.Freeze<Mock<ICityRepository>>();
            _userRepositoryMock = _fixture.Freeze<Mock<IUserRepository>>();
            _mediatorMock = _fixture.Freeze<Mock<IMediator>>();

            _createDistrictCommandHandler = new CreateDistrictCommand
                .CreateDistrictCommandHandler(
                _districtRepositoryMock.Object,
                _cityRepositoryMock.Object,
                _userRepositoryMock.Object,
                _mediatorMock.Object);
        }
    }
}
