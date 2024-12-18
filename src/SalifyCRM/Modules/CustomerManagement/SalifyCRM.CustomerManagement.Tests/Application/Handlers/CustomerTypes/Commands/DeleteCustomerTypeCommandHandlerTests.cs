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
    [Category("Delete")]
    [Category("CustomerType")]
    public class DeleteCustomerTypeCommandHandlerTests
    {
        private Mock<ICustomerTypeRepository> _customerTypeRepositoryMock;
        private Mock<IUserRepository> _userRepositoryMock;
        private Mock<IMediator> _mediatorMock;

        private IFixture _fixture;
        private DeleteCustomerTypeCommand.DeleteCustomerTypeCommandHandler _deleteCustomerTypeCommandHandler;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());

            _customerTypeRepositoryMock = _fixture.Freeze<Mock<ICustomerTypeRepository>>();
            _userRepositoryMock = _fixture.Freeze<Mock<IUserRepository>>();
            _mediatorMock = _fixture.Freeze<Mock<IMediator>>();

            _deleteCustomerTypeCommandHandler = new DeleteCustomerTypeCommand
                .DeleteCustomerTypeCommandHandler(
                _customerTypeRepositoryMock.Object,
                _userRepositoryMock.Object,
                _mediatorMock.Object);
        }
    }
}
