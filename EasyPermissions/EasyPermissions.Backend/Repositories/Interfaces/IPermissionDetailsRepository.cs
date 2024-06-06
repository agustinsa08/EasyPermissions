using EasyPermissions.Shared.DTOs;
using EasyPermissions.Shared.Entities;
using EasyPermissions.Shared.Responses;

namespace EasyPermissions.Backend.Repositories.Interfaces
{
   public interface IPermissionDetailsRepository
    {
        Task<ActionResponse<PermissionDetail>> GetAsync(int id);

        Task<ActionResponse<IEnumerable<PermissionDetail>>> GetAsync();

        Task<ActionResponse<IEnumerable<PermissionDetail>>> GetAsync(PaginationDTO pagination);

        Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination);
    }
}
