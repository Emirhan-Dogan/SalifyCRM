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
    public class GetCustomersForCustomerTagQueryHandlerTests
    {
        private Mock<ICustomerRepository> _customerRepositoryMock;
        private Mock<ICustomerTagRepository> _customerTagRepositoryMock;
        private Mock<IMediator> _mediatorMock;

        private GetCustomersForCustomerTagQuery.GetCustomersForCustomerTagQueryHandler _getCustomersForCustomerTagQueryHandler;
        private IFixture _fixture;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());

            _customerRepositoryMock = _fixture.Freeze<Mock<ICustomerRepository>>();
            _customerTagRepositoryMock = _fixture.Freeze<Mock<ICustomerTagRepository>>();
            _mediatorMock = _fixture.Freeze<Mock<IMediator>>();

            _getCustomersForCustomerTagQueryHandler = new GetCustomersForCustomerTagQuery
                .GetCustomersForCustomerTagQueryHandler(
                _customerRepositoryMock.Object,
                _customerTagRepositoryMock.Object,
                _mediatorMock.Object);
        }
    }
}
