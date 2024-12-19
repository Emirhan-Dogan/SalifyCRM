using AutoFixture.AutoMoq;
using AutoFixture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalifyCRM.InteractionManagement.Application.Handlers.InteractionTypes.Queries;
using MediatR;
using Moq;
using SalifyCRM.InteractionManagement.Application.RepositoryInterfaces;
using SalifyCRM.InteractionManagement.Domain.Entities;
using System.Linq.Expressions;
using FluentAssertions;
using Microsoft.EntityFrameworkCore.Query;

namespace SalifyCRM.InteractionManagement.Tests.Application.Handlers.InteractionTypes.Queries
{
    [TestFixture]
    [Category("Query")]
    [Category("InteractionType")]
    public class GetInteractionTypesQueryHandlerTests
    {
        private Mock<IInteractionTypeRepository> _interactionTypeRepositoryMock;
        private Mock<IMediator> _mediatorMock;

        private IFixture _fixture;
        private GetInteractionTypesQuery.GetInteractionTypesQueryHandler _getInteractionTypesQueryHandler;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());

            _interactionTypeRepositoryMock = _fixture.Freeze<Mock<IInteractionTypeRepository>>();
            _mediatorMock = _fixture.Freeze<Mock<IMediator>>();

            _getInteractionTypesQueryHandler = new GetInteractionTypesQuery
                .GetInteractionTypesQueryHandler(
                _interactionTypeRepositoryMock.Object,
                _mediatorMock.Object);
        }

        [Test]
        public void GetInteractionTypesQueryHandler_ShouldReturnSuccessAndData_WhenInteractionTypeRepositoryExist()
        {
            // Arrange
            int dataCount = 15;
            _fixture.Customize<InteractionType>(
                i => i.With(x => x.IsDeleted, false));

            var interactionTypes = _fixture.CreateMany<InteractionType>(dataCount);

            _interactionTypeRepositoryMock.Setup(
                i => i.GetListAsync(It.IsAny<Expression<Func<InteractionType, bool>>>(),
                It.IsAny<Func<IQueryable<InteractionType>, IIncludableQueryable<InteractionType, object>>>())
                ).ReturnsAsync(interactionTypes);

            GetInteractionTypesQuery query = new GetInteractionTypesQuery();

            // Action
            var result = _getInteractionTypesQueryHandler.Handle(query, CancellationToken.None).Result;

            // Assertion

            result.Success.Should()
                .BeTrue("Expected the operation to true, but it false");
            result.Data.Should()
                .NotBeNull("Expected non-null data to be returned, but it was null");
            result.Data.Count.Should()
                .Be(dataCount, $"expected the data count to be {dataCount}, but it was {result.Data.Count}");
        }
    }
}
