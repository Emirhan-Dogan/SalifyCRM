using AutoFixture;
using AutoFixture.AutoMoq;
using Core.Utilities.Results;
using FluentAssertions;
using MediatR;
using Moq;
using SalifyCRM.CustomerManagement.Application.Handlers.Occupations.Queries;
using SalifyCRM.CustomerManagement.Application.RepositoryInterfaces;
using SalifyCRM.CustomerManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.CustomerManagement.Tests.Application.Handlers.Occupations.Queries
{
    [TestFixture]
    [Category("Query")]
    [Category("Occupation")]
    public class GetOccupationsQueryHandlerTests
    {
        private Mock<IOccupationRepository> _occupationRepositoryMock;
        private Mock<IMediator> _mediatorMock;

        private GetOccupationsQuery.GetOccupationsQueryHandler _getOccupationsQueryHandler;
        private IFixture _fixture;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());

            _occupationRepositoryMock = _fixture.Freeze<Mock<IOccupationRepository>>();
            _mediatorMock = _fixture.Freeze<Mock<IMediator>>();

            _getOccupationsQueryHandler = new GetOccupationsQuery
                .GetOccupationsQueryHandler(
                _occupationRepositoryMock.Object,
                _mediatorMock.Object);
        }

        [Test]
        public void GetOccupationsQueryHandler_ShouldReturnSuccessWithData_WhenOccupationsExist()
        {
            // Arrange
            int dataCount = 15;

            _fixture.Customize<Occupation>(
                o => o.With(x => x.IsDeleted, false));

            var occupations = _fixture.CreateMany<Occupation>(dataCount).ToList();

            _occupationRepositoryMock.Setup(
                o=>o.GetListAsync(It.IsAny<Expression<Func<Occupation, bool>>>()))
                .ReturnsAsync(occupations);

            GetOccupationsQuery query = new GetOccupationsQuery();

            // Action
            var result = _getOccupationsQueryHandler.Handle(query, CancellationToken.None).Result;

            // Assertion
            result.Success.Should()
                .BeTrue();
            result.Data.Should()
                .NotBeNull();
            result.Data.Count.Should()
                .Be(occupations.Count());
        }

        [Test]
        public void GetOccupationsQueryHandler_ShouldReturnSuccessAndEmptyData_WhenEmptyOccupationsInRepository()
        {
            // Arrange
            _occupationRepositoryMock.Setup(
                o=>o.GetListAsync(It.IsAny<Expression<Func<Occupation, bool>>>()))
                .ReturnsAsync(new List<Occupation>());

            GetOccupationsQuery query = new GetOccupationsQuery();

            // Action
            var result = _getOccupationsQueryHandler.Handle(query, CancellationToken.None).Result;

            // Assertion
            result.Success.Should()
            .BeTrue();

            result.Data.Should()
            .BeNull();

            result.Data.Count.Should()
                .Be(0);
        }

        [Test]
        public void GetOccupationsQueryHandler_ShouldMapOccupationToOccupationResponse_WhenCorrectly_GivenMultipleData()
        {
            // Arrange
            int dataCount = 15;

            _fixture.Customize<Occupation>(
                o => o.With(x => x.IsDeleted, false));

            var occupations = _fixture.CreateMany<Occupation>(dataCount).ToList();

            _occupationRepositoryMock.Setup(
                o => o.GetListAsync(It.IsAny<Expression<Func<Occupation, bool>>>()))
                .ReturnsAsync(occupations);

            GetOccupationsQuery query = new GetOccupationsQuery();

            // Action
            var result = _getOccupationsQueryHandler.Handle(query, CancellationToken.None).Result;

            // Assertion
            for (int i = 0; i < occupations.Count; i++)
            {
                result.Data[i].Id.Should()
                   .Be(occupations[i].Id);

                result.Data[i].Name.Should()
                   .Be(occupations[i].Name);

                result.Data[i].Status.Should()
                   .Be(occupations[i].Status);

                result.Data[i].Descriptions.Should()
                   .Be(occupations[i].Descriptions);

            }
        }
    }
}
