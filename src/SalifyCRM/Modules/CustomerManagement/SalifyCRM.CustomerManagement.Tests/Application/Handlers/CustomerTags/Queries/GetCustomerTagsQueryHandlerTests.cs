using AutoFixture;
using AutoFixture.AutoMoq;
using FluentAssertions;
using MediatR;
using Moq;
using SalifyCRM.CustomerManagement.Application.Handlers.CustomerTags.Queries;
using SalifyCRM.CustomerManagement.Application.RepositoryInterfaces;
using SalifyCRM.CustomerManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.CustomerManagement.Tests.Application.Handlers.CustomerTags.Queries
{
    [TestFixture]
    [Category("Query")]
    [Category("CustomerTag")]
    public class GetCustomerTagsQueryHandlerTests
    {
        private Mock<ICustomerTagRepository> _customerTagRepositoryMock;
        private Mock<IMediator> _mediatorMock;

        private GetCustomerTagsQuery.GetCustomerTagsQueryHandler _getCustomerTagsQueryHandler;
        private IFixture _fixture;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());

            _customerTagRepositoryMock = _fixture.Freeze<Mock<ICustomerTagRepository>>();
            _mediatorMock = _fixture.Freeze<Mock<IMediator>>();

            _getCustomerTagsQueryHandler = new GetCustomerTagsQuery
                .GetCustomerTagsQueryHandler(
                _customerTagRepositoryMock.Object,
                _mediatorMock.Object);
        }

        [Test]
        public void GetCustomerTagsQueryHandler_ShouldReturnSuccessWithData_WhenCustomerTagsExist()
        {
            // Arrange
            int dataCount = 15;

            _fixture.Customize<CustomerTag>(
                c => c.With(x => x.IsDeleted, false));

            var customerTags = _fixture.CreateMany<CustomerTag>(dataCount).ToList();

            _customerTagRepositoryMock.Setup(
                c => c.GetListAsync(It.IsAny<Expression<Func<CustomerTag, bool>>>()))
                .ReturnsAsync(customerTags);

            GetCustomerTagsQuery query = new GetCustomerTagsQuery();

            // Action
            var result = _getCustomerTagsQueryHandler.Handle(query, CancellationToken.None).Result;

            // Assertion
            result.Success.Should()
                .BeTrue();
            result.Data.Should()
                .NotBeNull();
            result.Data.Count.Should()
                .Be(customerTags.Count);
        }

        [Test]
        public void GetCustomerTagsQueryHandler_ShouldReturnSuccessAndEmptyData_WhenEmptyCustomerTagsInRepository()
        {
            // Arrange
            _customerTagRepositoryMock.Setup(
                c => c.GetListAsync(It.IsAny<Expression<Func<CustomerTag, bool>>>()))
                .ReturnsAsync(new List<CustomerTag>());

            GetCustomerTagsQuery query = new GetCustomerTagsQuery();

            // Action
            var result = _getCustomerTagsQueryHandler.Handle(query, CancellationToken.None).Result;

            // Assertion
            result.Success.Should()
            .BeTrue();

            result.Data.Should()
            .BeNull();

            result.Data.Count.Should()
                .Be(0);
        }

        [Test]
        public void GetCustomerTagsQueryHandler_ShouldMapCustomerTagToCustomerTagResponse_WhenCorrectly_GivenMultipleData()
        {
            // Arrange
            int dataCount = 15;

            _fixture.Customize<CustomerTag>(
                c => c.With(x => x.IsDeleted, false));

            var customerTags = _fixture.CreateMany<CustomerTag>(dataCount).ToList();

            _customerTagRepositoryMock.Setup(
                c => c.GetListAsync(It.IsAny<Expression<Func<CustomerTag, bool>>>()))
                .ReturnsAsync(customerTags);

            GetCustomerTagsQuery query = new GetCustomerTagsQuery();

            // Action
            var result = _getCustomerTagsQueryHandler.Handle(query, CancellationToken.None).Result;

            // Assertion
            for (int i = 0; i < customerTags.Count; i++)
            {
                result.Data[i].Id.Should()
                   .Be(customerTags[i].Id);

                result.Data[i].Name.Should()
                   .Be(customerTags[i].Name);

                result.Data[i].Status.Should()
                   .Be(customerTags[i].Status);

                result.Data[i].Descriptions.Should()
                   .Be(customerTags[i].Descriptions);
            }
        }
    }
}
