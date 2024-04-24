using EasyPermissions.Backend.Repositories.Interfaces;
using EasyPermissions.Backend.UnitsOfWork.Interfaces;
using EasyPermissions.Shared.DTOs;
using EasyPermissions.Shared.Entities;
using EasyPermissions.Shared.Responses;

namespace EasyPermissions.Backend.UnitsOfWork.Implementations
{
   public class ImageNoticesUnitOfWork : GenericUnitOfWork<ImageNotice>, IImageNoticesUnitOfWork
    {
        private readonly IImageNoticesRepository _imageNoticesRepository;

        public ImageNoticesUnitOfWork(IGenericRepository<ImageNotice> repository, IImageNoticesRepository imageNoticesRepository) : base(repository)
        {
            _imageNoticesRepository = imageNoticesRepository;
        }

        public override async Task<ActionResponse<IEnumerable<ImageNotice>>> GetAsync(PaginationDTO pagination) => await _imageNoticesRepository.GetAsync(pagination);

        public override async Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination) => await _imageNoticesRepository.GetTotalPagesAsync(pagination);

    }
}
