using AutoFixture.AutoMoq;
using AutoFixture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Moq;
using SalifyCRM.CustomerManagement.Application.Handlers.Countries.Commands;
using SalifyCRM.CustomerManagement.Application.RepositoryInterfaces;

namespace SalifyCRM.CustomerManagement.Tests.Application.Handlers.Countries.Commands
{
    [TestFixture]
    [Category("Command")]
    [Category("Update")]
    [Category("Country")]
    public class UpdateCountryCommandHandlerTests
    {
        private Mock<ICountryRepository> _countryRepositoryMock;
        private Mock<IUserRepository> _userRepositoryMock;
        private Mock<IMediator> _mediatorMock;

        private IFixture _fixture;
        private UpdateCountryCommand.UpdateCountryCommandHandler _updateCountryCommandHandler;

        [SetUp]
        public void SetUp()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());

            _countryRepositoryMock = _fixture.Freeze<Mock<ICountryRepository>>();
            _userRepositoryMock = _fixture.Freeze<Mock<IUserRepository>>();
            _mediatorMock = _fixture.Freeze<Mock<IMediator>>();

            _updateCountryCommandHandler = new UpdateCountryCommand
                .UpdateCountryCommandHandler(
                _countryRepositoryMock.Object,
                _userRepositoryMock.Object,
                _mediatorMock.Object);
        }
    }
}
