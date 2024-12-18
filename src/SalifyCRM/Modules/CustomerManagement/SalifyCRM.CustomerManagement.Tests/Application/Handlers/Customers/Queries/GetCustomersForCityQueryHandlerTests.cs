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
    public class GetCustomersForCityQueryHandlerTests
    {
        private Mock<ICustomerRepository> _customerRepositoryMock;
        private Mock<ICityRepository> _cityRepositoryMock;
        private Mock<IMediator> _mediatorMock;

        private GetCustomersForCityQuery.GetCustomersForCityQueryHandler _getCustomersForCityQueryHandler;
        private IFixture _fixture;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());

            _customerRepositoryMock = _fixture.Freeze<Mock<ICustomerRepository>>();
            _cityRepositoryMock = _fixture.Freeze<Mock<ICityRepository>>();
            _mediatorMock = _fixture.Freeze<Mock<IMediator>>();

            _getCustomersForCityQueryHandler = new GetCustomersForCityQuery
                .GetCustomersForCityQueryHandler(
                _customerRepositoryMock.Object,
                _cityRepositoryMock.Object,
                _mediatorMock.Object);
        }
    }
}
