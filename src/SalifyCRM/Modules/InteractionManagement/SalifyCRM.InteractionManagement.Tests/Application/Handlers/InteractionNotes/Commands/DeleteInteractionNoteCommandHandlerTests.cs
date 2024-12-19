using AutoFixture.AutoMoq;
using AutoFixture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalifyCRM.InteractionManagement.Application.Handlers.InteractionNotes.Commands;
using MediatR;
using Moq;
using SalifyCRM.InteractionManagement.Application.RepositoryInterfaces;

namespace SalifyCRM.InteractionManagement.Tests.Application.Handlers.InteractionNotes.Commands
{
    [TestFixture]
    [Category("Command")]
    [Category("Delete")]
    [Category("InteractionNote")]
    public class DeleteInteractionNoteCommandHandlerTests
    {
        private Mock<IInteractionNoteRepository> _interactionNoteRepositoryMock;
        private Mock<IUserRepository> _userRepositoryMock;
        private Mock<IMediator> _mediatorMock;

        private IFixture _fixture;
        private DeleteInteractionNoteCommand.DeleteInteractionNoteCommandHandler _deleteInteractionNoteCommandHandler;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());

            _interactionNoteRepositoryMock = _fixture.Freeze<Mock<IInteractionNoteRepository>>();
            _userRepositoryMock = _fixture.Freeze<Mock<IUserRepository>>();
            _mediatorMock = _fixture.Freeze<Mock<IMediator>>();

            _deleteInteractionNoteCommandHandler = new DeleteInteractionNoteCommand
                    .DeleteInteractionNoteCommandHandler(
                    _interactionNoteRepositoryMock.Object,
                    _userRepositoryMock.Object,
                    _mediatorMock.Object);
        }
    }
}
