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
    [Category("Update")]
    [Category("User")]
    public class UpdateUserCommandHandlerTests
    {
        private Mock<IUserRepository> _userRepositoryMock;
        private Mock<IMediator> _mediatorMock;

        private UpdateUserCommand.UpdateUserCommandHandler _updateUserCommandHandler;

        [SetUp]
        public void SetUp()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _mediatorMock = new Mock<IMediator>();

            this._updateUserCommandHandler = new UpdateUserCommand
                .UpdateUserCommandHandler(
                _userRepositoryMock.Object,
                _mediatorMock.Object);
        }
    }
}
