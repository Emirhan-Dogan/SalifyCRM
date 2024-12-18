using AutoFixture;
using AutoFixture.AutoMoq;
using MediatR;
using Moq;
using SalifyCRM.CustomerManagement.Application.Handlers.CustomerTypes.Queries;
using SalifyCRM.CustomerManagement.Application.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.CustomerManagement.Tests.Application.Handlers.CustomerTypes.Queries
{
    [TestFixture]
    [Category("Query")]
    [Category("CustomerType")]
    public class GetCustomerTypeQueryHandlerTests
    {
        private Mock<ICustomerTypeRepository> _customerTypeRepositoryMock;
        private Mock<ICustomerRepository> _customerRepositoryMock;
        private Mock<IMediator> _mediatorMock;

        private GetCustomerTypeQuery.GetCustomerTypeQueryHandler _getCustomerTypeQueryHandler;
        private IFixture _fixture;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());

            _customerTypeRepositoryMock = _fixture.Freeze<Mock<ICustomerTypeRepository>>();
            _customerRepositoryMock = _fixture.Freeze<Mock<ICustomerRepository>>();
            _mediatorMock = _fixture.Freeze<Mock<IMediator>>();

            _getCustomerTypeQueryHandler = new GetCustomerTypeQuery
                .GetCustomerTypeQueryHandler(
                _customerTypeRepositoryMock.Object,
                _customerRepositoryMock.Object,
                _mediatorMock.Object);
        }

        [Test]
        public void GetCustomerTagQueryHandler_ShouldReturnSuccessWithData_WhenCustomerTagExist()
        {

        }
    }
}
