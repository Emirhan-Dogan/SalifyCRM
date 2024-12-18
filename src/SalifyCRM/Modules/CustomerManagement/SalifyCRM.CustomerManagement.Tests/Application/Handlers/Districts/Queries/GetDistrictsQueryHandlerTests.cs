using AutoFixture;
using AutoFixture.AutoMoq;
using MediatR;
using Moq;
using SalifyCRM.CustomerManagement.Application.Handlers.Districts.Queries;
using SalifyCRM.CustomerManagement.Application.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.CustomerManagement.Tests.Application.Handlers.Districts.Queries
{
    [TestFixture]
    [Category("Query")]
    [Category("District")]
    public class GetDistrictsQueryHandlerTests
    {
        private Mock<IDistrictRepository> _districtRepositoryMock;
        private Mock<IMediator> _mediatorMock;

        private IFixture _fixture;
        private GetDistrictsQuery.GetDistrictsQueryHandler _getDistrictsQueryHandler;

        [SetUp]
        public void SetUp()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());

            _districtRepositoryMock = _fixture.Freeze<Mock<IDistrictRepository>>();
            _mediatorMock = _fixture.Freeze<Mock<IMediator>>();

            _getDistrictsQueryHandler = new GetDistrictsQuery
                .GetDistrictsQueryHandler(
                _districtRepositoryMock.Object,
                _mediatorMock.Object);
        }
    }
}
