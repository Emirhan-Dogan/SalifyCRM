using AutoFixture.AutoMoq;
using AutoFixture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Moq;
using SalifyCRM.CustomerManagement.Application.Handlers.CustomerSocials.Commands;
using SalifyCRM.CustomerManagement.Application.RepositoryInterfaces;

namespace SalifyCRM.CustomerManagement.Tests.Application.Handlers.CustomerSocials.Commands
{
    [TestFixture]
    [Category("Command")]
    [Category("Delete")]
    [Category("CustomerSocial")]
    public class DeleteSocialMediaToCustomerCommandHandlerTests
    {
        private Mock<ICustomerSocialRepository> _customerSocialRepositoryMock;
        private Mock<ICustomerRepository> _customerRepositoryMock;
        private Mock<ISocialMediaRepository> _socialMediaRepositoryMock;
        private Mock<IUserRepository> _userRepositoryMock;
        private Mock<IMediator> _mediatorMock;

        private IFixture _fixture;
        private DeleteSocialMediaToCustomerCommand.DeleteSocialMediaToCustomerCommandHandler _deleteSocialMediaToCustomerCommandHandler;

        [SetUp]
        public void SetUp()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());

            _customerSocialRepositoryMock = _fixture.Freeze<Mock<ICustomerSocialRepository>>();
            _customerRepositoryMock = _fixture.Freeze<Mock<ICustomerRepository>>();
            _socialMediaRepositoryMock = _fixture.Freeze<Mock<ISocialMediaRepository>>();
            _userRepositoryMock = _fixture.Freeze<Mock<IUserRepository>>();
            _mediatorMock = _fixture.Freeze<Mock<IMediator>>();

            _deleteSocialMediaToCustomerCommandHandler = new DeleteSocialMediaToCustomerCommand
                .DeleteSocialMediaToCustomerCommandHandler(
                _customerSocialRepositoryMock.Object,
                _customerRepositoryMock.Object,
                _socialMediaRepositoryMock.Object,
                _userRepositoryMock.Object,
                _mediatorMock.Object);
        }
    }
}
