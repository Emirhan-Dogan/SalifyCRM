using MediatR;
using Moq;
using SalifyCRM.IdentityServer.Application.Handlers.Groups.Commands;
using SalifyCRM.IdentityServer.Application.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.IdentityServer.Tests.Application.Handlers.Groups.Commands
{
    [TestFixture]
    [Category("Command")]
    [Category("Delete")]
    [Category("Group")]
    public class DeleteGroupCommandHandlerTests
    {
        private Mock<IGroupRepository> _groupRepositoryMock;
        private Mock<IMediator> _mediatorMock;

        private DeleteGroupCommand.DeleteGroupCommandHandler _deleteGroupCommandHandler;

        [SetUp]
        public void Setup()
        {
            _groupRepositoryMock = new Mock<IGroupRepository>();
            _mediatorMock = new Mock<IMediator>();

            this._deleteGroupCommandHandler = new DeleteGroupCommand
                .DeleteGroupCommandHandler(
                _groupRepositoryMock.Object,
                _mediatorMock.Object);
        }
    }
}
