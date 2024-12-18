using AutoFixture.AutoMoq;
using AutoFixture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalifyCRM.CustomerManagement.Application.Handlers.Customers.Queries;
using MediatR;
using Moq;
using SalifyCRM.CustomerManagement.Application.RepositoryInterfaces;

namespace SalifyCRM.CustomerManagement.Tests.Application.Handlers.Customers.Queries
{
    [TestFixture]
    [Category("Query")]
    [Category("Customer")]
    public class GetCustomersForCustomerCategoryQueryHandlerTests
    {
        private Mock<ICustomerRepository> _customerRepositoryMock;
        private Mock<ICustomerCategoryRepository> _customerCategoryRepositoryMock;
        private Mock<IMediator> _mediatorMock;

        private GetCustomersForCustomerCategoryQuery.GetCustomersForCustomerCategoryQueryHandler _getCustomersForCustomerCategoryQueryHandler;
        private IFixture _fixture;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());

            _customerRepositoryMock = _fixture.Freeze<Mock<ICustomerRepository>>();
            _customerCategoryRepositoryMock = _fixture.Freeze<Mock<ICustomerCategoryRepository>>();
            _mediatorMock = _fixture.Freeze<Mock<IMediator>>();

            _getCustomersForCustomerCategoryQueryHandler = new GetCustomersForCustomerCategoryQuery
                .GetCustomersForCustomerCategoryQueryHandler(
                _customerRepositoryMock.Object,
                _customerCategoryRepositoryMock.Object,
                _mediatorMock.Object);
        }
    }
}
