using EasyPermissions.Backend.Data;
using EasyPermissions.Backend.Helpers;
using EasyPermissions.Backend.Repositories.Interfaces;
using EasyPermissions.Shared.DTOs;
using EasyPermissions.Shared.Entities;
using EasyPermissions.Shared.Responses;
using Microsoft.EntityFrameworkCore;

namespace EasyPermissions.Backend.Repositories.Implementations
{
    public class PermissionsRepository : GenericRepository<Permission>, IPermissionsRepository
    {
        private readonly DataContext _context;

        public PermissionsRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<ActionResponse<IEnumerable<Permission>>> GetAsync()
        {
            var permissions = await _context.Permissions
                .OrderBy(x => x.Description)
                .ToListAsync();
            return new ActionResponse<IEnumerable<Permission>>
            {
                WasSuccess = true,
                Result = permissions
            };
        }

        public override async Task<ActionResponse<IEnumerable<Permission>>> GetAsync(PaginationDTO pagination)
        {
            var queryable = _context.Permissions
                .Include(c => c.Status)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Description.ToLower().Contains(pagination.Filter.ToLower()));
            }

            return new ActionResponse<IEnumerable<Permission>>
            {
                WasSuccess = true,
                Result = await queryable
                    .OrderBy(x => x.Description)
                    .Paginate(pagination)
                    .ToListAsync()
            };
        }

        public override async Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination)
        {
            var queryable = _context.Permissions.AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Description.ToLower().Contains(pagination.Filter.ToLower()));
            }

            double count = await queryable.CountAsync();
            int totalPages = (int)Math.Ceiling(count / pagination.RecordsNumber);
            return new ActionResponse<int>
            {
                WasSuccess = true,
                Result = totalPages
            };
        }

        public override async Task<ActionResponse<Permission>> GetAsync(int id)
        {
            var permission = await _context.Permissions
                 .Include(c => c.Status!)
                 .FirstOrDefaultAsync(c => c.Id == id);

            if (permission == null)
            {
                return new ActionResponse<Permission>
                {
                    WasSuccess = false,
                    Message = "Permiso no existe"
                };
            }

            return new ActionResponse<Permission>
            {
                WasSuccess = true,
                Result = permission
            };
        }
    }
}