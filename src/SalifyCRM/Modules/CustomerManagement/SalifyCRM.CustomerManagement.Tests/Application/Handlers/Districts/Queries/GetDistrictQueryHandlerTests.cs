using AutoFixture.AutoMoq;
using AutoFixture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalifyCRM.CustomerManagement.Application.Handlers.Districts.Queries;
using Moq;
using SalifyCRM.CustomerManagement.Application.RepositoryInterfaces;
using MediatR;

namespace SalifyCRM.CustomerManagement.Tests.Application.Handlers.Districts.Queries
{
    [TestFixture]
    [Category("Query")]
    [Category("District")]
    public class GetDistrictQueryHandlerTests
    {
        private Mock<IDistrictRepository> _districtRepositoryMock;
        private Mock<IMediator> _mediatorMock;

        private IFixture _fixture;
        private GetDistrictQuery.GetDistrictQueryHandler _getDistrictQueryHandler;

        [SetUp]
        public void SetUp()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());

            _districtRepositoryMock = _fixture.Freeze<Mock<IDistrictRepository>>();
            _mediatorMock = _fixture.Freeze<Mock<IMediator>>();

            _getDistrictQueryHandler = new GetDistrictQuery
                .GetDistrictQueryHandler(
                _districtRepositoryMock.Object,
                _mediatorMock.Object);
        }
    }
}
