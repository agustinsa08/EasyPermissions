using EasyPermissions.Backend.Data;
using EasyPermissions.Backend.Helpers;
using EasyPermissions.Backend.Repositories.Interfaces;
using EasyPermissions.Shared.DTOs;
using EasyPermissions.Shared.Entities;
using EasyPermissions.Shared.Responses;
using Microsoft.EntityFrameworkCore;

namespace EasyPermissions.Backend.Repositories.Implementations
{
    public class TypeNoticesRepository : GenericRepository<TypeNotice>, ITypeNoticesRepository
    {
        private readonly DataContext _context;

        public TypeNoticesRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<ActionResponse<TypeNotice>> GetAsync(int id)
        {
            var typeNotice = await _context.TypeNotices
                 .FirstOrDefaultAsync(s => s.Id == id);

            if (typeNotice == null)
            {
                return new ActionResponse<TypeNotice>
                {
                    WasSuccess = false,
                    Message = "Tipo no existe"
                };
            }

            return new ActionResponse<TypeNotice>
            {
                WasSuccess = true,
                Result = typeNotice
            };
        }

        public override async Task<ActionResponse<IEnumerable<TypeNotice>>> GetAsync()
        {
            var typeNotices = await _context.TypeNotices
                .OrderBy(x => x.Name)
                .ToListAsync();
            return new ActionResponse<IEnumerable<TypeNotice>>
            {
                WasSuccess = true,
                Result = typeNotices
            };
        }

        public override async Task<ActionResponse<IEnumerable<TypeNotice>>> GetAsync(PaginationDTO pagination)
        {
            var queryable = _context.TypeNotices
                .Where(x => x.CategoryNotice!.Id == pagination.Id)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Name.ToLower().Contains(pagination.Filter.ToLower()));
            }

            return new ActionResponse<IEnumerable<TypeNotice>>
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
            var queryable = _context.TypeNotices
                .Where(x => x.CategoryNotice!.Id == pagination.Id)
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
    }
}