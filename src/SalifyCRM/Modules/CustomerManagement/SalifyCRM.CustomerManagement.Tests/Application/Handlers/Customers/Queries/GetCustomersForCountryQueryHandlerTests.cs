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
    public class GetCustomersForCountryQueryHandlerTests
    {
        private Mock<ICustomerRepository> _customerRepositoryMock;
        private Mock<ICountryRepository> _countryRepositoryMock;
        private Mock<IMediator> _mediatorMock;

        private GetCustomersForCountryQuery.GetCustomersForCountryQueryHandler _getCustomersForCountryQueryHandler;
        private IFixture _fixture;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());

            _customerRepositoryMock = _fixture.Freeze<Mock<ICustomerRepository>>();
            _countryRepositoryMock = _fixture.Freeze<Mock<ICountryRepository>>();
            _mediatorMock = _fixture.Freeze<Mock<IMediator>>();

            _getCustomersForCountryQueryHandler = new GetCustomersForCountryQuery
                .GetCustomersForCountryQueryHandler(
                _customerRepositoryMock.Object,
                _countryRepositoryMock.Object,
                _mediatorMock.Object);
        }
    }
}
