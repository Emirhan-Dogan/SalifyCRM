using AutoFixture;
using AutoFixture.AutoMoq;
using FluentAssertions;
using MediatR;
using Moq;
using SalifyCRM.CustomerManagement.Application.Handlers.AddressTypes.Queries;
using SalifyCRM.CustomerManagement.Application.RepositoryInterfaces;
using SalifyCRM.CustomerManagement.Application.Responses.AddressTypes;
using SalifyCRM.CustomerManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.CustomerManagement.Tests.Application.Handlers.AddressTypes.Queries
{
    [TestFixture]
    [Category("Query")]
    [Category("AddressType")]
    public class GetAddressTypesQueryHandlerTests
    {
        private Mock<IAddressTypeRepository> _addressTypeRepositoryMock;
        private Mock<IMediator> _mediatorMock;

        private GetAddressTypesQuery.GetAddressTypesQueryHandler _getAddressTypesQueryHandler;
        private IFixture _fixture;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());

            _addressTypeRepositoryMock = _fixture.Freeze<Mock<IAddressTypeRepository>>();
            _mediatorMock = _fixture.Freeze<Mock<IMediator>>();

            _getAddressTypesQueryHandler = new GetAddressTypesQuery
                .GetAddressTypesQueryHandler(
                _addressTypeRepositoryMock.Object,
                _mediatorMock.Object);
        }

        [Test]
        public void GetAddressTypesQueryHandler_ShouldReturnSuccessWithData_WhenAddressTypesExist()
        {
            // Arrange
            int dataCount = 15;
            _fixture.Customize<AddressType>(c => c.With(x => x.IsDeleted, false));
            var addressTypes = _fixture.CreateMany<AddressType>(dataCount).ToList();

            _addressTypeRepositoryMock.Setup(repo =>
                repo.GetListAsync(It.IsAny<Expression<Func<AddressType, bool>>>()))
                .ReturnsAsync(addressTypes);

            // Action
            var result = _getAddressTypesQueryHandler.Handle(new GetAddressTypesQuery(), CancellationToken.None).Result;

            // Assertion
            result.Success.Should().BeTrue($"Expected 'True' for Success, but got '{result.Success}'.");
            result.Data.Should().NotBeNull("Expected Data to be not null, but it was null.");
            result.Data.Count.Should().Be(addressTypes.Count,
                $"Expected Data count to be '{addressTypes.Count}', but got '{result.Data.Count}'.");
        }


        [Test]
        public void GetAddressTypesQueryHandler_ShouldReturnSuccessAndEmptyData_WhenEmptyAddressTypesInRepository()
        {
            // Arrange
            _addressTypeRepositoryMock.Setup(repo =>
                repo.GetListAsync(It.IsAny<Expression<Func<AddressType, bool>>>()))
                .ReturnsAsync(new List<AddressType>());

            // Action
            var result = _getAddressTypesQueryHandler.Handle(new GetAddressTypesQuery(), CancellationToken.None).Result;

            // Assertion
            result.Success.Should().BeTrue($"Expected 'True' for Success, but got '{result.Success}'.");
            result.Data.Should().NotBeNull("Expected Data to be not null, but it was null.");
            result.Data.Count.Should().Be(0, $"Expected Data Count to be '0', but got '{result.Data.Count}'.");
        }

        [Test]
        public void GetAddressTypesQueryHandler_ShouldMapAddressTypeToAddressTypeResponse_WhenCorrectly_GivenMultipleData()
        {
            // Arrange
            int dataCount = 5;

            _fixture.Customize<AddressType>(c => c.With(x => x.IsDeleted, false));
            var addresstypes = _fixture.CreateMany<AddressType>(dataCount).ToList();

            _addressTypeRepositoryMock.Setup(repo =>
                repo.GetListAsync(It.IsAny<Expression<Func<AddressType, bool>>>()))
                .ReturnsAsync(addresstypes);

            // Action
            var result = _getAddressTypesQueryHandler.Handle(new GetAddressTypesQuery(), CancellationToken.None).Result;

            // Assertion
            for (var i = 0; i < dataCount; i++)
            {
                result.Data[i].Id.Should().Be(addresstypes[i].Id,
                    $"Expected Id to be {addresstypes[i].Id} but got {result.Data[i].Id} at index {i}");

                result.Data[i].Name.Should().Be(addresstypes[i].Name,
                    $"Expected Name to be {addresstypes[i].Name} but got {result.Data[i].Name} at index {i}");

                result.Data[i].Status.Should().Be(addresstypes[i].Status,
                    $"Expected Status to be {addresstypes[i].Status} but got {result.Data[i].Status} at index {i}");

                result.Data[i].Descriptions.Should().Be(addresstypes[i].Descriptions,
                    $"Expected Descriptions to be {addresstypes[i].Descriptions} but got {result.Data[i].Descriptions} at index {i}");
            }
        }
    }
}
