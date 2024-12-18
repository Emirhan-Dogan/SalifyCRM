using AutoFixture.AutoMoq;
using AutoFixture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalifyCRM.CustomerManagement.Application.Handlers.Countries.Queries;
using Moq;
using SalifyCRM.CustomerManagement.Application.RepositoryInterfaces;
using MediatR;

namespace SalifyCRM.CustomerManagement.Tests.Application.Handlers.Countries.Queries
{
    [TestFixture]
    [Category("Query")]
    [Category("Country")]
    public class GetCountryQueryHandlerTests
    {
        private Mock<ICountryRepository> _countryRepositoryMock;
        private Mock<IMediator> _mediatorMock;

        private IFixture _fixture;
        private GetCountryQuery.GetCountryQueryHandler _getCountryQueryHandler;

        [SetUp]
        public void SetUp()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());

            _countryRepositoryMock = _fixture.Freeze<Mock<ICountryRepository>>();
            _mediatorMock = _fixture.Freeze<Mock<IMediator>>();

            _getCountryQueryHandler = new GetCountryQuery
                .GetCountryQueryHandler(
                _countryRepositoryMock.Object,
                _mediatorMock.Object);
        }
    }
}
