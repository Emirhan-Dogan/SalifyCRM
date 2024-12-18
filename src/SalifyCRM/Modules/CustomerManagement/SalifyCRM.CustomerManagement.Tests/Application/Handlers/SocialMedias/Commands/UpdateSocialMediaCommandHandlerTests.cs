using AutoFixture;
using AutoFixture.AutoMoq;
using MediatR;
using Moq;
using SalifyCRM.CustomerManagement.Application.Handlers.SocialMedias.Commands;
using SalifyCRM.CustomerManagement.Application.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.CustomerManagement.Tests.Application.Handlers.SocialMedias.Commands
{
    [TestFixture]
    [Category("Command")]
    [Category("Update")]
    [Category("SocialMedia")]
    public class UpdateSocialMediaCommandHandlerTests
    {
        private Mock<ISocialMediaRepository> _socialMediaRepositoryMock;
        private Mock<IUserRepository> _userRepositoryMock;
        private Mock<IMediator> _mediatorMock;

        private IFixture _fixture;
        private UpdateSocialMediaCommand.UpdateSocialMediaCommandHandler _updateSocialMediaCommandHandler;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());

            _socialMediaRepositoryMock = _fixture.Freeze<Mock<ISocialMediaRepository>>();
            _userRepositoryMock = _fixture.Freeze<Mock<IUserRepository>>();
            _mediatorMock = _fixture.Freeze<Mock<IMediator>>();

            _updateSocialMediaCommandHandler = new UpdateSocialMediaCommand
                .UpdateSocialMediaCommandHandler(
                _socialMediaRepositoryMock.Object,
                _userRepositoryMock.Object,
                _mediatorMock.Object);
        }
    }
}
