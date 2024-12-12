using MediatR;
using Moq;
using SalifyCRM.IdentityServer.Application.Handlers.UserGroups.Queries;
using SalifyCRM.IdentityServer.Application.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.IdentityServer.Tests.Application.Handlers.UserGroups.Queries
{
    [TestFixture]
    [Category("Query")]
    [Category("UserGroup")]
    public class GetUserGroupQueryHandlerTests
    {
        private Mock<IUserGroupRepository> _userGroupRepositoryMock;
        private Mock<IUserRepository> _userRepositoryMock;
        private Mock<IGroupRepository> _groupRepositoryMock;
        private Mock<IMediator> _mediatorMock;

        private GetUserGroupQuery.GetUserGroupQueryHandler _getUserGroupQueryHandler;

        [SetUp]
        public void SetUp()
        {
            _userGroupRepositoryMock = new Mock<IUserGroupRepository>();
            _userRepositoryMock = new Mock<IUserRepository>();
            _groupRepositoryMock = new Mock<IGroupRepository>();
            _mediatorMock = new Mock<IMediator>();

            this._getUserGroupQueryHandler = new GetUserGroupQuery
                .GetUserGroupQueryHandler(
                _userGroupRepositoryMock.Object,
                _userRepositoryMock.Object,
                _groupRepositoryMock.Object,
                _mediatorMock.Object);
        }
    }
}
