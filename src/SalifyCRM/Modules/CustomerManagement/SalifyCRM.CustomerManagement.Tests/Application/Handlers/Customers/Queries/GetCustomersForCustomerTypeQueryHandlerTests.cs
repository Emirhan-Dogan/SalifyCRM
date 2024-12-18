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
    public class GetCustomersForCustomerTypeQueryHandlerTests
    {
        private Mock<ICustomerRepository> _customerRepositoryMock;
        private Mock<ICustomerTypeRepository> _customerTypeRepositoryMock;
        private Mock<IMediator> _mediatorMock;

        private GetCustomersForCustomerTypeQuery.GetCustomersForCustomerTypeQueryHandler _getCustomersForCustomerTypeQueryHandler;
        private IFixture _fixture;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());

            _customerRepositoryMock = _fixture.Freeze<Mock<ICustomerRepository>>();
            _customerTypeRepositoryMock = _fixture.Freeze<Mock<ICustomerTypeRepository>>();
            _mediatorMock = _fixture.Freeze<Mock<IMediator>>();

            _getCustomersForCustomerTypeQueryHandler = new GetCustomersForCustomerTypeQuery
                .GetCustomersForCustomerTypeQueryHandler(
                _customerRepositoryMock.Object,
                _customerTypeRepositoryMock.Object,
                _mediatorMock.Object);
        }
    }
}
