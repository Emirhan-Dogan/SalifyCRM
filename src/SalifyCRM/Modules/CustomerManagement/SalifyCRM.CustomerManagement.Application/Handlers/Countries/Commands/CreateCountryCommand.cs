using Core.Utilities.Messages;
using Core.Utilities.Results;
using MediatR;
using SalifyCRM.CustomerManagement.Application.Handlers.Countries.ValidationRules;
using SalifyCRM.CustomerManagement.Application.Handlers.Occupations.ValidationRules;
using SalifyCRM.CustomerManagement.Application.RepositoryInterfaces;
using SalifyCRM.CustomerManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.CustomerManagement.Application.Handlers.Countries.Commands
{
    public class CreateCountryCommand : IRequest<IResult>
    {
        public int CreatedUserId { get; set; }

        public string Name { get; set; }
        public string Status { get; set; }

        public class CreateCountryCommandHandler : IRequestHandler<CreateCountryCommand, IResult>
        {
            private readonly ICountryRepository _countryRepository;
            private readonly IUserRepository _userRepository;
            private readonly IMediator _mediator;

            public CreateCountryCommandHandler(
                ICountryRepository countryRepository,
                IUserRepository userRepository,
                IMediator mediator)
            {
                this._countryRepository = countryRepository;
                this._userRepository = userRepository;
                this._mediator = mediator;
            }

            public async Task<IResult> Handle(CreateCountryCommand request, CancellationToken cancellationToken)
            {
                var validator = new CreateCountryValidator();
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

                var isThereUserRecord = _userRepository.Query().Any(u => u.Id == request.CreatedUserId);
                if (!isThereUserRecord)
                {
                    return new ErrorResult()
                         .AddErrorDetail(new ErrorDetail
                         {
                             Code = ErrorCode.BUS4003,
                             Message = "No user was found with the provided ID.",
                             Title = "User Not Found"
                         }
                        .AddMetadata("FieldName", "CreatedUserId")
                        .AddMetadata("InputValue", request.CreatedUserId.ToString())
                        );
                }

                Country country = new Country()
                {
                    LastUpdatedDate = DateTime.UtcNow,
                    CreatedDate = DateTime.UtcNow,
                    LastUpdatedUserId = request.CreatedUserId,
                    CreatedUserId = request.CreatedUserId,
                    IsDeleted = false,
                    Name = request.Name,
                    Status = request.Status
                };

                _countryRepository.Add(country);
                return new SuccessResult("Request Successful.");
            }
        }
    }
}
