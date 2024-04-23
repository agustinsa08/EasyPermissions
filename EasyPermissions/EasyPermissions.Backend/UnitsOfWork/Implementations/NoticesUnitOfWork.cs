using EasyPermissions.Backend.Repositories.Interfaces;
using EasyPermissions.Backend.UnitsOfWork.Interfaces;
using EasyPermissions.Shared.DTOs;
using EasyPermissions.Shared.Entities;
using EasyPermissions.Shared.Responses;

namespace EasyPermissions.Backend.UnitsOfWork.Implementations
{
   public class NoticesUnitOfWork : GenericUnitOfWork<Notice>, INoticesUnitOfWork
    {
        private readonly INoticesRepository _noticesRepository;

        public NoticesUnitOfWork(IGenericRepository<Notice> repository, INoticesRepository noticesRepository) : base(repository)
        {
            _noticesRepository = noticesRepository;
        }

        public override async Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination) => await _noticesRepository.GetTotalPagesAsync(pagination);

        public override async Task<ActionResponse<IEnumerable<Notice>>> GetAsync(PaginationDTO pagination) => await _noticesRepository.GetAsync(pagination);

        public override async Task<ActionResponse<IEnumerable<Notice>>> GetAsync() => await _noticesRepository.GetAsync();

        public override async Task<ActionResponse<Notice>> GetAsync(int id) => await _noticesRepository.GetAsync(id);
    }
}