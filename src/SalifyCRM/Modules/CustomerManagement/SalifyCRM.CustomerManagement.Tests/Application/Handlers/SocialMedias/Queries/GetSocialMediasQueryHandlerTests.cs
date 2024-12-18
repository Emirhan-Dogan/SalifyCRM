using AutoFixture;
using AutoFixture.AutoMoq;
using FluentAssertions;
using MediatR;
using Moq;
using SalifyCRM.CustomerManagement.Application.Handlers.SocialMedias.Queries;
using SalifyCRM.CustomerManagement.Application.RepositoryInterfaces;
using SalifyCRM.CustomerManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.CustomerManagement.Tests.Application.Handlers.SocialMedias.Queries
{
    [TestFixture]
    [Category("Query")]
    [Category("SocialMedia")]
    public class GetSocialMediasQueryHandlerTests
    {
        private Mock<ISocialMediaRepository> _socialMediaRepositoryMock;
        private Mock<IMediator> _mediatorMock;

        private GetSocialMediasQuery.GetSocialMediasQueryHandler _getSocialMediasQueryHandler;
        private IFixture _fixture;


        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());

            _mediatorMock = _fixture.Freeze<Mock<IMediator>>();
            _socialMediaRepositoryMock = _fixture.Freeze<Mock<ISocialMediaRepository>>();

            _getSocialMediasQueryHandler = new GetSocialMediasQuery
                .GetSocialMediasQueryHandler(
                _socialMediaRepositoryMock.Object,
                _mediatorMock.Object);
        }

        [Test]
        public void GetSocialMediasQueryHandler_ShouldReturnSuccessWithData_WhenSocialMediasExist()
        {
            // Arrange
            int dataCount = 15;
            
            _fixture.Customize<SocialMedia>(
                s => s.With(x => x.IsDeleted, false));
            
            var socialMedias = _fixture.CreateMany<SocialMedia>(dataCount);

            _socialMediaRepositoryMock.Setup(
                s => s.GetListAsync(It.IsAny<Expression<Func<SocialMedia, bool>>>()))
                .ReturnsAsync(socialMedias);

            GetSocialMediasQuery query = new GetSocialMediasQuery();

            // Action
            var result = _getSocialMediasQueryHandler.Handle(query, CancellationToken.None).Result;

            // Assertion
            result.Success.Should()
                .BeTrue();

            result.Data.Should()
                .NotBeNull();

            result.Data.Count.Should()
                .Be(socialMedias.Count());
        }

        [Test]
        public void GetSocialMediasQueryHandler_ShouldReturnSuccessAndEmptyData_WhenEmptySocialMediasInRepository()
        {
            // Arrange
            _socialMediaRepositoryMock.Setup(
                s => s.GetListAsync(It.IsAny<Expression<Func<SocialMedia, bool>>>()))
                .ReturnsAsync(new List<SocialMedia>());

            GetSocialMediasQuery query = new GetSocialMediasQuery();

            // Action
            var result = _getSocialMediasQueryHandler.Handle(query, CancellationToken.None).Result;

            // Assertion
            result.Success.Should()
                .BeTrue();

            result.Data.Should()
                .BeNull();

            result.Data.Count.Should()
                .Be(0);
        }

        [Test]
        public void GetSocialMediasQueryHandler_ShouldMapSocialMediaToSocialMediaResponse_WhenCorrectly_GivenMultipleData()
        {
            // Arrange
            int dataCount = 5;

            _fixture.Customize<SocialMedia>(
                s => s.With(x => x.IsDeleted, false));

            var socialMedias = _fixture.CreateMany<SocialMedia>(dataCount).ToList();

            _socialMediaRepositoryMock.Setup(
                s => s.GetListAsync(It.IsAny<Expression<Func<SocialMedia, bool>>>()))
                .ReturnsAsync(socialMedias);

            GetSocialMediasQuery query = new GetSocialMediasQuery();

            // Action
            var result = _getSocialMediasQueryHandler.Handle(query, CancellationToken.None).Result;

            // Assertion
            for ( int i = 0; i < socialMedias.Count(); i++ )
            {
                result.Data[i].Id.Should()
                    .Be(socialMedias[i].Id);

                result.Data[i].Name.Should()
                    .Be(socialMedias[i].Name);

                result.Data[i].Status.Should()
                    .Be(socialMedias[i].Status);
            }
        }
    }
}
