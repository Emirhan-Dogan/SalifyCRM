using AutoFixture;
using AutoFixture.AutoMoq;
using Core.Utilities.Results;
using FluentAssertions;
using MediatR;
using Moq;
using SalifyCRM.CustomerManagement.Application.Handlers.MartialStatuses.Queries;
using SalifyCRM.CustomerManagement.Application.RepositoryInterfaces;
using SalifyCRM.CustomerManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.CustomerManagement.Tests.Application.Handlers.MartialStatuses.Queries
{
    [TestFixture]
    [Category("Query")]
    [Category("MartialStatus")]
    public class GetMartialStatusesQueryHandlerTests
    {
        private Mock<IMartialStatusRepository> _martialStatusRepositoryMock;
        private Mock<IMediator> _mediatorMock;

        private GetMartialStatusesQuery.GetMartialStatusesQueryHandler _getMartialStatusesQueryHandler;
        private IFixture _fixture;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());

            _martialStatusRepositoryMock = _fixture.Freeze<Mock<IMartialStatusRepository>>();
            _mediatorMock = _fixture.Freeze<Mock<IMediator>>();

            _getMartialStatusesQueryHandler = new GetMartialStatusesQuery
                .GetMartialStatusesQueryHandler(
                _martialStatusRepositoryMock.Object,
                _mediatorMock.Object);
        }

        [Test]
        public void GetMartialStatusesQueryHandler_ShouldReturnSuccessWithData_WhenMartialStatusesExist()
        {
            // Arrange
            int dataCount = 15;

            _fixture.Customize<MartialStatus>(
                m => m.With(x => x.IsDeleted, false));

            var martialStatuses = _fixture.CreateMany<MartialStatus>(dataCount).ToList();

            _martialStatusRepositoryMock.Setup(
                m => m.GetListAsync(It.IsAny<Expression<Func<MartialStatus, bool>>>()))
                .ReturnsAsync(martialStatuses);

            GetMartialStatusesQuery query = new GetMartialStatusesQuery();

            // Action
            var result = _getMartialStatusesQueryHandler.Handle(query, CancellationToken.None).Result;

            // Assertion
            result.Success.Should()
                .BeTrue();
            result.Data.Should()
                .NotBeNull();
            result.Data.Count.Should()
                .Be(martialStatuses.Count);
        }

        [Test]
        public void GetMartialStatusesQueryHandler_ShouldReturnSuccessAndEmptyData_WhenEmptyMartialStatusesInRepository()
        {
            // Arrange
            _martialStatusRepositoryMock.Setup(
                m => m.GetListAsync(It.IsAny<Expression<Func<MartialStatus, bool>>>()))
                .ReturnsAsync(new List<MartialStatus>());

            GetMartialStatusesQuery query = new GetMartialStatusesQuery();

            // Action
            var result = _getMartialStatusesQueryHandler.Handle(query, CancellationToken.None).Result;

            // Assertion
            result.Success.Should()
            .BeTrue();

            result.Data.Should()
            .BeNull();

            result.Data.Count.Should()
                .Be(0);
        }

        [Test]
        public void GetMartialStatusesQueryHandler_ShouldMapMartialStatusToMartialStatusResponse_WhenCorrectly_GivenMultipleData()
        {
            // Arrange
            int dataCount = 15;

            _fixture.Customize<MartialStatus>(
                m => m.With(x => x.IsDeleted, false));

            var martialStatuses = _fixture.CreateMany<MartialStatus>(dataCount).ToList();

            _martialStatusRepositoryMock.Setup(
                m => m.GetListAsync(It.IsAny<Expression<Func<MartialStatus, bool>>>()))
                .ReturnsAsync(martialStatuses);

            GetMartialStatusesQuery query = new GetMartialStatusesQuery();

            // Action
            var result = _getMartialStatusesQueryHandler.Handle(query, CancellationToken.None).Result;

            // Assertion
            for (int i = 0; i < martialStatuses.Count; i++)
            {
                result.Data[i].Id.Should()
                   .Be(martialStatuses[i].Id);

                result.Data[i].Name.Should()
                   .Be(martialStatuses[i].Name);

                result.Data[i].Status.Should()
                   .Be(martialStatuses[i].Status);

                result.Data[i].Descriptions.Should()
                   .Be(martialStatuses[i].Descriptions);
            }
        }
    }
}
