using EasyPermissions.Backend.Repositories.Interfaces;
using EasyPermissions.Backend.UnitsOfWork.Interfaces;
using EasyPermissions.Shared.DTOs;
using EasyPermissions.Shared.Entities;
using EasyPermissions.Shared.Responses;

namespace EasyPermissions.Backend.UnitsOfWork.Implementations
{
    public class PermissionsUnitOfWork : GenericUnitOfWork<Permission>, IPermissionsUnitOfWork
    {
        private readonly IPermissionsRepository _permissionsRepository;

        public PermissionsUnitOfWork(IGenericRepository<Permission> repository, IPermissionsRepository permissionsRepository) : base(repository)
        {
            _permissionsRepository = permissionsRepository;
        }

        public async Task<ActionResponse<int>> GetTotalPagesAsync(string email, PaginationDTO pagination) => await _permissionsRepository.GetTotalPagesAsync(email, pagination);

        public async Task<ActionResponse<IEnumerable<Permission>>> GetAsync(string email, PaginationDTO pagination) => await _permissionsRepository.GetAsync(email, pagination);

        public override async Task<ActionResponse<IEnumerable<Permission>>> GetAsync() => await _permissionsRepository.GetAsync();

        public override async Task<ActionResponse<Permission>> GetAsync(int id) => await _permissionsRepository.GetAsync(id);
    }
}