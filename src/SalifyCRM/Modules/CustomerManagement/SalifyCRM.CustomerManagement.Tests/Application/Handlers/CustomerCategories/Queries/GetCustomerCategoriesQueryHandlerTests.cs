using AutoFixture;
using AutoFixture.AutoMoq;
using MediatR;
using Moq;
using SalifyCRM.CustomerManagement.Application.Handlers.CustomerCategories.Queries;
using SalifyCRM.CustomerManagement.Application.RepositoryInterfaces;
using SalifyCRM.CustomerManagement.Application.Responses.CustomerCategories;
using SalifyCRM.CustomerManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using NUnit.Framework;
using FluentAssertions;

namespace SalifyCRM.CustomerManagement.Tests.Application.Handlers.CustomerCategories.Queries
{
    [TestFixture]
    [Category("Query")]
    [Category("CustomerCategory")]
    public class GetCustomerCategoriesQueryHandlerTests
    {
        private Mock<ICustomerCategoryRepository> _customerCategoryRepositoryMock;
        private Mock<IMediator> _mediatorMock;
        private GetCustomerCategoriesQuery.GetCustomerCategoriesQueryHandler _getCustomerCategoriesQueryHandler;
        private IFixture _fixture;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());

            _customerCategoryRepositoryMock = _fixture.Freeze<Mock<ICustomerCategoryRepository>>();
            _mediatorMock = _fixture.Freeze<Mock<IMediator>>();

            _getCustomerCategoriesQueryHandler = new GetCustomerCategoriesQuery
                .GetCustomerCategoriesQueryHandler(
                _customerCategoryRepositoryMock.Object,
                _mediatorMock.Object);
        }

        [Test]
        public async Task GetCustomerCategoriesQueryHandler_ShouldReturnSuccessWithData_WhenCategoriesExist()
        {
            // Arrange
            int dataCount = 15;
            _fixture.Customize<CustomerCategory>(c => c.With(x => x.IsDeleted, false));
            var customerCategories = _fixture.CreateMany<CustomerCategory>(dataCount).ToList();

            _customerCategoryRepositoryMock.Setup(repo =>
                repo.GetListAsync(It.IsAny<Expression<Func<CustomerCategory, bool>>>()))
               .ReturnsAsync(customerCategories);

            // Action
            var result = await _getCustomerCategoriesQueryHandler.Handle(new GetCustomerCategoriesQuery(), CancellationToken.None);

            // Assertion
            result.Success.Should().BeTrue($"Expected 'True' for Success, but got '{result.Success}'.");
            result.Data.Should().NotBeNull("Expected Data to be not null, but it was null.");
            result.Data.Count.Should().Be(customerCategories.Count, 
                $"Expected Data count to be {customerCategories.Count}, but got {result.Data.Count}.");
        }

        [Test]
        public async Task GetCustomerCategoriesQueryHandler_ShouldReturnSuccessAndEmptyData_WhenEmptyCategoriesInRepository()
        {
            // Arrange
            _customerCategoryRepositoryMock.Setup(repo =>
            repo.GetListAsync(It.IsAny<Expression<Func<CustomerCategory, bool>>>()))
            .ReturnsAsync(new List<CustomerCategory>());

            // Action
            var result = await _getCustomerCategoriesQueryHandler.Handle(new GetCustomerCategoriesQuery(), CancellationToken.None);

            // Assertion
            result.Success.Should().BeTrue($"Expected 'True' for Success, but got '{result.Success}'.");
            result.Data.Should().NotBeNull("Expected Data to be not null, but it was null.");
            result.Data.Count.Should().Be(0, $"Expected Data count to be 0, but got {result.Data.Count}.");
        }

        [Test]
        public async Task GetCustomerCategoriesQueryHandler_ShouldMapCustomerCategoryToCustomerCategoryResponse_WhenCorrectly_GivenMultipleData()
        {
            // Arrange
            int dataCount = 5;
            _fixture.Customize<CustomerCategory>(c => c.With(x => x.IsDeleted, false));
            var customerCategories = _fixture.CreateMany<CustomerCategory>(dataCount).ToList();

            _customerCategoryRepositoryMock.Setup(repo =>
                repo.GetListAsync(It.IsAny<Expression<Func<CustomerCategory, bool>>>()))
                .ReturnsAsync(customerCategories);

            // Action
            var result = await _getCustomerCategoriesQueryHandler.Handle(new GetCustomerCategoriesQuery(), CancellationToken.None);

            // Assertion
            for (int i = 0; i < customerCategories.Count; i++)
            {
                result.Data[i].Id.Should().Be(customerCategories[i].Id,
                    $"Expected Id to be {customerCategories[i].Id} but got {result.Data[i].Id} at index {i}");
                result.Data[i].Name.Should().Be(customerCategories[i].Name,
                    $"Expected Name to be {customerCategories[i].Name} but got {result.Data[i].Name} at index {i}");
                result.Data[i].Status.Should().Be(customerCategories[i].Status,
                    $"Expected Status to be {customerCategories[i].Status} but got {result.Data[i].Status} at index {i}");
            }
        }
    }
}
