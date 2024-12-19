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
    [Category("Create")]
    [Category("InteractionNote")]
    public class CreateInteractionNoteCommandHandlerTests
    {
        private Mock<IInteractionNoteRepository> _interactionNoteRepositoryMock;
        private Mock<IInteractionRepository> _interactionRepositoryMock;
        private Mock<IUserRepository> _userRepositoryMock;
        private Mock<IMediator> _mediatorMock;

        private IFixture _fixture;
        private CreateInteractionNoteCommand.CreateInteractionNoteCommandHandler _createInteractionNoteCommandHandler;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());

            _interactionNoteRepositoryMock = _fixture.Freeze<Mock<IInteractionNoteRepository>>();
            _interactionRepositoryMock = _fixture.Freeze<Mock<IInteractionRepository>>();
            _userRepositoryMock = _fixture.Freeze<Mock<IUserRepository>>();
            _mediatorMock = _fixture.Freeze<Mock<IMediator>>();

            _createInteractionNoteCommandHandler = new CreateInteractionNoteCommand
                    .CreateInteractionNoteCommandHandler(
                    _interactionNoteRepositoryMock.Object,
                    _interactionRepositoryMock.Object,
                    _userRepositoryMock.Object,
                    _mediatorMock.Object);
        }
    }
}
