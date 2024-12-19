using AutoFixture.AutoMoq;
using AutoFixture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalifyCRM.InteractionManagement.Application.Handlers.InteractionNotes.Queries;
using MediatR;
using Moq;
using SalifyCRM.InteractionManagement.Application.RepositoryInterfaces;

namespace SalifyCRM.InteractionManagement.Tests.Application.Handlers.InteractionNotes.Queries
{
    [TestFixture]
    [Category("Query")]
    [Category("InteractionNote")]
    public class GetInteractionNotesForInteractionQueryHandlerTests
    {
        private Mock<IInteractionNoteRepository> _interactionNoteRepositoryMock;
        private Mock<IInteractionRepository> _interactionRepositoryMock;
        private Mock<IMediator> _mediatorMock;

        private IFixture _fixture;
        private GetInteractionNotesForInteractionQuery.GetInteractionNotesForInteractionQueryHandler _getInteractionNotesForInteractionQueryHandler;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());

            _interactionNoteRepositoryMock = _fixture.Freeze<Mock<IInteractionNoteRepository>>();
            _interactionRepositoryMock = _fixture.Freeze<Mock<IInteractionRepository>>();
            _mediatorMock = _fixture.Freeze<Mock<IMediator>>();

            _getInteractionNotesForInteractionQueryHandler = new GetInteractionNotesForInteractionQuery
                .GetInteractionNotesForInteractionQueryHandler(
                _interactionNoteRepositoryMock.Object,
                _interactionRepositoryMock.Object,
                _mediatorMock.Object);
        }

        [Test]
        public void GetInteractionNotesForInteractionQueryHandler_ShouldReturnSuccessAndData_WhenInteractionNoteRepositoryExist()
        {

        }
    }
}
