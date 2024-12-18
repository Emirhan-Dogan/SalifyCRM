using AutoFixture;
using AutoFixture.AutoMoq;
using MediatR;
using Moq;
using SalifyCRM.CustomerManagement.Application.Handlers.SocialMedias.Queries;
using SalifyCRM.CustomerManagement.Application.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.CustomerManagement.Tests.Application.Handlers.SocialMedias.Queries
{
    [TestFixture]
    [Category("Query")]
    [Category("SocialMedia")]
    public class GetSocialMediaQueryHandlerTests
    {
        private Mock<ISocialMediaRepository> _socialMediaRepositoryMock;
        private Mock<ICustomerRepository> _customerRepositoryMock;
        private Mock<IMediator> _mediatorMock;

        private GetSocialMediaQuery.GetSocialMediaQueryHandler _getSocialMediaQueryHandler;
        private IFixture _fixture;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());

            _socialMediaRepositoryMock = _fixture.Freeze<Mock<ISocialMediaRepository>>();
            _customerRepositoryMock = _fixture.Freeze<Mock<ICustomerRepository>>();
            _mediatorMock = _fixture.Freeze<Mock<IMediator>>();

            _getSocialMediaQueryHandler = new GetSocialMediaQuery
                .GetSocialMediaQueryHandler(
                _socialMediaRepositoryMock.Object,
                _customerRepositoryMock.Object,
                _mediatorMock.Object);
        }

        [Test]
        public void GetCustomerTagQueryHandler_ShouldReturnSuccessWithData_WhenCustomerTagExist()
        {

        }

        [Test]
        public void GetCustomerTagQueryHandler_ShouldReturnErrorWithEmptyData_WhenCustomerTagDoesNotExist()
        {

        }

        [Test]
        public void GetCustomerTagQueryHandler_ShouldMapCustomerTagToCustomerTagDetailResponse_WhenCustomerTagExist()
        {

        }

        [Test]
        public void GetCustomerTagQueryHandler_ShouldReturnErrorWithEmptyData_WhenCustomerTagIdIsNull()
        {

        }

        [Test]
        public void GetCustomerTagQueryHandler_ShouldReturnErrorWithEmptyData_WhenCustomerTagIdIsNegative()
        {

        }
    }
}
