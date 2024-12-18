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
    public class GetCustomersForOccupationQueryHandlerTests
    {
        private Mock<ICustomerRepository> _customerRepositoryMock;
        private Mock<IOccupationRepository> _occupationRepositoryMock;
        private Mock<IMediator> _mediatorMock;

        private GetCustomersForOccupationQuery.GetCustomersForOccupationQueryHandler _getCustomersForOccupationQueryHandler;
        private IFixture _fixture;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());

            _customerRepositoryMock = _fixture.Freeze<Mock<ICustomerRepository>>();
            _occupationRepositoryMock = _fixture.Freeze<Mock<IOccupationRepository>>();
            _mediatorMock = _fixture.Freeze<Mock<IMediator>>();

            _getCustomersForOccupationQueryHandler = new GetCustomersForOccupationQuery
                .GetCustomersForOccupationQueryHandler(
                _customerRepositoryMock.Object,
                _occupationRepositoryMock.Object,
                _mediatorMock.Object);
        }
    }
}
