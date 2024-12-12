using MediatR;
using Moq;
using SalifyCRM.IdentityServer.Application.Handlers.Users.Queries;
using SalifyCRM.IdentityServer.Application.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.IdentityServer.Tests.Application.Handlers.Users.Queries
{
    [TestFixture]
    [Category("Query")]
    [Category("User")]
    public class GetUserQueryHandlerTests
    {
        private Mock<IUserRepository> _userRepositoryMock;
        private Mock<IMediator> _mediatorMock;

        private GetUserQuery.GetUserQueryHandler _getUserQueryHandler;

        [SetUp]
        public void SetUp()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _mediatorMock = new Mock<IMediator>();

            this._getUserQueryHandler = new GetUserQuery
                .GetUserQueryHandler(
                _userRepositoryMock.Object,
                _mediatorMock.Object);
        }
    }
}
