using AutoFixture;
using AutoFixture.AutoMoq;
using MediatR;
using Moq;
using SalifyCRM.CustomerManagement.Application.Handlers.MartialStatuses.Queries;
using SalifyCRM.CustomerManagement.Application.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.CustomerManagement.Tests.Application.Handlers.MartialStatuses.Queries
{
    [TestFixture]
    [Category("Query")]
    [Category("MartialStatus")]
    public class GetMartialStatusQueryHandlerTests
    {
        private Mock<IMartialStatusRepository> _martialStatusRepositoryMock;
        private Mock<ICustomerRepository> _customerRepositoryMock;
        private Mock<IMediator> _mediatorMock;

        private GetMartialStatusQuery.GetMartialStatusQueryHandler _getMartialStatusQueryHandler;
        private IFixture _fixture;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());

            _martialStatusRepositoryMock = _fixture.Freeze<Mock<IMartialStatusRepository>>();
            _customerRepositoryMock = _fixture.Freeze<Mock<ICustomerRepository>>();
            _mediatorMock = _fixture.Freeze<Mock<IMediator>>();

            _getMartialStatusQueryHandler = new GetMartialStatusQuery
                .GetMartialStatusQueryHandler(
                _martialStatusRepositoryMock.Object,
                _customerRepositoryMock.Object,
                _mediatorMock.Object);
        }

        [Test]
        public void GetCustomerTagQueryHandler_ShouldReturnSuccessWithData_WhenCustomerTagExist()
        {

        }
    }
}
