using Core.Utilities.Messages;
using Core.Utilities.Results;
using MediatR;
using SalifyCRM.CustomerManagement.Application.Handlers.Addresses.ValidationRules;
using SalifyCRM.CustomerManagement.Application.Handlers.Occupations.ValidationRules;
using SalifyCRM.CustomerManagement.Application.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.CustomerManagement.Application.Handlers.Addresses.Commands
{
    public class UpdateAddressCommand : IRequest<IResult>
    {
        public int Id { get; set; }
        public int LastUpdatedUserId { get; set; }
        public int AddressTypeId { get; set; }
        public int CountryId { get; set; }
        public int CityId { get; set; }
        public int DistrictId { get; set; }

        public string Name { get; set; }
        public string Details { get; set; }
        public string Status { get; set; }

        public class UpdateAddressCommandHandler : IRequestHandler<UpdateAddressCommand, IResult>
        {
            private readonly IAddressRepository _addressRepository;
            private readonly IAddressTypeRepository _addressTypeRepository;
            private readonly ICountryRepository _countryRepository;
            private readonly ICityRepository _cityRepository;
            private readonly IDistrictRepository _districtRepository;
            private readonly IUserRepository _userRepository;
            private readonly IMediator _mediator;

            public UpdateAddressCommandHandler(
                IAddressRepository addressRepository,
                IAddressTypeRepository addressTypeRepository,
                ICountryRepository countryRepository,
                ICityRepository cityRepository,
                IDistrictRepository districtRepository,
                IUserRepository userRepository,
                IMediator mediator)
            {
                this._addressRepository = addressRepository;
                this._addressTypeRepository = addressTypeRepository;
                this._countryRepository = countryRepository;
                this._cityRepository = cityRepository;
                this._districtRepository = districtRepository;
                this._userRepository = userRepository;
                this._mediator = mediator;
            }

            public async Task<IResult> Handle(UpdateAddressCommand request, CancellationToken cancellationToken)
            {
                var validator = new UpdateAddressValidator();
                var validationResult = await validator.ValidateAsync(request);

                if (!validationResult.IsValid)
                {
                    var errorResult = new ErrorResult();

                    foreach (var failure in validationResult.Errors)
                    {
                        errorResult.AddErrorDetail(new ErrorDetail
                        {
                            Code = ErrorCode.VAL1004,
                            Message = failure.ErrorMessage,
                            Title = "Validation Error"
                        }
                        .AddMetadata("FieldName", failure.PropertyName)
                        .AddMetadata("InputValue", failure.AttemptedValue?.ToString()));
                    }

                    return errorResult;
                }

                var isThereUserRecord = _userRepository.Query().Any(u => u.Id == request.LastUpdatedUserId);
                if (!isThereUserRecord)
                {
                    return new ErrorResult()
                         .AddErrorDetail(new ErrorDetail
                         {
                             Code = ErrorCode.BUS4003,
                             Message = "No user was found with the provided ID.",
                             Title = "User Not Found"
                         }
                        .AddMetadata("FieldName", "LastUpdatedUserId")
                        .AddMetadata("InputValue", request.LastUpdatedUserId.ToString())
                        );
                }

                var isThereAddressTypeRecord = _addressTypeRepository.Query().Any(u => u.Id == request.AddressTypeId);
                if (!isThereAddressTypeRecord)
                {
                    return new ErrorResult()
                         .AddErrorDetail(new ErrorDetail
                         {
                             Code = ErrorCode.BUS4003,
                             Message = "No address type was found with the provided AddressTypeId.",
                             Title = "AddressType Not Found"
                         }
                        .AddMetadata("FieldName", "AddressTypeId")
                        .AddMetadata("InputValue", request.AddressTypeId.ToString())
                        );
                }

                var isThereCountryRecord = _countryRepository.Query().Any(u => u.Id == request.CountryId);
                if (!isThereCountryRecord)
                {
                    return new ErrorResult()
                         .AddErrorDetail(new ErrorDetail
                         {
                             Code = ErrorCode.BUS4003,
                             Message = "No country was found with the provided CountryId.",
                             Title = "Country Not Found"
                         }
                        .AddMetadata("FieldName", "CountryId")
                        .AddMetadata("InputValue", request.CountryId.ToString())
                        );
                }

                var isThereCityRecord = _cityRepository.Query().Any(u => u.Id == request.CityId);
                if (!isThereCityRecord)
                {
                    return new ErrorResult()
                         .AddErrorDetail(new ErrorDetail
                         {
                             Code = ErrorCode.BUS4003,
                             Message = "No city was found with the provided CityId.",
                             Title = "City Not Found"
                         }
                        .AddMetadata("FieldName", "CityId")
                        .AddMetadata("InputValue", request.CityId.ToString())
                        );
                }

                var isThereDistrictRecord = _districtRepository.Query().Any(u => u.Id == request.DistrictId);
                if (!isThereDistrictRecord)
                {
                    return new ErrorResult()
                         .AddErrorDetail(new ErrorDetail
                         {
                             Code = ErrorCode.BUS4003,
                             Message = "No district was found with the provided DistrictId.",
                             Title = "District Not Found"
                         }
                        .AddMetadata("FieldName", "DistrictId")
                        .AddMetadata("InputValue", request.DistrictId.ToString())
                        );
                }

                var address = await _addressRepository.GetAsync(O => O.Id == request.Id && O.IsDeleted == false);
                if (address == null)
                {
                    return new ErrorResult()
                         .AddErrorDetail(new ErrorDetail
                         {
                             Code = ErrorCode.BUS4003,
                             Message = "No address was found with the provided Id.",
                             Title = "Address Not Found"
                         }
                        .AddMetadata("FieldName", "Id")
                        .AddMetadata("InputValue", request.Id.ToString())
                        );
                }

                address.AddressTypeId = request.AddressTypeId;
                address.CountryId = request.CountryId;
                address.CityId = request.CityId;
                address.DistrictId = request.DistrictId;
                address.Name = request.Name;
                address.Details = request.Details;
                address.Status = request.Status;

                address.LastUpdatedDate = DateTime.UtcNow;
                address.LastUpdatedUserId = request.LastUpdatedUserId;

                _addressRepository.Update(address);
                return new SuccessResult("Request Successful.");
            }
        }
    }
}
