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
using SalifyCRM.CustomerManagement.Application.Handlers.CustomerTypes.Commands;

namespace SalifyCRM.CustomerManagement.Tests.Application.Handlers.CustomerTypes.Commands
{
    [TestFixture]
    [Category("Command")]
    [Category("Create")]
    [Category("CustomerType")]
    public class CreateCustomerTypeCommandHandlerTests
    {
        private Mock<ICustomerTypeRepository> _customerTypeRepositoryMock;
        private Mock<IUserRepository> _userRepositoryMock;
        private Mock<IMediator> _mediatorMock;

        private IFixture _fixture;
        private CreateCustomerTypeCommand.CreateCustomerTypeCommandHandler _createCustomerTypeCommandHandler;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());

            _customerTypeRepositoryMock = _fixture.Freeze<Mock<ICustomerTypeRepository>>();
            _userRepositoryMock = _fixture.Freeze<Mock<IUserRepository>>();
            _mediatorMock = _fixture.Freeze<Mock<IMediator>>();

            _createCustomerTypeCommandHandler = new CreateCustomerTypeCommand
                .CreateCustomerTypeCommandHandler(
                _customerTypeRepositoryMock.Object,
                _userRepositoryMock.Object,
                _mediatorMock.Object);
        }
    }
}
