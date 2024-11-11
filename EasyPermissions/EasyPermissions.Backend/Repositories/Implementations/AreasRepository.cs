using EasyPermissions.Backend.Data;
using EasyPermissions.Backend.Repositories.Interfaces;
using EasyPermissions.Shared.DTOs;
using EasyPermissions.Shared.Entities;
using EasyPermissions.Shared.Responses;
using EasyPermissions.Backend.Helpers;
using Microsoft.EntityFrameworkCore;

namespace EasyPermissions.Backend.Repositories.Implementations
{
     public class AreasRepository : GenericRepository<Area>, IAreasRepository
    {
        private readonly DataContext _context;

        public AreasRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<ActionResponse<IEnumerable<Area>>> GetAsync(PaginationDTO pagination)
        {
            var queryable = _context.Areas.AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Name.ToLower().Contains(pagination.Filter.ToLower()));
            }

            return new ActionResponse<IEnumerable<Area>>
            {
                WasSuccess = true,
                Result = await queryable
                    .OrderBy(x => x.Name)
                    .Include(a => a.User!)
                    .Paginate(pagination)
                    .ToListAsync()
            };
        }

        public override async Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination)
        {
            var queryable = _context.Areas.AsQueryable();

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

        public async Task<List<Area>> GetAllWhithoutLeaderAsync()
        {
            var areas = await _context.Areas
                .Where(x => x.UserId == null)
                .OrderBy(x => x.Id)
                .ToListAsync();
            return areas;
        }
    }
}
