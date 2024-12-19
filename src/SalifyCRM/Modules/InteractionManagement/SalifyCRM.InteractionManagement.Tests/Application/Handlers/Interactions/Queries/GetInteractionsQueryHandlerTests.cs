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
using SalifyCRM.InteractionManagement.Domain.Entities;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;
using FluentAssertions;

namespace SalifyCRM.InteractionManagement.Tests.Application.Handlers.Interactions.Queries
{
    [TestFixture]
    [Category("Query")]
    [Category("Interaction")]
    public class GetInteractionsQueryHandlerTests
    {
        private Mock<IInteractionRepository> _interactionRepositoryMock;
        private Mock<IMediator> _mediatorMock;

        private IFixture _fixture;
        private GetInteractionsQuery.GetInteractionsQueryHandler _getInteractionsQueryHandler;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());

            _interactionRepositoryMock = _fixture.Freeze<Mock<IInteractionRepository>>();
            _mediatorMock = _fixture.Freeze<Mock<IMediator>>();

            _getInteractionsQueryHandler = new GetInteractionsQuery
                .GetInteractionsQueryHandler(
                _interactionRepositoryMock.Object,
                _mediatorMock.Object);
        }

        [Test]
        public void GetInteractionsQueryHandler_ShouldReturnSuccessAndData_WhenInteractionRepositoryExist()
        {
            // Arrange 
            int dataCount = 15;

            _fixture.Customize<Interaction>(s => s.With(x => x.IsDeleted, false));

            var interactions = _fixture.CreateMany<Interaction>(dataCount);

            _interactionRepositoryMock.Setup(i=>
            i.GetListAsync(
                It.IsAny<Expression<Func<Interaction, bool>>>(),
                It.IsAny<Func<IQueryable<Interaction>, IIncludableQueryable<Interaction, object>>>())
                ).ReturnsAsync(interactions);

            GetInteractionsQuery query = new GetInteractionsQuery();

            // Action
            var result = _getInteractionsQueryHandler.Handle(query, CancellationToken.None).Result;

            // Assertion

            result.Success.Should()
                .BeTrue();

            result.Data.Should()
                .NotBeNull();

            result.Data.Count.Should()
                .Be(dataCount);
        }
    }
}
