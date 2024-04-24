using EasyPermissions.Backend.Data;
using EasyPermissions.Backend.Helpers;
using EasyPermissions.Backend.Repositories.Interfaces;
using EasyPermissions.Shared.DTOs;
using EasyPermissions.Shared.Entities;
using EasyPermissions.Shared.Responses;
using Microsoft.EntityFrameworkCore;

namespace EasyPermissions.Backend.Repositories.Implementations
{
    public class CategoryNoticesRepository : GenericRepository<CategoryNotice>, ICategoryNoticesRepository
    {
        private readonly DataContext _context;

        public CategoryNoticesRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<ActionResponse<IEnumerable<CategoryNotice>>> GetAsync()
        {
            var categoryNotices = await _context.CategoryNotices
                .OrderBy(x => x.Name)
                .Include(s => s.TypeNotices)
                .ToListAsync();
            return new ActionResponse<IEnumerable<CategoryNotice>>
            {
                WasSuccess = true,
                Result = categoryNotices
            };
        }

        public override async Task<ActionResponse<IEnumerable<CategoryNotice>>> GetAsync(PaginationDTO pagination)
        {
            var queryable = _context.CategoryNotices
                .Include(c => c.TypeNotices)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Name.ToLower().Contains(pagination.Filter.ToLower()));
            }

            return new ActionResponse<IEnumerable<CategoryNotice>>
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
            var queryable = _context.CategoryNotices.AsQueryable();

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

        public override async Task<ActionResponse<CategoryNotice>> GetAsync(int id)
        {
            var categoryNotice = await _context.CategoryNotices
                 .Include(c => c.TypeNotices!)
                 .FirstOrDefaultAsync(c => c.Id == id);

            if (categoryNotice == null)
            {
                return new ActionResponse<CategoryNotice>
                {
                    WasSuccess = false,
                    Message = "Categoría no existe"
                };
            }

            return new ActionResponse<CategoryNotice>
            {
                WasSuccess = true,
                Result = categoryNotice
            };
        }
    }
}