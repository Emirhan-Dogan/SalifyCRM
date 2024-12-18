using Core.Utilities.Messages;
using Core.Utilities.Results;
using MediatR;
using SalifyCRM.CustomerManagement.Application.Extensions;
using SalifyCRM.CustomerManagement.Application.RepositoryInterfaces;
using SalifyCRM.CustomerManagement.Application.Responses.Districts;
using SalifyCRM.CustomerManagement.Application.Responses.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.CustomerManagement.Application.Handlers.Permissions.Queries
{
    public class GetPermissionQuery : IRequest<IDataResult<PermissionDetailResponse>>
    {
        public int Id { get; set; }

        public class GetPermissionQueryHandler : IRequestHandler<GetPermissionQuery, IDataResult<PermissionDetailResponse>>
        {
            private readonly IPermissionRepository _permissionRepository;
            private readonly IMediator _mediator;

            public GetPermissionQueryHandler(IPermissionRepository permissionRepository, IMediator mediator)
            {
                this._permissionRepository = permissionRepository;
                this._mediator = mediator;
            }

            public async Task<IDataResult<PermissionDetailResponse>> Handle(GetPermissionQuery request, CancellationToken cancellationToken)
            {
                if (request.Id == null)
                {
                    return new ErrorDataResult<PermissionDetailResponse>()
                        .AddErrorDetail(new ErrorDetail
                        {
                            Code = ErrorCode.VAL1001,
                            Message = "Required field is missing.",
                            Title = "Missing Field Error"
                        }
                        .AddMetadata("FieldName", "Id")
                        .AddMetadata("InputValue", "Null")
                        );
                }
                if (request.Id < 0)
                {
                    return new ErrorDataResult<PermissionDetailResponse>()
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

                var permission = await _permissionRepository.GetAsync(o => o.Id == request.Id && o.IsDeleted == false);
                if (permission == null)
                {
                    return new ErrorDataResult<PermissionDetailResponse>()
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
                PermissionDetailResponse result = PermissionExtensions.ToPermissionDetailResponse(permission);

                return new SuccessDataResult<PermissionDetailResponse>(result, "Request successful");

            }
        }
    }
}
