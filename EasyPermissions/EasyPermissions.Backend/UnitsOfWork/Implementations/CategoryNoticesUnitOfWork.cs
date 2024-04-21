using EasyPermissions.Backend.Repositories.Interfaces;
using EasyPermissions.Backend.UnitsOfWork.Interfaces;
using EasyPermissions.Shared.DTOs;
using EasyPermissions.Shared.Entities;
using EasyPermissions.Shared.Responses;

namespace EasyPermissions.Backend.UnitsOfWork.Implementations
{
    public class CategoryNoticesUnitOfWork : GenericUnitOfWork<CategoryNotice>, ICategoryNoticesUnitOfWork
    {
        private readonly ICategoryNoticesRepository _categoryNoticesRepository;

        public CategoryNoticesUnitOfWork(IGenericRepository<CategoryNotice> repository, ICategoryNoticesRepository categoryNoticesRepository) : base(repository)
        {
            _categoryNoticesRepository = categoryNoticesRepository;
        }

        public override async Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination) => await _categoryNoticesRepository.GetTotalPagesAsync(pagination);

        public override async Task<ActionResponse<IEnumerable<CategoryNotice>>> GetAsync(PaginationDTO pagination) => await _categoryNoticesRepository.GetAsync(pagination);

        public override async Task<ActionResponse<IEnumerable<CategoryNotice>>> GetAsync() => await _categoryNoticesRepository.GetAsync();

        public override async Task<ActionResponse<CategoryNotice>> GetAsync(int id) => await _categoryNoticesRepository.GetAsync(id);
    }
}