using Core.Utilities.Results;
using MediatR;
using SalifyCRM.IdentityServer.Application.Handlers.Groups.ValidationRules;
using SalifyCRM.IdentityServer.Application.Handlers.Users.ValidationRules;
using SalifyCRM.IdentityServer.Application.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.IdentityServer.Application.Handlers.Users.Commands
{
    public class UpdateUserCommand : IRequest<IResult>
    {
        public int LastUpdatedUserId { get; set; }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? CitizenId { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? ProfileImage { get; set; }
        public string? Notes { get; set; }
        public bool Status { get; set; }

        public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, IResult>
        {
            private readonly IUserRepository _userRepository;
            private readonly IMediator _mediator;

            public UpdateUserCommandHandler(IUserRepository userRepository, IMediator mediator)
            {
                this._userRepository = userRepository;
                this._mediator = mediator;
            }

            public async Task<IResult> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
            {
                var validator = new UpdateUserValidator();
                var result = validator.Validate(request);

                if (!result.IsValid)
                {
                    string errorMessage = "";
                    foreach (var error in result.Errors)
                    {
                        errorMessage += $"Hata: {error.PropertyName} - {error.ErrorMessage}";
                    }
                    return new ErrorResult(errorMessage);
                }

                var user = await _userRepository.GetAsync(O => O.Id == request.Id);
                user.FirstName = request.FirstName;
                user.LastName = request.LastName;
                user.CitizenId = request.CitizenId;
                user.PhoneNumber = request.PhoneNumber;
                user.Email = request.Email;
                user.Gender = request.Gender;
                user.BirthDate = request.BirthDate;
                user.ProfileImage = request.ProfileImage;
                user.Notes = request.Notes;
                user.Status = request.Status;

                user.LastUpdatedDate = DateTime.UtcNow;
                user.LastUpdatedUserId = request.LastUpdatedUserId;

                _userRepository.Update(user);
                _userRepository.SaveChanges();

                return new SuccessResult();
            }
        }
    }
}
