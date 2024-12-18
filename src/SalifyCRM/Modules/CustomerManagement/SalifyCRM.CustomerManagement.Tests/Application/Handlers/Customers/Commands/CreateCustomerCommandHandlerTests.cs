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
    [Category("Create")]
    [Category("Customer")]
    public class CreateCustomerCommandHandlerTests
    {
        private Mock<ICustomerRepository> _customerRepositoryMock;
        private Mock<ICustomerTypeRepository> _customerTypeRepositoryMock;
        private Mock<IOccupationRepository> _occupationRepositoryMock;
        private Mock<IMartialStatusRepository> _martialStatusRepositoryMock;
        private Mock<ICustomerCategoryRepository> _customerCategoryRepositoryMock;
        private Mock<IUserRepository> _userRepositoryMock;
        private Mock<IMediator> _mediatorMock;

        private CreateCustomerCommand.CreateCustomerCommandHandler _createCustomerCommandHandler;
        private IFixture _fixture;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());

            _customerRepositoryMock = _fixture.Freeze<Mock<ICustomerRepository>>();
            _customerTypeRepositoryMock = _fixture.Freeze<Mock<ICustomerTypeRepository>>();
            _occupationRepositoryMock = _fixture.Freeze<Mock<IOccupationRepository>>();
            _martialStatusRepositoryMock = _fixture.Freeze<Mock<IMartialStatusRepository>>();
            _customerCategoryRepositoryMock = _fixture.Freeze<Mock<ICustomerCategoryRepository>>();
            _userRepositoryMock = _fixture.Freeze<Mock<IUserRepository>>();
            _mediatorMock = _fixture.Freeze<Mock<IMediator>>();

            _createCustomerCommandHandler = new CreateCustomerCommand
                .CreateCustomerCommandHandler(
                 _customerRepositoryMock.Object,
                _customerTypeRepositoryMock.Object,
                _occupationRepositoryMock.Object,
                _martialStatusRepositoryMock.Object,
                _customerCategoryRepositoryMock.Object,
                _userRepositoryMock.Object,
                _mediatorMock.Object);
        }
    }
}
