using AutoFixture.AutoMoq;
using AutoFixture;
using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalifyCRM.CustomerManagement.Application.RepositoryInterfaces;
using SalifyCRM.CustomerManagement.Application.Handlers.Occupations.Commands;

namespace SalifyCRM.CustomerManagement.Tests.Application.Handlers.Occupations.Commands
{
    [TestFixture]
    [Category("Command")]
    [Category("Update")]
    [Category("Occupation")]
    public class UpdateOccupationCommandHandlerTests
    {
        private Mock<IOccupationRepository> _occupationRepositoryMock;
        private Mock<IUserRepository> _userRepositoryMock;
        private Mock<IMediator> _mediatorMock;

        private IFixture _fixture;
        private UpdateOccupationCommand.UpdateOccupationCommandHandler _updateOccupationCommandHandler;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());

            _occupationRepositoryMock = _fixture.Freeze<Mock<IOccupationRepository>>();
            _userRepositoryMock = _fixture.Freeze<Mock<IUserRepository>>();
            _mediatorMock = _fixture.Freeze<Mock<IMediator>>();

            _updateOccupationCommandHandler = new UpdateOccupationCommand
                .UpdateOccupationCommandHandler(
                _occupationRepositoryMock.Object,
                _userRepositoryMock.Object,
                _mediatorMock.Object);
        }
    }
}
