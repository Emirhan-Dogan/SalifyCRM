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

namespace SalifyCRM.CustomerManagement.Tests.Application.Handlers.Addresses.Queries
{
    [TestFixture]
    [Category("Query")]
    [Category("Address")]
    public class GetAddressesQueryHandlerTests
    {
        private Mock<IAddressRepository> _addressRepositoryMock;
        private Mock<IMediator> _mediatorMock;

        private IFixture _fixture;
        private GetAddressesQuery.GetAddressesQueryHandler _getAddressesQueryHandler;

        [SetUp]
        public void SetUp()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());

            _addressRepositoryMock = _fixture.Freeze<Mock<IAddressRepository>>();
            _mediatorMock = _fixture.Freeze<Mock<IMediator>>();

            _getAddressesQueryHandler = new GetAddressesQuery
                .GetAddressesQueryHandler(
                _addressRepositoryMock.Object,
                _mediatorMock.Object);
        }
    }
}
