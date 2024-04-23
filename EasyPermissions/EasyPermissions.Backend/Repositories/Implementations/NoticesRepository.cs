using EasyPermissions.Backend.Data;
using EasyPermissions.Backend.Helpers;
using EasyPermissions.Backend.Repositories.Interfaces;
using EasyPermissions.Shared.DTOs;
using EasyPermissions.Shared.Entities;
using EasyPermissions.Shared.Responses;
using Microsoft.EntityFrameworkCore;

namespace EasyPermissions.Backend.Repositories.Implementations
{
    public class NoticesRepository : GenericRepository<Notice>, INoticesRepository
    {
        private readonly DataContext _context;

        public NoticesRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<ActionResponse<IEnumerable<Notice>>> GetAsync()
        {
            var notices = await _context.Notices
                .OrderBy(x => x.Name)
                .ToListAsync();
            return new ActionResponse<IEnumerable<Notice>>
            {
                WasSuccess = true,
                Result = notices
            };
        }

        public override async Task<ActionResponse<IEnumerable<Notice>>> GetAsync(PaginationDTO pagination)
        {
            var queryable = _context.Notices
                .Include(c => c.ImageNotices)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Name.ToLower().Contains(pagination.Filter.ToLower()));
            }

            return new ActionResponse<IEnumerable<Notice>>
            {
                WasSuccess = true,
                Result = await queryable
                    .OrderBy(x => x.Name)
                    .Paginate(pagination)
                    .ToListAsync()
            };
        }

        public override async Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination)
        {
            var queryable = _context.Notices.AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Name.ToLower().Contains(pagination.Filter.ToLower()));
            }

            double count = await queryable.CountAsync();
            int totalPages = (int)Math.Ceiling(count / pagination.RecordsNumber);
            return new ActionResponse<int>
            {
                WasSuccess = true,
                Result = totalPages
            };
        }

        public override async Task<ActionResponse<Notice>> GetAsync(int id)
        {
            var notice = await _context.Notices
                 .Include(c => c.ImageNotices!)
                 .FirstOrDefaultAsync(c => c.Id == id);

            if (notice == null)
            {
                return new ActionResponse<Notice>
                {
                    WasSuccess = false,
                    Message = "Noticia no existe"
                };
            }

            return new ActionResponse<Notice>
            {
                WasSuccess = true,
                Result = notice
            };
        }
    }
}