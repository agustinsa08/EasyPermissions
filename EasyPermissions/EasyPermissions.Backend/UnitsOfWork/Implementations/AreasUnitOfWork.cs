using EasyPermissions.Backend.Repositories.Interfaces;
using EasyPermissions.Backend.UnitsOfWork.Interfaces;
using EasyPermissions.Shared.DTOs;
using EasyPermissions.Shared.Entities;
using EasyPermissions.Shared.Responses;

namespace EasyPermissions.Backend.UnitsOfWork.Implementations
{
    public class AreasUnitOfWork : GenericUnitOfWork<Area>, IAreasUnitOfWork
    {
        private readonly IAreasRepository _areasRepository;

        public AreasUnitOfWork(IGenericRepository<Area> repository, IAreasRepository areasRepository) : base(repository)
        {
            _areasRepository = areasRepository;
        }

        public override async Task<ActionResponse<IEnumerable<Area>>> GetAsync(PaginationDTO pagination) => await _areasRepository.GetAsync(pagination);

        public override async Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination) => await _areasRepository.GetTotalPagesAsync(pagination);
    }
}
