using EasyPermissions.Shared.DTOs;
using EasyPermissions.Shared.Entities;
using EasyPermissions.Shared.Responses;

namespace EasyPermissions.Backend.Repositories.Interfaces
{
   public interface IImageNoticesRepository
    {
        Task<ActionResponse<IEnumerable<ImageNotice>>> GetAsync(PaginationDTO pagination);

        Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination);

    }
}