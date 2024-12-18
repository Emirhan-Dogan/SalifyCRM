using AutoFixture;
using AutoFixture.AutoMoq;
using MediatR;
using Moq;
using SalifyCRM.CustomerManagement.Application.Handlers.Occupations.Queries;
using SalifyCRM.CustomerManagement.Application.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.CustomerManagement.Tests.Application.Handlers.Occupations.Queries
{
    [TestFixture]
    [Category("Query")]
    [Category("Occupation")]
    public class GetOccupationQueryHandlerTests
    {
        private Mock<IOccupationRepository> _occupationRepositoryMock;
        private Mock<ICustomerRepository> _customerRepositoryMock;
        private Mock<IMediator> _mediatorMock;

        private GetOccupationQuery.GetOccupationQueryHandler _getOccupationQueryHandler;
        private IFixture _fixture;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());

            _occupationRepositoryMock = _fixture.Freeze<Mock<IOccupationRepository>>();
            _customerRepositoryMock = _fixture.Freeze<Mock<ICustomerRepository>>();
            _mediatorMock = _fixture.Freeze<Mock<IMediator>>();

            _getOccupationQueryHandler = new GetOccupationQuery
                .GetOccupationQueryHandler(
                _occupationRepositoryMock.Object,
                _customerRepositoryMock.Object,
                _mediatorMock.Object);
        }

        [Test]
        public void GetCustomerTagQueryHandler_ShouldReturnSuccessWithData_WhenCustomerTagExist()
        {

        }
    }
}
