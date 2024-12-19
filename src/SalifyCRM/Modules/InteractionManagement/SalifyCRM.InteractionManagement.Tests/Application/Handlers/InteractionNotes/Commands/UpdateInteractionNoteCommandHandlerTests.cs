using AutoFixture;
using AutoFixture.AutoMoq;
using MediatR;
using Moq;
using SalifyCRM.InteractionManagement.Application.Handlers.InteractionNotes.Commands;
using SalifyCRM.InteractionManagement.Application.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.InteractionManagement.Tests.Application.Handlers.InteractionNotes.Commands
{
    [TestFixture]
    [Category("Command")]
    [Category("Update")]
    [Category("InteractionNote")]
    public class UpdateInteractionNoteCommandHandlerTests
    {
        private Mock<IInteractionNoteRepository> _interactionNoteRepositoryMock;
        private Mock<IUserRepository> _userRepositoryMock;
        private Mock<IMediator> _mediatorMock;

        private IFixture _fixture;
        private UpdateInteractionNoteCommand.UpdateInteractionNoteCommandHandler _updateInteractionNoteCommandHandler;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());

            _interactionNoteRepositoryMock = _fixture.Freeze<Mock<IInteractionNoteRepository>>();
            _userRepositoryMock = _fixture.Freeze<Mock<IUserRepository>>();
            _mediatorMock = _fixture.Freeze<Mock<IMediator>>();

            _updateInteractionNoteCommandHandler = new UpdateInteractionNoteCommand
                .UpdateInteractionNoteCommandHandler(
                _interactionNoteRepositoryMock.Object,
                _userRepositoryMock.Object,
                _mediatorMock.Object);
        }
    }
}
