using AutoFixture.AutoMoq;
using AutoFixture;
using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalifyCRM.CustomerManagement.Application.RepositoryInterfaces;
using SalifyCRM.CustomerManagement.Application.Handlers.CustomerTags.Commands;

namespace SalifyCRM.CustomerManagement.Tests.Application.Handlers.CustomerTags.Commands
{
    [TestFixture]
    [Category("Command")]
    [Category("Update")]
    [Category("CustomerTag")]
    public class UpdateCustomerTagCommandHandlerTests
    {
        private Mock<ICustomerTagRepository> _customerTagRepositoryMock;
        private Mock<IUserRepository> _userRepositoryMock;
        private Mock<IMediator> _mediatorMock;

        private IFixture _fixture;
        private UpdateCustomerTagCommand.UpdateCustomerTagCommandHandler _updateCustomerTagCommandHandler;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());

            _customerTagRepositoryMock = _fixture.Freeze<Mock<ICustomerTagRepository>>();
            _userRepositoryMock = _fixture.Freeze<Mock<IUserRepository>>();
            _mediatorMock = _fixture.Freeze<Mock<IMediator>>();

            _updateCustomerTagCommandHandler = new UpdateCustomerTagCommand
                .UpdateCustomerTagCommandHandler(
                _customerTagRepositoryMock.Object,
                _userRepositoryMock.Object,
                _mediatorMock.Object);
        }
    }
}
