using AutoFixture.AutoMoq;
using AutoFixture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Moq;
using SalifyCRM.CustomerManagement.Application.Handlers.Addresses.Queries;
using SalifyCRM.CustomerManagement.Application.RepositoryInterfaces;

namespace SalifyCRM.CustomerManagement.Tests.Application.Handlers.CustomerAddresses.Queries
{
    [TestFixture]
    [Category("Query")]
    [Category("Address")]
    public class GetAddressesForCustomerQueryHandlerTests
    {
        private Mock<IAddressRepository> _addressRepositoryMock;
        private Mock<ICustomerRepository> _customerRepositoryMock;
        private Mock<IMediator> _mediatorMock;

        private IFixture _fixture;
        private GetAddressesForCustomerQuery.GetAddressesForCustomerQueryHandler _getAddressesForCustomerQueryHandler;

        [SetUp]
        public void SetUp()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());

            _addressRepositoryMock = _fixture.Freeze<Mock<IAddressRepository>>();
            _customerRepositoryMock = _fixture.Freeze<Mock<ICustomerRepository>>();
            _mediatorMock = _fixture.Freeze<Mock<IMediator>>();

            _getAddressesForCustomerQueryHandler = new GetAddressesForCustomerQuery
                .GetAddressesForCustomerQueryHandler(
                _addressRepositoryMock.Object,
                _customerRepositoryMock.Object,
                _mediatorMock.Object);
        }
    }
}
