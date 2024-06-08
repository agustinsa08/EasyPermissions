using EasyPermissions.Shared.DTOs;
using EasyPermissions.Shared.Entities;
using EasyPermissions.Shared.Responses;

namespace EasyPermissions.Backend.Repositories.Interfaces
{
   public interface IPermissionsRepository
    {
        Task<ActionResponse<Permission>> GetAsync(int id);

        Task<ActionResponse<IEnumerable<Permission>>> GetAsync();

        Task<ActionResponse<IEnumerable<Permission>>> GetAsync(string email, PaginationDTO pagination);

        Task<ActionResponse<int>> GetTotalPagesAsync(string email, PaginationDTO pagination);
    }
}
