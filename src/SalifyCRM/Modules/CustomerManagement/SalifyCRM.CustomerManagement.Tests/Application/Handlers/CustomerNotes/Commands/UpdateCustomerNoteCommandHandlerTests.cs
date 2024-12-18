using AutoFixture.AutoMoq;
using AutoFixture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalifyCRM.CustomerManagement.Application.Handlers.CustomerNotes.Commands;
using Moq;
using MediatR;
using SalifyCRM.CustomerManagement.Application.RepositoryInterfaces;

namespace SalifyCRM.CustomerManagement.Tests.Application.Handlers.CustomerNotes.Commands
{
    [TestFixture]
    [Category("Command")]
    [Category("Update")]
    [Category("CustomerNote")]
    public class UpdateCustomerNoteCommandHandlerTests
    {
        private Mock<ICustomerNoteRepository> _customerNoteRepositoryMock;
        private Mock<ICustomerRepository> _customerRepositoryMock;
        private Mock<IUserRepository> _userRepositoryMock;
        private Mock<IMediator> _mediatorMock;

        private IFixture _fixture;
        private UpdateCustomerNoteCommand.UpdateCustomerNoteCommandHandler _updateCustomerNoteCommandHandler;

        [SetUp]
        public void SetUp()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());

            _customerNoteRepositoryMock = _fixture.Freeze<Mock<ICustomerNoteRepository>>();
            _customerRepositoryMock = _fixture.Freeze<Mock<ICustomerRepository>>();
            _userRepositoryMock = _fixture.Freeze<Mock<IUserRepository>>();
            _mediatorMock = _fixture.Freeze<Mock<IMediator>>();

            _updateCustomerNoteCommandHandler = new UpdateCustomerNoteCommand
                .UpdateCustomerNoteCommandHandler(
                _customerNoteRepositoryMock.Object,
                _customerRepositoryMock.Object,
                _userRepositoryMock.Object,
                _mediatorMock.Object);
        }
    }
}
