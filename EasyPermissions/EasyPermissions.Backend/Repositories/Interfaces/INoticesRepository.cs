using EasyPermissions.Shared.DTOs;
using EasyPermissions.Shared.Entities;
using EasyPermissions.Shared.Responses;

namespace EasyPermissions.Backend.Repositories.Interfaces
{
    public interface INoticesRepository
    {
        Task<ActionResponse<Notice>> GetAsync(int id);

        Task<ActionResponse<IEnumerable<Notice>>> GetAsync();

        Task<ActionResponse<IEnumerable<Notice>>> GetAsync(PaginationDTO pagination);

        Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination);
    }
}
