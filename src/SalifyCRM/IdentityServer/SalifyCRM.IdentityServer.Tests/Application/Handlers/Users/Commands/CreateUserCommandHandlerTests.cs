using MediatR;
using Moq;
using SalifyCRM.IdentityServer.Application.Handlers.Users.Commands;
using SalifyCRM.IdentityServer.Application.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.IdentityServer.Tests.Application.Handlers.Users.Commands
{
    [TestFixture]
    [Category("Command")]
    [Category("Create")]
    [Category("User")]
    public class CreateUserCommandHandlerTests
    {
        private Mock<IUserRepository> _userRepositoryMock;
        private Mock<IMediator> _mediatorMock;

        private CreateUserCommand.CreateUserCommandHandler _createUserCommandHandler;

        [SetUp]
        public void SetUp()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _mediatorMock = new Mock<IMediator>();

            this._createUserCommandHandler = new CreateUserCommand
                .CreateUserCommandHandler(
                _userRepositoryMock.Object,
                _mediatorMock.Object);
        }
    }
}
