using EasyPermissions.Shared.DTOs;
using EasyPermissions.Shared.Entities;
using EasyPermissions.Shared.Responses;

namespace EasyPermissions.Backend.UnitsOfWork.Interfaces
{
   public interface ITypeNoticesUnitOfWork
    {
        Task<ActionResponse<TypeNotice>> GetAsync(int id);

        Task<ActionResponse<IEnumerable<TypeNotice>>> GetAsync();

        Task<ActionResponse<IEnumerable<TypeNotice>>> GetAsync(PaginationDTO pagination);

        Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination);
    }
}
