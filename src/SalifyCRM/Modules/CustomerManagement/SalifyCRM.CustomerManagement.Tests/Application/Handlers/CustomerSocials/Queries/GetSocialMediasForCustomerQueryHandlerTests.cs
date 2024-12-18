using AutoFixture.AutoMoq;
using AutoFixture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Moq;
using SalifyCRM.CustomerManagement.Application.Handlers.CustomerSocials.Queries;
using SalifyCRM.CustomerManagement.Application.RepositoryInterfaces;

namespace SalifyCRM.CustomerManagement.Tests.Application.Handlers.CustomerSocials.Queries
{
    [TestFixture]
    [Category("Query")]
    [Category("SocialMedia")]
    public class GetSocialMediasForCustomerQueryHandlerTests
    {
        private Mock<ICustomerSocialRepository> _customerSocialRepositoryMock;
        private Mock<ICustomerRepository> _customerRepositoryMock;
        private Mock<IMediator> _mediatorMock;

        private IFixture _fixture;
        private GetSocialMediasForCustomerQuery.GetSocialMediasForCustomerQueryHandler _getSocialMediasForCustomerQueryHandler;

        [SetUp]
        public void SetUp()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());

            _customerSocialRepositoryMock = _fixture.Freeze<Mock<ICustomerSocialRepository>>();
            _customerRepositoryMock = _fixture.Freeze<Mock<ICustomerRepository>>();
            _mediatorMock = _fixture.Freeze<Mock<IMediator>>();

            _getSocialMediasForCustomerQueryHandler = new GetSocialMediasForCustomerQuery
                .GetSocialMediasForCustomerQueryHandler(
                _customerSocialRepositoryMock.Object,
                _customerRepositoryMock.Object,
                _mediatorMock.Object);
        }
    }
}
