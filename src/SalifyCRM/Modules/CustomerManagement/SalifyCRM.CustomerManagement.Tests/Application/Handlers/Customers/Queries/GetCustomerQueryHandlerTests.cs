using AutoFixture.AutoMoq;
using AutoFixture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalifyCRM.CustomerManagement.Application.Handlers.Customers.Queries;
using MediatR;
using Moq;
using SalifyCRM.CustomerManagement.Application.RepositoryInterfaces;

namespace SalifyCRM.CustomerManagement.Tests.Application.Handlers.Customers.Queries
{
    [TestFixture]
    [Category("Query")]
    [Category("Customer")]
    public class GetCustomerQueryHandlerTests
    {
        private Mock<ICustomerRepository> _customerRepositoryMock;
        private Mock<IAddressRepository> _addressRepositoryMock;
        private Mock<ICustomerSocialRepository> _customerSocialRepositoryMock;
        private Mock<ICustomerPermissionRepository> _customerPermissionRepositoryMock;
        private Mock<ICustomerNoteRepository> _customerNoteRepositoryMock;
        private Mock<ICustomerTagRepository> _customerTagRepositoryMock;
        private Mock<IMediator> _mediatorMock;

        private GetCustomerQuery.GetCustomerQueryHandler _getCustomerQueryHandler;
        private IFixture _fixture;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());

            _customerRepositoryMock = _fixture.Freeze<Mock<ICustomerRepository>>();
            _addressRepositoryMock = _fixture.Freeze<Mock<IAddressRepository>>();
            _customerSocialRepositoryMock = _fixture.Freeze<Mock<ICustomerSocialRepository>>();
            _customerPermissionRepositoryMock = _fixture.Freeze<Mock<ICustomerPermissionRepository>>();
            _customerNoteRepositoryMock = _fixture.Freeze<Mock<ICustomerNoteRepository>>();
            _customerTagRepositoryMock = _fixture.Freeze<Mock<ICustomerTagRepository>>();
            _mediatorMock = _fixture.Freeze<Mock<IMediator>>();

            _getCustomerQueryHandler = new GetCustomerQuery
                .GetCustomerQueryHandler(
                _customerRepositoryMock.Object,
                _addressRepositoryMock.Object,
                _customerSocialRepositoryMock.Object,
                _customerPermissionRepositoryMock.Object,
                _customerNoteRepositoryMock.Object,
                _customerTagRepositoryMock.Object,
                _mediatorMock.Object);
        }
    }
}
