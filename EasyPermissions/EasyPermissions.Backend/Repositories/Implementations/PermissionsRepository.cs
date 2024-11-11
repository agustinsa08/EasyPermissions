using EasyPermissions.Backend.Data;
using EasyPermissions.Backend.Helpers;
using EasyPermissions.Backend.Repositories.Interfaces;
using EasyPermissions.Shared.DTOs;
using EasyPermissions.Shared.Entities;
using EasyPermissions.Shared.Enums;
using EasyPermissions.Shared.Responses;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.ComponentModel;

namespace EasyPermissions.Backend.Repositories.Implementations
{
    public class PermissionsRepository : GenericRepository<Permission>, IPermissionsRepository
    {
        private readonly DataContext _context;
        private readonly IUsersRepository _usersRepository;
        private readonly IMailHelper _mailHelper;

        public PermissionsRepository(DataContext context, IUsersRepository usersRepository, IMailHelper mailHelper) : base(context)
        {
            _context = context;
            _usersRepository = usersRepository;
            _mailHelper = mailHelper;
        }

        public override async Task<ActionResponse<IEnumerable<Permission>>> GetAsync()
        {
            var permissions = await _context.Permissions
                .OrderBy(x => x.Description)
                 .Include(c => c.UserId!)
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
                .Include(c => c.User)
                .Include(c => c.CategoryPermission)
                .ThenInclude(c => c!.TypePermission!)
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
                .Include(c => c.User)
                .Include(c => c.CategoryPermission)
                .ThenInclude(c => c!.TypePermission!)
                .Include(c => c.PermissionDetails)
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

        public async Task<ActionResponse<Permission>> UpdateFullAsync(string email, PermissionDTO permissionDTO)
        {
            var user = await _usersRepository.GetUserAsync(email);
            if (user == null)
            {
                return new ActionResponse<Permission>
                {
                    WasSuccess = false,
                    Message = "Usuario no existe"
                };
            }

            var isAdmin = await _usersRepository.IsUserInRoleAsync(user, UserType.Admin.ToString());
            var isLeader = await _usersRepository.IsUserInRoleAsync(user, UserType.Leader.ToString());
            if (!isAdmin && !isLeader && permissionDTO.Status != PermissionStatus.Approved)
            {
                return new ActionResponse<Permission>
                {
                    WasSuccess = false,
                    Message = "Solo permitido para líderes."
                };
            }

            //Console.WriteLine($"permissionDTO: {permissionDTO}");
            //Console.WriteLine($"permissionDTO.Id: {permissionDTO.Id}");

            var permission = await _context.Permissions
                .Include(c => c.User)
                .Include(c => c.CategoryPermission)
                .ThenInclude(c => c!.TypePermission!)
                .Include(c => c.PermissionDetails)
                .FirstOrDefaultAsync(s => s.Id == permissionDTO.Id);
            if (permission == null)
            {
                return new ActionResponse<Permission>
                {
                    WasSuccess = false,
                    Message = "Permiso no existe"
                };
            }

            permission.User = permission.User;
            permission.Date = permission.Date;
            permission.CategoryPermissionId = permission.CategoryPermissionId;
            permission.Description = permission.Description;
            permission.DateStatus = DateTime.UtcNow;
            permission.Status = permissionDTO.Status;
            _context.Update(permission);
            permission.PermissionDetails!.Add(new PermissionDetail
            {
                PermissionId = permissionDTO.Id,
                Date = DateTime.UtcNow,
                Status = permissionDTO.Status
            });
            await _context.SaveChangesAsync();

            DateTime utcDate = permission.Date.Kind == DateTimeKind.Utc ? permission.Date : permission.Date.ToUniversalTime();

            TimeZoneInfo timeZone = TimeZoneInfo.FindSystemTimeZoneById("SA Pacific Standard Time");

            DateTime localDate = TimeZoneInfo.ConvertTimeFromUtc(utcDate, timeZone);

            string formattedDate = localDate.ToString("dd MMMM yyyy HH:mm");

            _mailHelper.SendMail(user?.FullName!, user?.Email!,
             "Permisos Fácil - Actualización de permiso",
             $@"
            <div style='font-family: Arial, sans-serif; color: #333; max-width: 600px; margin: auto; padding: 20px; border: 1px solid #ddd; border-radius: 10px;'>
                <h1 style='color: #4CAF50; text-align: center;'>Permisos Fácil - Registro de Permiso</h1>
        
                <p>Hola <strong>{user?.FullName}</strong>,</p>
        
                <p>Nos complace informarle que se ha registrado un nuevo estado para su permiso solicitado el día <strong>{formattedDate}</strong>. A continuación, el detalle:</p>

                    <div style='background-color: #f9f9f9; padding: 15px; border: 1px solid #ddd; border-radius: 5px; margin: 15px 0;'>
                        <p><strong>Área:</strong> {user?.Area?.Name}</p>
                        <p><strong>Líder:</strong> {user?.Area?.User?.FullName!}</p>
                        <p><strong>Colaborador:</strong> {user?.FullName}</p>
                        <p><strong>Fecha de Registro:</strong>{formattedDate}</p>
                        <p><strong>Estado:</strong>{GetDescription(permission.Status)}</p>
                    </div>
        
                <p>Recibirá una notificación cuando el estado de su permiso cambie. Mientras tanto, puede consultar el estado actual en su cuenta en Permisos Fácil.</p>

                <hr style='border: none; border-top: 1px solid #ddd; margin: 20px 0;'>

                <p style='font-size: 12px; color: #666;'>Este mensaje fue enviado automáticamente por el sistema de Permisos Fácil. Si no realizó esta solicitud, puede ignorar este correo.</p>
            </div>");

            _mailHelper.SendMail(user?.Area?.User?.FullName!, user?.Area?.User?.Email!,
             "Permisos Fácil - Actualización de permiso",
             $@"
            <div style='font-family: Arial, sans-serif; color: #333; max-width: 600px; margin: auto; padding: 20px; border: 1px solid #ddd; border-radius: 10px;'>
                <h1 style='color: #4CAF50; text-align: center;'>Permisos Fácil - Registro de Permiso</h1>
        
                <p>Hola <strong>{user?.FullName}</strong>,</p>
        
                <p>Nos complace informarle que se ha registrado un nuevo estado para un permiso solicitado por <strong>{user?.FullName}</strong>. A continuación, el detalle:</p>

                    <div style='background-color: #f9f9f9; padding: 15px; border: 1px solid #ddd; border-radius: 5px; margin: 15px 0;'>
                        <p><strong>Área:</strong> {user?.Area?.Name}</p>
                        <p><strong>Líder:</strong> {user?.Area?.User?.FullName!}</p>
                        <p><strong>Colaborador:</strong> {user?.FullName}</p>
                        <p><strong>Fecha de Registro:</strong>{formattedDate}</p>
                        <p><strong>Estado:</strong>{GetDescription(permission.Status)}</p>
                    </div>
        
                <p>Recibirá una notificación cuando el estado de su permiso cambie. Mientras tanto, puede consultar el estado actual en su cuenta en Permisos Fácil.</p>

                <hr style='border: none; border-top: 1px solid #ddd; margin: 20px 0;'>

                <p style='font-size: 12px; color: #666;'>Este mensaje fue enviado automáticamente por el sistema de Permisos Fácil. Si no realizó esta solicitud, puede ignorar este correo.</p>
            </div>");

            return new ActionResponse<Permission>
            {
                WasSuccess = true,
                Result = permission
            };
        }

        public async Task<ActionResponse<IEnumerable<Permission>>> GetAllLeaderAsync(Guid userId, PaginationDTO pagination)
        {
            var user = await _usersRepository.GetUserAsync(userId);
            if (user == null)
            {
                return new ActionResponse<IEnumerable<Permission>>
                {
                    WasSuccess = false,
                    Message = "Usuario no existe"
                };
            }

            var isLeader = await _usersRepository.IsUserInRoleAsync(user, UserType.Leader.ToString());
            if (!isLeader)
            {
                return new ActionResponse<IEnumerable<Permission>>
                {
                    WasSuccess = false,
                    Message = "El usuario no es líder"
                };
            }

            var queryable = _context.Permissions
                .Where(x => x.UserId == userId.ToString())
                .Include(c => c.User)
                .Include(c => c.CategoryPermission)
                .ThenInclude(c => c!.TypePermission!)
                .OrderByDescending(x => x.Date)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Description.ToLower().Contains(pagination.Filter.ToLower()));
            }

            return new ActionResponse<IEnumerable<Permission>>
            {
                WasSuccess = true,
                Result = await queryable
                    .Paginate(pagination)
                    .ToListAsync()
            };
        }

        public async Task<ActionResponse<IEnumerable<Permission>>> GetAllUserAsync(Guid userId, PaginationDTO pagination)
        {

            var queryable = _context.Permissions
                .Where(x => x.UserId == userId.ToString())
                .Include(c => c.User)
                .Include(c => c.CategoryPermission)
                .ThenInclude(c => c!.TypePermission!)
                .OrderByDescending(x => x.Date)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Description.ToLower().Contains(pagination.Filter.ToLower()));
            }

            return new ActionResponse<IEnumerable<Permission>>
            {
                WasSuccess = true,
                Result = await queryable
                    .Paginate(pagination)
                    .ToListAsync()
            };
        }

        private string GetDescription(PermissionStatus value)
        {
            var fieldInfo = value.GetType().GetField(value.ToString());
            var attributes = fieldInfo!.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];
            return attributes!.Length > 0 ? attributes[0].Description : value.ToString();
        }
    }
}