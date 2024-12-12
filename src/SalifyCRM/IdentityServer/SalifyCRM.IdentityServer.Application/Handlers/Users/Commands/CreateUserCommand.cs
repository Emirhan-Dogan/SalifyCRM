using Core.Utilities.Results;
using MediatR;
using SalifyCRM.IdentityServer.Application.Handlers.Groups.ValidationRules;
using SalifyCRM.IdentityServer.Application.Handlers.Users.ValidationRules;
using SalifyCRM.IdentityServer.Application.RepositoryInterfaces;
using SalifyCRM.IdentityServer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.IdentityServer.Application.Handlers.Users.Commands
{
    public class CreateUserCommand : IRequest<IResult>
    {
        public int CreatedUserId { get; set; }

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

        public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, IResult>
        {
            private readonly IUserRepository _userRepository;
            private readonly IMediator _mediator;

            public CreateUserCommandHandler(IUserRepository userRepository, IMediator mediator)
            {
                this._userRepository = userRepository;
                this._mediator = mediator;
            }

            public async Task<IResult> Handle(CreateUserCommand request, CancellationToken cancellationToken)
            {
                var validator = new CreateUserValidator();
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

                var isThereUserRecord = _userRepository.Query().Any(u => u.Email == request.Email);

                if (isThereUserRecord == true)
                    return new ErrorResult("Bu email zaten mevcut bir kullanıcı var.");

                User user = new User()
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    CitizenId = request.CitizenId,
                    PhoneNumber = request.PhoneNumber,
                    Email = request.Email,
                    Gender = request.Gender,
                    BirthDate = request.BirthDate,
                    ProfileImage = request.ProfileImage,
                    Notes = request.Notes,
                    Status = request.Status,
                    IsDeleted = false,
                    CreatedDate = DateTime.UtcNow,
                    LastLoginDate = null,
                    LastUpdatedDate = DateTime.UtcNow,
                    PasswordHash = null,
                    PasswordSalt = null,
                    CreatedUserId = request.CreatedUserId,
                    LastUpdatedUserId = request.CreatedUserId
                };

                _userRepository.Add(user);
                _userRepository.SaveChanges();

                return new SuccessResult("Kullanıcı kayıt işlemi başarılı.");
            }
        }
    }
}
