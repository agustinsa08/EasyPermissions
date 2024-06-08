using EasyPermissions.Backend.Data;
using EasyPermissions.Backend.Helpers;
using EasyPermissions.Backend.Repositories.Interfaces;
using EasyPermissions.Shared.DTOs;
using EasyPermissions.Shared.Entities;
using EasyPermissions.Shared.Enums;
using EasyPermissions.Shared.Responses;
using Microsoft.EntityFrameworkCore;

namespace EasyPermissions.Backend.Repositories.Implementations
{
    public class PermissionsRepository : GenericRepository<Permission>, IPermissionsRepository
    {
        private readonly DataContext _context;
        private readonly IUsersRepository _usersRepository;

        public PermissionsRepository(DataContext context, IUsersRepository usersRepository) : base(context)
        {
            _context = context;
            _usersRepository = usersRepository;
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

        public async Task<ActionResponse<IEnumerable<Permission>>> GetAsync(string email, PaginationDTO pagination)
        {
            var user = await _usersRepository.GetUserAsync(email);
            if (user == null)
            {
                return new ActionResponse<IEnumerable<Permission>>
                {
                    WasSuccess = false,
                    Message = "Usuario no válido",
                };
            }
            var queryable = _context.Permissions
                .Include(c => c.CategoryPermission)
                .AsQueryable();

            var isAdmin = await _usersRepository.IsUserInRoleAsync(user, UserType.Admin.ToString());
            if (!isAdmin)
            {
                queryable = queryable.Where(s => s.User!.Email == email);
            }

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Description.ToLower().Contains(pagination.Filter.ToLower()));
            }

            return new ActionResponse<IEnumerable<Permission>>
            {
                WasSuccess = true,
                Result = await queryable
                    .OrderByDescending(x => x.Date)
                    .Paginate(pagination)
                    .ToListAsync()
            };
        }

        public async Task<ActionResponse<int>> GetTotalPagesAsync(string email, PaginationDTO pagination)
        {
            var user = await _usersRepository.GetUserAsync(email);
            if (user == null)
            {
                return new ActionResponse<int>
                {
                    WasSuccess = false,
                    Message = "Usuario no válido",
                };
            }

            var queryable = _context.Permissions.AsQueryable();

            var isAdmin = await _usersRepository.IsUserInRoleAsync(user, UserType.Admin.ToString());
            if (!isAdmin)
            {
                queryable = queryable.Where(s => s.User!.Email == email);
            }

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Description.ToLower().Contains(pagination.Filter.ToLower()));
            }

            double count = await queryable.CountAsync();
            int totalPages = (int)Math.Ceiling(count / pagination.RecordsNumber);
            return new ActionResponse<int>
            {
                WasSuccess = true,
                Result = (int)totalPages
            };
        }

        public override async Task<ActionResponse<Permission>> GetAsync(int id)
        {
            var permission = await _context.Permissions
                 .Include(c => c.CategoryPermission!)
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