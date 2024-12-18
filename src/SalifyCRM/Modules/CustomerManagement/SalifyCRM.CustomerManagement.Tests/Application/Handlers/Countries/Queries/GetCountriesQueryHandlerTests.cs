using AutoFixture.AutoMoq;
using AutoFixture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalifyCRM.CustomerManagement.Application.Handlers.Countries.Queries;
using Moq;
using MediatR;
using SalifyCRM.CustomerManagement.Application.RepositoryInterfaces;

namespace SalifyCRM.CustomerManagement.Tests.Application.Handlers.Countries.Queries
{
    [TestFixture]
    [Category("Query")]
    [Category("Country")]
    public class GetCountriesQueryHandlerTests
    {
        private Mock<ICountryRepository> _countryRepositoryMock;
        private Mock<IMediator> _mediatorMock;

        private IFixture _fixture;
        private GetCountriesQuery.GetCountriesQueryHandler _getCountriesQueryHandler;

        [SetUp]
        public void SetUp()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());

            _countryRepositoryMock = _fixture.Freeze<Mock<ICountryRepository>>();
            _mediatorMock = _fixture.Freeze<Mock<IMediator>>();

            _getCountriesQueryHandler = new GetCountriesQuery
                .GetCountriesQueryHandler(
                _countryRepositoryMock.Object,
                _mediatorMock.Object);
        }
    }
}
