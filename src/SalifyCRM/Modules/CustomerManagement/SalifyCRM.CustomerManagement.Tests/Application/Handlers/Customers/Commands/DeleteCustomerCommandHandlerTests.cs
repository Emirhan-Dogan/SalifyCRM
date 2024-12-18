using AutoFixture.AutoMoq;
using AutoFixture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalifyCRM.CustomerManagement.Application.Handlers.Customers.Commands;
using MediatR;
using Moq;
using SalifyCRM.CustomerManagement.Application.RepositoryInterfaces;

namespace SalifyCRM.CustomerManagement.Tests.Application.Handlers.Customers.Commands
{
    [TestFixture]
    [Category("Command")]
    [Category("Delete")]
    [Category("Customer")]
    public class DeleteCustomerCommandHandlerTests
    {
        private Mock<ICustomerRepository> _customerRepositoryMock;
        private Mock<IUserRepository> _userRepositoryMock;
        private Mock<IMediator> _mediatorMock;

        private DeleteCustomerCommand.DeleteCustomerCommandHandler _deleteCustomerCommandHandler;
        private IFixture _fixture;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());

            _customerRepositoryMock = _fixture.Freeze<Mock<ICustomerRepository>>();
            _userRepositoryMock = _fixture.Freeze<Mock<IUserRepository>>();
            _mediatorMock = _fixture.Freeze<Mock<IMediator>>();

            _deleteCustomerCommandHandler = new DeleteCustomerCommand
                .DeleteCustomerCommandHandler(
                _customerRepositoryMock.Object,
                _userRepositoryMock.Object,
                _mediatorMock.Object);
        }
    }
}
