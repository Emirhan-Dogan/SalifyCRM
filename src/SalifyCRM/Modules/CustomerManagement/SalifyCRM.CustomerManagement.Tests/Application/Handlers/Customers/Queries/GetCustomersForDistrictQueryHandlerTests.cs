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
    public class GetCustomersForDistrictQueryHandlerTests
    {
        private Mock<ICustomerRepository> _customerRepositoryMock;
        private Mock<IDistrictRepository> _districtRepositoryMock;
        private Mock<IMediator> _mediatorMock;

        private GetCustomersForDistrictQuery.GetCustomersForDistrictQueryHandler _getCustomersForDistrictQueryHandler;
        private IFixture _fixture;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());

            _customerRepositoryMock = _fixture.Freeze<Mock<ICustomerRepository>>();
            _districtRepositoryMock = _fixture.Freeze<Mock<IDistrictRepository>>();
            _mediatorMock = _fixture.Freeze<Mock<IMediator>>();

            _getCustomersForDistrictQueryHandler = new GetCustomersForDistrictQuery
                .GetCustomersForDistrictQueryHandler(
                _customerRepositoryMock.Object,
                _districtRepositoryMock.Object,
                _mediatorMock.Object);
        }
    }
}
