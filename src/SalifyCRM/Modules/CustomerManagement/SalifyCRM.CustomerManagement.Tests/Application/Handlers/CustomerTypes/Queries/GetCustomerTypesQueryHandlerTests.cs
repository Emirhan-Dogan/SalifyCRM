using AutoFixture;
using AutoFixture.AutoMoq;
using FluentAssertions;
using MediatR;
using Moq;
using SalifyCRM.CustomerManagement.Application.Handlers.CustomerTypes.Queries;
using SalifyCRM.CustomerManagement.Application.RepositoryInterfaces;
using SalifyCRM.CustomerManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.CustomerManagement.Tests.Application.Handlers.CustomerTypes.Queries
{
    [TestFixture]
    [Category("Query")]
    [Category("CustomerType")]
    public class GetCustomerTypesQueryHandlerTests
    {
        private Mock<ICustomerTypeRepository> _customerTypeRepositoryMock;
        private Mock<IMediator> _mediatorMock;

        private GetCustomerTypesQuery.GetCustomerTypesQueryHandler _getCustomerTypesQueryHandler;
        private IFixture _fixture;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());

            _customerTypeRepositoryMock = _fixture.Freeze<Mock<ICustomerTypeRepository>>();
            _mediatorMock = _fixture.Freeze<Mock<IMediator>>();

            _getCustomerTypesQueryHandler = new GetCustomerTypesQuery
                .GetCustomerTypesQueryHandler(
                _customerTypeRepositoryMock.Object,
                _mediatorMock.Object);
        }

        [Test]
        public void GetCustomerTypesQueryHandler_ShouldReturnSuccessWithData_WhenCustomerTypesExist()
        {
            // Arrange
            int dataCount = 15;

            _fixture.Customize<CustomerType>(
                m => m.With(x => x.IsDeleted, false));

            var customerTypes = _fixture.CreateMany<CustomerType>(dataCount).ToList();

            _customerTypeRepositoryMock.Setup(
                c => c.GetListAsync(It.IsAny<Expression<Func<CustomerType, bool>>>()))
                .ReturnsAsync(customerTypes);

            GetCustomerTypesQuery query = new GetCustomerTypesQuery();

            // Action
            var result = _getCustomerTypesQueryHandler.Handle(query, CancellationToken.None).Result;

            // Assertion
            result.Success.Should()
                .BeTrue();
            result.Data.Should()
                .NotBeNull();
            result.Data.Count.Should()
                .Be(customerTypes.Count);
        }

        [Test]
        public void GetCustomerTypesQueryHandler_ShouldReturnSuccessAndEmptyData_WhenEmptyCustomerTypesInRepository()
        {
            // Arrange
            _customerTypeRepositoryMock.Setup(
                c => c.GetListAsync(It.IsAny<Expression<Func<CustomerType, bool>>>()))
                .ReturnsAsync(new List<CustomerType>());

            GetCustomerTypesQuery query = new GetCustomerTypesQuery();

            // Action
            var result = _getCustomerTypesQueryHandler.Handle(query, CancellationToken.None).Result;

            // Assertion
            result.Success.Should()
            .BeTrue();

            result.Data.Should()
            .BeNull();

            result.Data.Count.Should()
                .Be(0);
        }

        [Test]
        public void GetCustomerTypesQueryHandler_ShouldMapCustomerTypeToCustomerTypeResponse_WhenCorrectly_GivenMultipleData()
        {
            // Arrange
            int dataCount = 15;

            _fixture.Customize<CustomerType>(
                m => m.With(x => x.IsDeleted, false));

            var customerTypes = _fixture.CreateMany<CustomerType>(dataCount).ToList();

            _customerTypeRepositoryMock.Setup(
                c => c.GetListAsync(It.IsAny<Expression<Func<CustomerType, bool>>>()))
                .ReturnsAsync(customerTypes);

            GetCustomerTypesQuery query = new GetCustomerTypesQuery();

            // Action
            var result = _getCustomerTypesQueryHandler.Handle(query, CancellationToken.None).Result;

            // Assertion
            for (int i = 0; i < customerTypes.Count; i++)
            {
                result.Data[i].Id.Should()
                   .Be(customerTypes[i].Id);

                result.Data[i].Name.Should()
                   .Be(customerTypes[i].Name);

                result.Data[i].Status.Should()
                   .Be(customerTypes[i].Status);

                result.Data[i].Descriptions.Should()
                   .Be(customerTypes[i].Descriptions);
            }
        }
    }
}
