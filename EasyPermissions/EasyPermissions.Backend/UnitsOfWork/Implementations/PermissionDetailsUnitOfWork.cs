using EasyPermissions.Backend.Repositories.Interfaces;
using EasyPermissions.Backend.UnitsOfWork.Interfaces;
using EasyPermissions.Shared.DTOs;
using EasyPermissions.Shared.Entities;
using EasyPermissions.Shared.Responses;

namespace EasyPermissions.Backend.UnitsOfWork.Implementations
{
    public class PermissionDetailsUnitOfWork : GenericUnitOfWork<PermissionDetail>, IPermissionDetailsUnitOfWork
    {
        private readonly IPermissionDetailsRepository _permissionDetailsRepository;

        public PermissionDetailsUnitOfWork(IGenericRepository<PermissionDetail> repository, IPermissionDetailsRepository permissionDetailsRepository) : base(repository)
        {
            _permissionDetailsRepository = permissionDetailsRepository;
        }

        public override async Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination) => await _permissionDetailsRepository.GetTotalPagesAsync(pagination);

        public override async Task<ActionResponse<IEnumerable<PermissionDetail>>> GetAsync(PaginationDTO pagination) => await _permissionDetailsRepository.GetAsync(pagination);

        public override async Task<ActionResponse<IEnumerable<PermissionDetail>>> GetAsync() => await _permissionDetailsRepository.GetAsync();

        public override async Task<ActionResponse<PermissionDetail>> GetAsync(int id) => await _permissionDetailsRepository.GetAsync(id);
    }
}