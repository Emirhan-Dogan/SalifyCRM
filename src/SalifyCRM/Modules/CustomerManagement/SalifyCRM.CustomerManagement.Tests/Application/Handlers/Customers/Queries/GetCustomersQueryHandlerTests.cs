using AutoFixture;
using AutoFixture.AutoMoq;
using MediatR;
using Moq;
using SalifyCRM.CustomerManagement.Application.Handlers.Customers.Queries;
using SalifyCRM.CustomerManagement.Application.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.CustomerManagement.Tests.Application.Handlers.Customers.Queries
{
    [TestFixture]
    [Category("Query")]
    [Category("Customer")]
    public class GetCustomersQueryHandlerTests
    {
        private Mock<ICustomerRepository> _customerRepositoryMock;
        private Mock<IMediator> _mediatorMock;

        private GetCustomersQuery.GetCustomersQueryHandler _getCustomersQueryHandler;
        private IFixture _fixture;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());

            _customerRepositoryMock = _fixture.Freeze<Mock<ICustomerRepository>>();
            _mediatorMock = _fixture.Freeze<Mock<IMediator>>();

            _getCustomersQueryHandler = new GetCustomersQuery
                .GetCustomersQueryHandler(
                _customerRepositoryMock.Object,
                _mediatorMock.Object);
        }
    }
}
