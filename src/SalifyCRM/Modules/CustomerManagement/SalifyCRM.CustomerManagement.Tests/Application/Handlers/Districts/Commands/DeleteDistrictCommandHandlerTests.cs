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
    [Category("Delete")]
    [Category("District")]
    public class DeleteDistrictCommandHandlerTests
    {
        private Mock<IDistrictRepository> _districtRepositoryMock;
        private Mock<IUserRepository> _userRepositoryMock;
        private Mock<IMediator> _mediatorMock;

        private IFixture _fixture;
        private DeleteDistrictCommand.DeleteDistrictCommandHandler _deleteDistrictCommandHandler;

        [SetUp]
        public void SetUp()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());

            _districtRepositoryMock = _fixture.Freeze<Mock<IDistrictRepository>>();
            _userRepositoryMock = _fixture.Freeze<Mock<IUserRepository>>();
            _mediatorMock = _fixture.Freeze<Mock<IMediator>>();

            _deleteDistrictCommandHandler = new DeleteDistrictCommand
                .DeleteDistrictCommandHandler(
                _districtRepositoryMock.Object,
                _userRepositoryMock.Object,
                _mediatorMock.Object);
        }
    }
}
