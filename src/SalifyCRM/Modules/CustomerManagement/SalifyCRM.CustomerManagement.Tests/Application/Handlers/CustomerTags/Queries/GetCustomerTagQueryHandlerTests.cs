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
using System.Linq.Expressions;
using System.Threading;
using NUnit.Framework;
using Castle.Core.Resource;

namespace SalifyCRM.CustomerManagement.Tests.Application.Handlers.CustomerTags.Queries
{
    [TestFixture]
    [Category("Query")]
    [Category("CustomerTag")]
    public class GetCustomerTagQueryHandlerTests
    {
        private Mock<ICustomerTagRepository> _customerTagRepositoryMock;
        private Mock<ICustomerRepository> _customerRepositoryMock;
        private Mock<IMediator> _mediatorMock;

        private GetCustomerTagQuery.GetCustomerTagQueryHandler _getCustomerTagQueryHandler;
        private IFixture _fixture;


        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());

            _customerTagRepositoryMock = _fixture.Freeze<Mock<ICustomerTagRepository>>();
            _customerRepositoryMock = _fixture.Freeze<Mock<ICustomerRepository>>();
            _mediatorMock = _fixture.Freeze<Mock<IMediator>>();

            _getCustomerTagQueryHandler = new GetCustomerTagQuery
                .GetCustomerTagQueryHandler(
                _customerTagRepositoryMock.Object,
                _customerRepositoryMock.Object,
                _mediatorMock.Object);
        }


        [Test]
        public void GetCustomerTagQueryHandler_ShouldReturnSuccessWithData_WhenCustomerTagExist()
        {
            // Arrange
            int customerCount = 10;
            _fixture.Customize<CustomerTag>(
                c => c.With(x => x.IsDeleted, false));

            _fixture.Customize<Customer>(
                c => c.With(x => x.IsDeleted, false));

            var customerTag = _fixture.Create<CustomerTag>();

            var customers = _fixture.CreateMany<Customer>(customerCount);

            _customerTagRepositoryMock.Setup(
                c =>
                c.GetAsync(It.IsAny<Expression<Func<CustomerTag, bool>>>()))
                .ReturnsAsync(customerTag);

            _customerRepositoryMock.Setup(
                c =>
                c.GetCustomersByCustomerTagId(It.IsAny<int>()))
                .Returns(customers.ToList());

            GetCustomerTagQuery query = new GetCustomerTagQuery()
            {
                Id = _fixture.Create<int>()
            };

            // Action
            var result = _getCustomerTagQueryHandler.Handle(query, CancellationToken.None).Result;

            // Assertion
            result.Success.Should()
                .BeTrue("Expected `Success` to be `true` but found `false`.");
            result.Data.Should()
                .NotBeNull("Expected `Data` to not be null, but it was null.");
            result.Data.Customers.Count.Should()
                .Be(customerCount,
                $"Expected `Customers` count to be `{customerCount}`, but found `{result.Data.Customers.Count}`.");
        }


        [Test]
        public void GetCustomerTagQueryHandler_ShouldReturnErrorWithEmptyData_WhenCustomerTagDoesNotExist()
        {
            // Arrange
            _customerTagRepositoryMock.Setup(
                c =>
                c.GetAsync(It.IsAny<Expression<Func<CustomerTag, bool>>>()))
                .ReturnsAsync(new CustomerTag());

            GetCustomerTagQuery query = new GetCustomerTagQuery()
            {
                Id = _fixture.Create<int>()
            };

            // Action
            var result = _getCustomerTagQueryHandler.Handle(query, CancellationToken.None).Result;

            // Assertion
            result.Success.Should()
                .BeFalse("Expected `Success` to be `false`, but found `true`.");
            result.Data.Should()
                .BeNull("Expected `Data` to be null, but found non-null data.");
        }


        [Test]
        public void GetCustomerTagQueryHandler_ShouldMapCustomerTagToCustomerTagDetailResponse_WhenCustomerTagExist()
        {
            // Arrange
            int customerCount = 10;
            _fixture.Customize<CustomerTag>(
                c => c.With(x => x.IsDeleted, false));

            _fixture.Customize<Customer>(
                c => c.With(x => x.IsDeleted, false));

            var customerTag = _fixture.Create<CustomerTag>();

            var customers = _fixture.CreateMany<Customer>(customerCount).ToList();

            _customerTagRepositoryMock.Setup(
                c =>
                c.GetAsync(It.IsAny<Expression<Func<CustomerTag, bool>>>()))
                .ReturnsAsync(customerTag);

            _customerRepositoryMock.Setup(
                c =>
                c.GetCustomersByCustomerTagId(It.IsAny<int>()))
                .Returns(customers);

            GetCustomerTagQuery query = new GetCustomerTagQuery()
            {
                Id = _fixture.Create<int>()
            };

            // Action
            var result = _getCustomerTagQueryHandler.Handle(query, CancellationToken.None).Result;

            // Assertion
            result.Data.Should()
                .NotBeNull("Expected `Data` to not be null, but it was null.");
            result.Data.Id.Should()
                .Be(customerTag.Id, $"Expected `Data.Id` to be `{customerTag.Id}`, but found `{result.Data.Id}`.");
            result.Data.Name.Should()
                .Be(customerTag.Name, $"Expected `Data.Name` to be `{customerTag.Name}`, but found `{result.Data.Name}`.");
            result.Data.Status.Should()
                .Be(customerTag.Status, $"Expected `Data.Status` to be `{customerTag.Status}`, but found `{result.Data.Status}`.");

            for (int i = 0; i < result.Data.Customers.Count; i++)
            {
                result.Data.Customers[i].Id.Should()
                    .Be(customers[i].Id,
                    $"Customer ID mismatch at index `{i}`: Expected `{customers[i].Id}`, but found `{result.Data.Customers[i].Id}`.");
            }
        }


        [Test]
        public void GetCustomerTagQueryHandler_ShouldReturnErrorWithEmptyData_WhenCustomerTagIdIsNull()
        {
            // Arrange
            _customerTagRepositoryMock.Setup(
                c =>
                c.GetAsync(It.IsAny<Expression<Func<CustomerTag, bool>>>()))
                .ReturnsAsync(new CustomerTag());

            _customerRepositoryMock.Setup(
                c =>
                c.GetCustomersByCustomerTagId(It.IsAny<int>()))
                .Returns(new List<Customer>());

            GetCustomerTagQuery query = new GetCustomerTagQuery();
            Console.WriteLine(query.Id);
            // Action
            var result = _getCustomerTagQueryHandler.Handle(query, CancellationToken.None).Result;

            // Assertion
            result.Success.Should()
                .BeFalse("Expected `Success` to be `false`, but found `true`.");
            result.Data.Should()
                .BeNull("Expected `Data` to be null, but found non-null data.");
        }


        [Test]
        public void GetCustomerTagQueryHandler_ShouldReturnErrorWithEmptyData_WhenCustomerTagIdIsNegative()
        {
            // Arrange
            int customerCount = 10;
            _fixture.Customize<CustomerTag>(
                c => c.With(x => x.IsDeleted, false));

            _fixture.Customize<Customer>(
                c => c.With(x => x.IsDeleted, false));

            var customerTag = _fixture.Create<CustomerTag>();

            var customers = _fixture.CreateMany<Customer>(customerCount).ToList();

            _customerTagRepositoryMock.Setup(
                c =>
                c.GetAsync(It.IsAny<Expression<Func<CustomerTag, bool>>>()))
                .ReturnsAsync(customerTag);

            _customerRepositoryMock.Setup(
                c =>
                c.GetCustomersByCustomerTagId(It.IsAny<int>()))
                .Returns(customers);

            int negativeNumber = -Math.Abs(_fixture.Create<int>());

            GetCustomerTagQuery query = new GetCustomerTagQuery()
            {
                Id = negativeNumber
            };

            // Action
            var result = _getCustomerTagQueryHandler.Handle(query, CancellationToken.None).Result;

            // Assertion
            result.Success.Should()
                .BeFalse("Expected `Success` to be `false`, but found `true`.");
            result.Data.Should()
                .BeNull("Expected `Data` to be null, but found non-null data.");
        }


        [Test]
        public void GetCustomerTagQueryHandler_ShouldReturnSuccessWithDataButDataInNoCustomers_WhenEmptyCustomersInCustomerTagExist()
        {
            // Arrange
            _fixture.Customize<CustomerTag>(
                c => c.With(x => x.IsDeleted, false));

            var customerTag = _fixture.Create<CustomerTag>();

            _customerTagRepositoryMock.Setup(
                c =>
                c.GetAsync(It.IsAny<Expression<Func<CustomerTag, bool>>>()))
                .ReturnsAsync(customerTag);

            _customerRepositoryMock.Setup(
                c =>
                c.GetCustomersByCustomerTagId(It.IsAny<int>()))
                .Returns(new List<Customer>());

            GetCustomerTagQuery query = new GetCustomerTagQuery()
            {
                Id = _fixture.Create<int>()
            };

            // Action
            var result = _getCustomerTagQueryHandler.Handle(query, CancellationToken.None).Result;

            // Assertion
            result.Success.Should()
                .BeTrue("Expected `Success` to be `true` but found `false`.");
            result.Data.Should()
                .NotBeNull("Expected `Data` to not be null, but it was null.");
            result.Data.Customers.Should()
                .BeEmpty("Expected `Customers` to be empty, but found non-empty list.");
        }
    }
}
