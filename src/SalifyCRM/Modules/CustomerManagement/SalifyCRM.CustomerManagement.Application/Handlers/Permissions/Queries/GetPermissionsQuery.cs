using Core.Utilities.Results;
using MediatR;
using SalifyCRM.CustomerManagement.Application.Extensions;
using SalifyCRM.CustomerManagement.Application.RepositoryInterfaces;
using SalifyCRM.CustomerManagement.Application.Responses.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.CustomerManagement.Application.Handlers.Permissions.Queries
{
    public class GetPermissionsQuery : IRequest<IDataResult<List<PermissionResponse>>>
    {
        public class GetPermissionsQueryHandler : IRequestHandler<GetPermissionsQuery, IDataResult<List<PermissionResponse>>>
        {
            private readonly IPermissionRepository _permissionRepository;
            private readonly IMediator _mediator;

            public GetPermissionsQueryHandler(IPermissionRepository permissionRepository, IMediator mediator)
            {
                this._permissionRepository = permissionRepository;
                this._mediator = mediator;
            }

            public async Task<IDataResult<List<PermissionResponse>>> Handle(GetPermissionsQuery request, CancellationToken cancellationToken)
            {
                var permissions = await _permissionRepository.GetListAsync(O => O.IsDeleted == false);

                List<PermissionResponse> result = PermissionExtensions.ToPermissionResponseList(permissions.ToList());

                return new SuccessDataResult<List<PermissionResponse>>(result);
            }
        }
    }
}
