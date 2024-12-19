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
    public class GetInteractionNotesQueryHandlerTests
    {
        private Mock<IInteractionNoteRepository> _interactionNoteRepositoryMock;
        private Mock<IMediator> _mediatorMock;

        private IFixture _fixture;
        private GetInteractionNotesQuery.GetInteractionNotesQueryHandler _getInteractionNotesQueryHandler;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());

            _interactionNoteRepositoryMock = _fixture.Freeze<Mock<IInteractionNoteRepository>>();
            _mediatorMock = _fixture.Freeze<Mock<IMediator>>();

            _getInteractionNotesQueryHandler = new GetInteractionNotesQuery
                .GetInteractionNotesQueryHandler(
                _interactionNoteRepositoryMock.Object,
                _mediatorMock.Object);
        }

        [Test]
        public void GetInteractionNotesForInteractionQueryHandler_ShouldReturnSuccessAndData_WhenInteractionNoteRepositoryExist()
        {

        }
    }
}
