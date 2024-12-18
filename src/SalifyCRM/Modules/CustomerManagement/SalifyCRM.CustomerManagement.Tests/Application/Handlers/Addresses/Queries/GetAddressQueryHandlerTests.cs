using AutoFixture.AutoMoq;
using AutoFixture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalifyCRM.CustomerManagement.Application.Handlers.Addresses.Queries;
using Moq;
using MediatR;
using SalifyCRM.CustomerManagement.Application.RepositoryInterfaces;

namespace SalifyCRM.CustomerManagement.Tests.Application.Handlers.Addresses.Queries
{
    [TestFixture]
    [Category("Query")]
    [Category("Address")]
    public class GetAddressQueryHandlerTests
    {
        private Mock<IAddressRepository> _addressRepositoryMock;
        private Mock<IMediator> _mediatorMock;

        private IFixture _fixture;
        private GetAddressQuery.GetAddressQueryHandler _getAddressQueryHandler;

        [SetUp]
        public void SetUp()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());

            _addressRepositoryMock = _fixture.Freeze<Mock<IAddressRepository>>();
            _mediatorMock = _fixture.Freeze<Mock<IMediator>>();

            _getAddressQueryHandler = new GetAddressQuery
                .GetAddressQueryHandler(
                _addressRepositoryMock.Object,
                _mediatorMock.Object);
        }
    }
}
