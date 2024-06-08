using EasyPermissions.Backend.UnitsOfWork.Implementations;
using EasyPermissions.Backend.UnitsOfWork.Interfaces;
using EasyPermissions.Shared.Entities;
using EasyPermissions.Shared.Enums;
using EasyPermissions.Shared.Responses;

namespace EasyPermissions.Backend.Helpers
{
    public class PermissionHelper: IPermissionHelper
    {
        private readonly IUsersUnitOfWork _usersUnitOfWork;
        private readonly IPermissionsUnitOfWork _permissionsUnitOfWork;

        public PermissionHelper(IUsersUnitOfWork usersUnitOfWork, IPermissionsUnitOfWork permissionsUnitOfWork)
        {
            _usersUnitOfWork = usersUnitOfWork;
            _permissionsUnitOfWork = permissionsUnitOfWork;
        }

        public async Task<ActionResponse<bool>> ProcessPermissionAsync(string email, int categoryPermissionId, string description)
        {
            var user = await _usersUnitOfWork.GetUserAsync(email);
            if (user == null)
            {
                return new ActionResponse<bool>
                {
                    WasSuccess = false,
                    Message = "Usuario no válido"
                };
            }

            var permission = new Permission
            {
                Date = DateTime.UtcNow,
                User = user,
                CategoryPermissionId = categoryPermissionId,
                Description = description,
                Status = PermissionStatus.Pending,
                DateStatus = DateTime.UtcNow,
                PermissionDetails = new List<PermissionDetail>()
            };
            permission.PermissionDetails.Add(new PermissionDetail
            {
                Date = DateTime.UtcNow,
                Status = PermissionStatus.Pending
            });

            var response = await _permissionsUnitOfWork.AddAsync(permission);
            return new ActionResponse<bool>
            {
                WasSuccess = response.WasSuccess,
                Message = response.Message
            };
        }
    }
}

