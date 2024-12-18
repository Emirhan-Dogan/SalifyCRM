using AutoFixture.AutoMoq;
using AutoFixture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Moq;
using SalifyCRM.CustomerManagement.Application.Handlers.CustomerNotes.Queries;
using SalifyCRM.CustomerManagement.Application.RepositoryInterfaces;

namespace SalifyCRM.CustomerManagement.Tests.Application.Handlers.CustomerNotes.Queries
{
    [TestFixture]
    [Category("Query")]
    [Category("CustomerNote")]
    public class GetCustomerNotesQueryHandlerTests
    {
        private Mock<ICustomerNoteRepository> _customerNoteRepositoryMock;
        private Mock<IMediator> _mediatorMock;

        private IFixture _fixture;
        private GetCustomerNotesQuery.GetCustomerNotesQueryHandler _getCustomerNotesQueryHandler;

        [SetUp]
        public void SetUp()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());

            _customerNoteRepositoryMock = _fixture.Freeze<Mock<ICustomerNoteRepository>>();
            _mediatorMock = _fixture.Freeze<Mock<IMediator>>();

            _getCustomerNotesQueryHandler = new GetCustomerNotesQuery
                .GetCustomerNotesQueryHandler(
                _customerNoteRepositoryMock.Object,
                _mediatorMock.Object);
        }
    }
}
