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
    [Category("Update")]
    [Category("Interaction")]
    public class UpdateInteractionCommandHandlerTests
    {
        private Mock<IInteractionTypeRepository> _interactionTypeRepositoryMock;
        private Mock<IInteractionRepository> _interactionRepositoryMock;
        private Mock<IOrderRepository> _orderRepositoryMock;
        private Mock<IUserRepository> _userRepositoryMock;
        private Mock<IMediator> _mediatorMock;

        private IFixture _fixture;
        private UpdateInteractionCommand.UpdateInteractionCommandHandler _updateInteractionCommandHandler;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());

            _interactionTypeRepositoryMock = _fixture.Freeze<Mock<IInteractionTypeRepository>>();
            _interactionRepositoryMock = _fixture.Freeze<Mock<IInteractionRepository>>();
            _orderRepositoryMock = _fixture.Freeze<Mock<IOrderRepository>>();
            _userRepositoryMock = _fixture.Freeze<Mock<IUserRepository>>();
            _mediatorMock = _fixture.Freeze<Mock<IMediator>>();

            _updateInteractionCommandHandler = new UpdateInteractionCommand
                .UpdateInteractionCommandHandler(
                _interactionRepositoryMock.Object,
                _interactionTypeRepositoryMock.Object,
                _orderRepositoryMock.Object,
                _userRepositoryMock.Object,
                _mediatorMock.Object);
        }
    }
}
