using EasyPermissions.Shared.DTOs;
using EasyPermissions.Shared.Entities;
using EasyPermissions.Shared.Responses;

namespace EasyPermissions.Backend.UnitsOfWork.Interfaces
{
    public interface IPermissionDetailsUnitOfWork
    {
        Task<ActionResponse<PermissionDetail>> GetAsync(int id);

        Task<ActionResponse<IEnumerable<PermissionDetail>>> GetAsync();

        Task<ActionResponse<IEnumerable<PermissionDetail>>> GetAsync(PaginationDTO pagination);

        Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination);

        Task<IEnumerable<PermissionDetail>> GetComboAsync(int permissionId);

    }
}