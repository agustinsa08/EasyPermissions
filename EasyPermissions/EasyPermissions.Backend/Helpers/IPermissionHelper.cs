using EasyPermissions.Shared.Responses;

namespace EasyPermissions.Backend.Helpers
{
    public interface IPermissionHelper
    {
        Task<ActionResponse<bool>> ProcessPermissionAsync(string email, int categoryPermissionId, string description);
    }
}