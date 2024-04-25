using EasyPermissions.Backend.Data;
using EasyPermissions.Backend.Helpers;
using EasyPermissions.Backend.Repositories.Interfaces;
using EasyPermissions.Shared.DTOs;
using EasyPermissions.Shared.Entities;
using EasyPermissions.Shared.Responses;
using Microsoft.EntityFrameworkCore;

namespace EasyPermissions.Backend.Repositories.Implementations
{
  public class TypePermissionsRepository : GenericRepository<TypePermission>, ITypePermissionsRepository
    {
        private readonly DataContext _context;

        public TypePermissionsRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<ActionResponse<IEnumerable<TypePermission>>> GetAsync()
        {
            var typePermissions = await _context.TypePermissions
                .OrderBy(x => x.Name)
                .ToListAsync();
            return new ActionResponse<IEnumerable<TypePermission>>
            {
                WasSuccess = true,
                Result = typePermissions
            };
        }

        public override async Task<ActionResponse<IEnumerable<TypePermission>>> GetAsync(PaginationDTO pagination)
        {
            var queryable = _context.TypePermissions
                .Include(c => c.CategoryPermissions)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Name.ToLower().Contains(pagination.Filter.ToLower()));
            }

            return new ActionResponse<IEnumerable<TypePermission>>
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
            var queryable = _context.TypePermissions.AsQueryable();

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

        public override async Task<ActionResponse<TypePermission>> GetAsync(int id)
        {
            var typePermission = await _context.TypePermissions
                 .Include(c => c.CategoryPermissions!)
                 .FirstOrDefaultAsync(c => c.Id == id);

            if (typePermission == null)
            {
                return new ActionResponse<TypePermission>
                {
                    WasSuccess = false,
                    Message = "Tipo no existe"
                };
            }

            return new ActionResponse<TypePermission>
            {
                WasSuccess = true,
                Result = typePermission
            };
        }
    }
}
