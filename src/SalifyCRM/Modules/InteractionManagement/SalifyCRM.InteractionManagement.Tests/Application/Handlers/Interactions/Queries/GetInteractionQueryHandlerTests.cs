using AutoFixture.AutoMoq;
using AutoFixture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalifyCRM.InteractionManagement.Application.Handlers.Interactions.Queries;
using MediatR;
using Moq;
using SalifyCRM.InteractionManagement.Application.RepositoryInterfaces;

namespace SalifyCRM.InteractionManagement.Tests.Application.Handlers.Interactions.Queries
{
    [TestFixture]
    [Category("Query")]
    [Category("Interaction")]
    public class GetInteractionQueryHandlerTests
    {
        private Mock<IInteractionRepository> _interactionRepositoryMock;
        private Mock<IInteractionNoteRepository> _interactionNoteRepositoryMock;
        private Mock<IMediator> _mediatorMock;

        private IFixture _fixture;
        private GetInteractionQuery.GetInteractionQueryHandler _getInteractionQueryHandler;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());

            _interactionNoteRepositoryMock = _fixture.Freeze<Mock<IInteractionNoteRepository>>();
            _interactionRepositoryMock = _fixture.Freeze<Mock<IInteractionRepository>>();
            _mediatorMock = _fixture.Freeze<Mock<IMediator>>();

            _getInteractionQueryHandler = new GetInteractionQuery
                .GetInteractionQueryHandler(
                _interactionRepositoryMock.Object,
                _interactionNoteRepositoryMock.Object,
                _mediatorMock.Object);
        }

        [Test]
        public void GetInteractionQueryHandler_ShouldReturnSuccessAndData_WhenInteractionRepositoryExist()
        {

        }
    }
}
