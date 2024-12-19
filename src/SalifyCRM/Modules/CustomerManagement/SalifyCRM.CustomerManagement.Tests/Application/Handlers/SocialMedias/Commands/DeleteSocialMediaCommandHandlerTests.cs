﻿using AutoFixture.AutoMoq;
using AutoFixture;
using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalifyCRM.CustomerManagement.Application.RepositoryInterfaces;
using SalifyCRM.CustomerManagement.Application.Handlers.SocialMedias.Commands;

namespace SalifyCRM.CustomerManagement.Tests.Application.Handlers.SocialMedias.Commands
{
    [TestFixture]
    [Category("Command")]
    [Category("Delete")]
    [Category("SocialMedia")]
    public class DeleteSocialMediaCommandHandlerTests
    {
        private Mock<ISocialMediaRepository> _socialMediaRepositoryMock;
        private Mock<IUserRepository> _userRepositoryMock;
        private Mock<IMediator> _mediatorMock;

        private IFixture _fixture;
        private DeleteSocialMediaCommand.DeleteSocialMediaCommandHandler _deleteSocialMediaCommandHandler;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());

            _socialMediaRepositoryMock = _fixture.Freeze<Mock<ISocialMediaRepository>>();
            _userRepositoryMock = _fixture.Freeze<Mock<IUserRepository>>();
            _mediatorMock = _fixture.Freeze<Mock<IMediator>>();

            _deleteSocialMediaCommandHandler = new DeleteSocialMediaCommand
                .DeleteSocialMediaCommandHandler(
                _socialMediaRepositoryMock.Object,
                _userRepositoryMock.Object,
                _mediatorMock.Object);
        }
    }
}