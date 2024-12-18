using AutoFixture.AutoMoq;
using AutoFixture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Moq;
using SalifyCRM.CustomerManagement.Application.RepositoryInterfaces;
using SalifyCRM.CustomerManagement.Application.Handlers.Cities.Queries;

namespace SalifyCRM.CustomerManagement.Tests.Application.Handlers.Cities.Queries
{
    [TestFixture]
    [Category("Query")]
    [Category("City")]
    public class GetCityQueryHandlerTests
    {
        private Mock<ICityRepository> _cityRepositoryMock;
        private Mock<IMediator> _mediatorMock;

        private IFixture _fixture;
        private GetCityQuery.GetCityQueryHandler _getCityQueryHandler;

        [SetUp]
        public void SetUp()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());

            _cityRepositoryMock = _fixture.Freeze<Mock<ICityRepository>>();
            _mediatorMock = _fixture.Freeze<Mock<IMediator>>();

            _getCityQueryHandler = new GetCityQuery
                .GetCityQueryHandler(
                _cityRepositoryMock.Object,
                _mediatorMock.Object);
        }
    }
}
