using AutoFixture.AutoMoq;
using AutoFixture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Moq;
using SalifyCRM.CustomerManagement.Application.Handlers.CustomerTagMappings.Queries;
using SalifyCRM.CustomerManagement.Application.RepositoryInterfaces;

namespace SalifyCRM.CustomerManagement.Tests.Application.Handlers.CustomerTagMappings.Queries
{
    [TestFixture]
    [Category("Query")]
    [Category("CustomerTag")]
    public class GetCustomerTagsForCustomerQueryHandlerTests
    {
        private Mock<ICustomerTagMappingRepository> _customerTagMappingRepositoryMock;
        private Mock<ICustomerRepository> _customerRepositoryMock;
        private Mock<IMediator> _mediatorMock;

        private IFixture _fixture;
        private GetCustomerTagsForCustomerQuery.GetCustomerTagsForCustomerQueryHandler _getCustomerTagsForCustomerQueryHandler;

        [SetUp]
        public void SetUp()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());


            _customerTagMappingRepositoryMock = _fixture.Freeze<Mock<ICustomerTagMappingRepository>>();
            _customerRepositoryMock = _fixture.Freeze<Mock<ICustomerRepository>>();
            _mediatorMock = _fixture.Freeze<Mock<IMediator>>();

            _getCustomerTagsForCustomerQueryHandler = new GetCustomerTagsForCustomerQuery
                .GetCustomerTagsForCustomerQueryHandler(
                _customerTagMappingRepositoryMock.Object,
                _customerRepositoryMock.Object,
                _mediatorMock.Object);
        }
    }
}
