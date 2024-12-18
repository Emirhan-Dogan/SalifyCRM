using Core.Utilities.Messages;
using Core.Utilities.Results;
using MediatR;
using SalifyCRM.CustomerManagement.Application.RepositoryInterfaces;
using SalifyCRM.CustomerManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.CustomerManagement.Application.Handlers.Cities.Commands
{
    public class DeleteCityCommand : IRequest<IResult>
    {
        public int Id { get; set; }
        public int DeletedUserId { get; set; }
        public class DeleteCityCommandHandler : IRequestHandler<DeleteCityCommand, IResult>
        {
            private readonly ICityRepository _cityRepository;
            private readonly IDistrictRepository _districtRepository;
            private readonly IUserRepository _userRepository;
            private readonly IMediator _mediator;

            public DeleteCityCommandHandler(
                ICityRepository cityRepository,
                IDistrictRepository districtRepository,
                IUserRepository userRepository,
                IMediator mediator)
            {
                this._cityRepository = cityRepository;
                this._districtRepository = districtRepository;
                this._userRepository = userRepository;
                this._mediator = mediator;
            }

            public async Task<IResult> Handle(DeleteCityCommand request, CancellationToken cancellationToken)
            {
                if (request.Id < 0)
                {
                    return new ErrorResult()
                         .AddErrorDetail(new ErrorDetail
                         {
                             Code = ErrorCode.VAL1002,
                             Message = "The provided value should not be negative.",
                             Title = "Negative Value Error"
                         }
                        .AddMetadata("FieldName", "Id")
                        .AddMetadata("InputValue", request.Id.ToString())
                        );
                }
                if (request.DeletedUserId < 0)
                {
                    return new ErrorResult()
                         .AddErrorDetail(new ErrorDetail
                         {
                             Code = ErrorCode.VAL1002,
                             Message = "The provided value should not be negative.",
                             Title = "Negative Value Error"
                         }
                        .AddMetadata("FieldName", "DeletedUserId")
                        .AddMetadata("InputValue", request.DeletedUserId.ToString())
                        );
                }

                var isThereUserRecord = _userRepository.Query().Any(u => u.Id == request.DeletedUserId);
                if (!isThereUserRecord)
                {
                    return new ErrorResult()
                         .AddErrorDetail(new ErrorDetail
                         {
                             Code = ErrorCode.BUS4003,
                             Message = "No user was found with the provided ID.",
                             Title = "User Not Found"
                         }
                        .AddMetadata("FieldName", "DeletedUserId")
                        .AddMetadata("InputValue", request.DeletedUserId.ToString())
                        );
                }

                var city = await _cityRepository.GetAsync(O => O.Id == request.Id && O.IsDeleted == false);
                if (city == null)
                {
                    return new ErrorResult()
                        .AddErrorDetail(new ErrorDetail
                        {
                            Code = ErrorCode.BUS4002,
                            Message = "The requested data could not be found.",
                            Title = "Data Not Found Error"
                        }
                        .AddMetadata("FieldName", "Id")
                        .AddMetadata("InputValue", request.Id.ToString())
                        .AddMetadata("Suggestion", "A deleted or non-existing record was requested.")
                        );
                }
                DateTime lastUpdatedDate = DateTime.UtcNow;

                var districts = await _districtRepository.GetListAsync(O => O.CityId == request.Id && O.IsDeleted == false);

                foreach (var district in districts)
                {
                    district.LastUpdatedDate = lastUpdatedDate;
                    district.LastUpdatedUserId = request.DeletedUserId;
                    district.IsDeleted = true;

                    _districtRepository.Update(district);
                }

                city.LastUpdatedDate = lastUpdatedDate;
                city.LastUpdatedUserId = request.DeletedUserId;
                city.IsDeleted = true;

                _cityRepository.Update(city);

                return new SuccessResult("Request Successful.");
            }
        }
    }
}
