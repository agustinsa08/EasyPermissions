using EasyPermissions.Shared.DTOs;
using EasyPermissions.Shared.Entities;
using EasyPermissions.Shared.Responses;

namespace EasyPermissions.Backend.Repositories.Interfaces
{
    public interface ICategoryNoticesRepository
    {
        Task<ActionResponse<CategoryNotice>> GetAsync(int id);

        Task<ActionResponse<IEnumerable<CategoryNotice>>> GetAsync();

        Task<ActionResponse<IEnumerable<CategoryNotice>>> GetAsync(PaginationDTO pagination);

        Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination);
    }
}
