using EasyPermissions.Backend.Repositories.Interfaces;
using EasyPermissions.Backend.UnitsOfWork.Interfaces;
using EasyPermissions.Shared.DTOs;
using EasyPermissions.Shared.Entities;
using EasyPermissions.Shared.Responses;

namespace EasyPermissions.Backend.UnitsOfWork.Implementations
{
    public class TypePermissionsUnitOfWork : GenericUnitOfWork<TypePermission>, ITypePermissionsUnitOfWork
    {
        private readonly ITypePermissionsRepository _typePermissionsRepository;

        public TypePermissionsUnitOfWork(IGenericRepository<TypePermission> repository, ITypePermissionsRepository typePermissionsRepository) : base(repository)
        {
            _typePermissionsRepository = typePermissionsRepository;
        }

        public override async Task<ActionResponse<IEnumerable<TypePermission>>> GetAsync(PaginationDTO pagination) => await _typePermissionsRepository.GetAsync(pagination);

        public override async Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination) => await _typePermissionsRepository.GetTotalPagesAsync(pagination);

    }
}
