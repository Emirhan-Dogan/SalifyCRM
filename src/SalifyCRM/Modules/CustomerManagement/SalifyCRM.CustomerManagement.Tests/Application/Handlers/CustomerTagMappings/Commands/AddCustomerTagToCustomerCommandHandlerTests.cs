using AutoFixture.AutoMoq;
using AutoFixture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalifyCRM.CustomerManagement.Application.Handlers.CustomerTagMappings.Commands;
using SalifyCRM.CustomerManagement.Application.RepositoryInterfaces;
using Moq;
using MediatR;

namespace SalifyCRM.CustomerManagement.Tests.Application.Handlers.CustomerTagMappings.Commands
{
    [TestFixture]
    [Category("Command")]
    [Category("Create")]
    [Category("CustomerTagMapping")]
    public class AddCustomerTagToCustomerCommandHandlerTests
    {
        private Mock<ICustomerTagMappingRepository> _customerTagMappingRepositoryMock;
        private Mock<ICustomerTagRepository> _customerTagRepositoryMock;
        private Mock<ICustomerRepository> _customerRepositoryMock;
        private Mock<IUserRepository> _userRepositoryMock;
        private Mock<IMediator> _mediatorMock;

        private IFixture _fixture;
        private AddCustomerTagToCustomerCommand.AddCustomerTagToCustomerCommandHandler _addCustomerTagToCustomerCommandHandler;

        [SetUp]
        public void SetUp()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());

            _customerTagMappingRepositoryMock = _fixture.Freeze<Mock<ICustomerTagMappingRepository>>();
            _customerTagRepositoryMock = _fixture.Freeze<Mock<ICustomerTagRepository>>();
            _customerRepositoryMock = _fixture.Freeze<Mock<ICustomerRepository>>();
            _userRepositoryMock = _fixture.Freeze<Mock<IUserRepository>>();
            _mediatorMock = _fixture.Freeze<Mock<IMediator>>();

            _addCustomerTagToCustomerCommandHandler = new AddCustomerTagToCustomerCommand
                .AddCustomerTagToCustomerCommandHandler(
                _customerTagMappingRepositoryMock.Object,
                _customerTagRepositoryMock.Object,
                _customerRepositoryMock.Object,
                _userRepositoryMock.Object,
                _mediatorMock.Object);
        }
    }
}
