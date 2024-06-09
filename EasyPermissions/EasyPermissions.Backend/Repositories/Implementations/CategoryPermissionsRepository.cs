using EasyPermissions.Backend.Data;
using EasyPermissions.Backend.Helpers;
using EasyPermissions.Backend.Repositories.Interfaces;
using EasyPermissions.Shared.DTOs;
using EasyPermissions.Shared.Entities;
using EasyPermissions.Shared.Responses;
using Microsoft.EntityFrameworkCore;

namespace EasyPermissions.Backend.Repositories.Implementations
{
    public class CategoryPermissionsRepository : GenericRepository<CategoryPermission>, ICategoryPermissionsRepository
    {
        private readonly DataContext _context;

        public CategoryPermissionsRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<ActionResponse<CategoryPermission>> GetAsync(int id)
        {
            var categoryPermission = await _context.CategoryPermissions
                 .FirstOrDefaultAsync(s => s.Id == id);

            if (categoryPermission == null)
            {
                return new ActionResponse<CategoryPermission>
                {
                    WasSuccess = false,
                    Message = "Categoría no existe"
                };
            }

            return new ActionResponse<CategoryPermission>
            {
                WasSuccess = true,
                Result = categoryPermission
            };
        }

        public override async Task<ActionResponse<IEnumerable<CategoryPermission>>> GetAsync()
        {
            var categoryPermission = await _context.CategoryPermissions
                .OrderBy(x => x.Name)
                .ToListAsync();
            return new ActionResponse<IEnumerable<CategoryPermission>>
            {
                WasSuccess = true,
                Result = categoryPermission
            };
        }

        public override async Task<ActionResponse<IEnumerable<CategoryPermission>>> GetAsync(PaginationDTO pagination)
        {
            var queryable = _context.CategoryPermissions
                .Where(x => x.TypePermission!.Id == pagination.Id)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Name.ToLower().Contains(pagination.Filter.ToLower()));
            }

            return new ActionResponse<IEnumerable<CategoryPermission>>
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
            var queryable = _context.CategoryPermissions
                .Where(x => x.TypePermission!.Id == pagination.Id)
                .AsQueryable();

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

        public async Task<IEnumerable<CategoryPermission>> GetComboAsync(int typePermissionId)
        {
            return await _context.CategoryPermissions
                .Where(s => s.TypePermissionId == typePermissionId)
                .OrderBy(s => s.Name)
                .ToListAsync();
        }
    }
}