using AutoFixture.AutoMoq;
using AutoFixture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalifyCRM.InteractionManagement.Application.Handlers.Interactions.Commands;
using MediatR;
using Moq;
using SalifyCRM.InteractionManagement.Application.RepositoryInterfaces;

namespace SalifyCRM.InteractionManagement.Tests.Application.Handlers.Interactions.Commands
{
    [TestFixture]
    [Category("Command")]
    [Category("Delete")]
    [Category("Interaction")]
    public class DeleteInteractionCommandHandlerTests
    {
        private Mock<IInteractionRepository> _interactionRepositoryMock;
        private Mock<IUserRepository> _userRepositoryMock;
        private Mock<IMediator> _mediatorMock;

        private IFixture _fixture;
        private DeleteInteractionCommand.DeleteInteractionCommandHandler _deleteInteractionCommandHandler;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());

            _interactionRepositoryMock = _fixture.Freeze<Mock<IInteractionRepository>>();
            _userRepositoryMock = _fixture.Freeze<Mock<IUserRepository>>();
            _mediatorMock = _fixture.Freeze<Mock<IMediator>>();

            _deleteInteractionCommandHandler = new DeleteInteractionCommand
                .DeleteInteractionCommandHandler(
                _interactionRepositoryMock.Object,
                _userRepositoryMock.Object,
                _mediatorMock.Object);
        }
    }
}
