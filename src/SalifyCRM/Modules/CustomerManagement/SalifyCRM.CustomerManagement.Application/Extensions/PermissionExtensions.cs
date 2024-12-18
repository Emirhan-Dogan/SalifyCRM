using SalifyCRM.CustomerManagement.Application.Responses.Permissions;
using SalifyCRM.CustomerManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.CustomerManagement.Application.Extensions
{
    public static class PermissionExtensions
    {
        public static PermissionResponse ToPermissionResponse(this Permission permission)
        {
            return new PermissionResponse()
            {
                Id = permission.Id,
                Name = permission.Name,
                Descriptions = permission.Descriptions,
                Status = permission.Status,
                DocumentPath = permission.DocumentPath
            };
        }

        public static List<PermissionResponse> ToPermissionResponseList(this List<Permission> permissions)
        {
            return permissions.Select(
                p => new PermissionResponse()
                {
                    Id = p.Id,
                    Name = p.Name,
                    Status = p.Status,
                    DocumentPath = p.DocumentPath,
                    Descriptions = p.Descriptions
                }).ToList();
        }

        public static PermissionDetailResponse ToPermissionDetailResponse(this Permission permission)
        {
            return new PermissionDetailResponse()
            {
                Id = permission.Id,
                Name = permission.Name,
                Descriptions = permission.Descriptions,
                Status = permission.Status,
                DocumentPath = permission.DocumentPath
            };
        }
    }
}
