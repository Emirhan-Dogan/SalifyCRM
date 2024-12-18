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
using SalifyCRM.CustomerManagement.Application.Handlers.MartialStatuses.Commands;

namespace SalifyCRM.CustomerManagement.Tests.Application.Handlers.MartialStatuses.Commands
{
    [TestFixture]
    [Category("Command")]
    [Category("Delete")]
    [Category("MartialStatus")]
    public class DeleteMartialStatusCommandHandlerTests
    {
        private Mock<IMartialStatusRepository> _martialStatusRepositoryMock;
        private Mock<IUserRepository> _userRepositoryMock;
        private Mock<IMediator> _mediatorMock;

        private IFixture _fixture;
        private DeleteMartialStatusCommand.DeleteMartialStatusCommandHandler _deleteMartialStatusCommandHandler;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());

            _martialStatusRepositoryMock = _fixture.Freeze<Mock<IMartialStatusRepository>>();
            _userRepositoryMock = _fixture.Freeze<Mock<IUserRepository>>();
            _mediatorMock = _fixture.Freeze<Mock<IMediator>>();

            _deleteMartialStatusCommandHandler = new DeleteMartialStatusCommand
                .DeleteMartialStatusCommandHandler(
                _martialStatusRepositoryMock.Object,
                _userRepositoryMock.Object,
                _mediatorMock.Object);
        }
    }
}
