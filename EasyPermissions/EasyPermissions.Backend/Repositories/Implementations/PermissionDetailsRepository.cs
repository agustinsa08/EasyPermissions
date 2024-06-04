using EasyPermissions.Backend.Data;
using EasyPermissions.Backend.Helpers;
using EasyPermissions.Backend.Repositories.Interfaces;
using EasyPermissions.Shared.DTOs;
using EasyPermissions.Shared.Entities;
using EasyPermissions.Shared.Responses;
using Microsoft.EntityFrameworkCore;

namespace EasyPermissions.Backend.Repositories.Implementations
{
    public class PermissionDetailsRepository : GenericRepository<PermissionDetail>, IPermissionDetailsRepository
    {
        private readonly DataContext _context;

        public PermissionDetailsRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<ActionResponse<IEnumerable<PermissionDetail>>> GetAsync()
        {
            var permissionDetails = await _context.PermissionDetails
                .OrderBy(x => x.Status)
                .ToListAsync();
            return new ActionResponse<IEnumerable<PermissionDetail>>
            {
                WasSuccess = true,
                Result = permissionDetails
            };
        }

        public override async Task<ActionResponse<IEnumerable<PermissionDetail>>> GetAsync(PaginationDTO pagination)
        {
            var queryable = _context.PermissionDetails
                .Include(c => c.Status)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Status.ToString().Contains(pagination.Filter.ToLower()));
            }

            return new ActionResponse<IEnumerable<PermissionDetail>>
            {
                WasSuccess = true,
                Result = await queryable
                    .OrderBy(x => x.Status)
                    .Paginate(pagination)
                    .ToListAsync()
            };
        }

        public override async Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination)
        {
            var queryable = _context.PermissionDetails.AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Status.ToString().ToLower().Contains(pagination.Filter.ToLower()));
            }

            double count = await queryable.CountAsync();
            int totalPages = (int)Math.Ceiling(count / pagination.RecordsNumber);
            return new ActionResponse<int>
            {
                WasSuccess = true,
                Result = totalPages
            };
        }

        public override async Task<ActionResponse<PermissionDetail>> GetAsync(int id)
        {
            var permissionDetail = await _context.PermissionDetails
                 .Include(c => c.Status!)
                 .FirstOrDefaultAsync(c => c.Id == id);

            if (permissionDetail == null)
            {
                return new ActionResponse<PermissionDetail>
                {
                    WasSuccess = false,
                    Message = "Detalle permiso no existe"
                };
            }

            return new ActionResponse<PermissionDetail>
            {
                WasSuccess = true,
                Result = permissionDetail
            };
        }
    }
}