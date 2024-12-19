using AutoFixture.AutoMoq;
using AutoFixture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalifyCRM.InteractionManagement.Application.Handlers.Interactions.Commands;
using MediatR;
using Moq;
using SalifyCRM.InteractionManagement.Application.RepositoryInterfaces;

namespace SalifyCRM.InteractionManagement.Tests.Application.Handlers.Interactions.Commands
{
    [TestFixture]
    [Category("Command")]
    [Category("Create")]
    [Category("Interaction")]
    public class CreateInteractionCommandHandlerTests
    {
        private Mock<IInteractionRepository> _interactionRepositoryMock;
        private Mock<IInteractionTypeRepository> _interactionTypeRepositoryMock;
        private Mock<IOrderRepository> _orderRepositoryMock;
        private Mock<ICustomerRepository> _customerRepositoryMock;
        private Mock<IUserRepository> _userRepositoryMock;
        private Mock<IMediator> _mediatorMock;

        private IFixture _fixture;
        CreateInteractionCommand.CreateInteractionCommandHandler _createInteractionCommandHandler;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());

            _interactionRepositoryMock = _fixture.Freeze<Mock<IInteractionRepository>>();
            _interactionTypeRepositoryMock = _fixture.Freeze<Mock<IInteractionTypeRepository>>();
            _orderRepositoryMock = _fixture.Freeze<Mock<IOrderRepository>>();
            _customerRepositoryMock = _fixture.Freeze<Mock<ICustomerRepository>>();
            _userRepositoryMock = _fixture.Freeze<Mock<IUserRepository>>();
            _mediatorMock = _fixture.Freeze<Mock<IMediator>>();

            _createInteractionCommandHandler = new CreateInteractionCommand
                    .CreateInteractionCommandHandler(
                    _interactionRepositoryMock.Object,
                    _interactionTypeRepositoryMock.Object,
                    _orderRepositoryMock.Object,
                    _customerRepositoryMock.Object,
                    _userRepositoryMock.Object,
                    _mediatorMock.Object);
        }
    }
}
