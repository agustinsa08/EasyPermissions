using EasyPermissions.Backend.Repositories.Implementations;
using EasyPermissions.Backend.Repositories.Interfaces;
using EasyPermissions.Backend.UnitsOfWork.Interfaces;
using EasyPermissions.Shared.DTOs;
using EasyPermissions.Shared.Entities;
using EasyPermissions.Shared.Responses;

namespace EasyPermissions.Backend.UnitsOfWork.Implementations
{
  public class TypeNoticesUnitOfWork : GenericUnitOfWork<TypeNotice>, ITypeNoticesUnitOfWork
    {
        private readonly ITypeNoticesRepository _typeNoticesRepository;

        public TypeNoticesUnitOfWork(IGenericRepository<TypeNotice> repository, ITypeNoticesRepository typeNoticesRepository) : base(repository)
        {
            _typeNoticesRepository = typeNoticesRepository;
        }

        public override async Task<ActionResponse<IEnumerable<TypeNotice>>> GetAsync() => await _typeNoticesRepository.GetAsync();

        public override async Task<ActionResponse<TypeNotice>> GetAsync(int id) => await _typeNoticesRepository.GetAsync(id);

        public override async Task<ActionResponse<IEnumerable<TypeNotice>>> GetAsync(PaginationDTO pagination) => await _typeNoticesRepository.GetAsync(pagination);

        public override async Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination) => await _typeNoticesRepository.GetTotalPagesAsync(pagination);

    }
}
