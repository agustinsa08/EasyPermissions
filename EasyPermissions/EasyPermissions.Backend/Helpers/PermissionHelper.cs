using EasyPermissions.Backend.UnitsOfWork.Implementations;
using EasyPermissions.Backend.UnitsOfWork.Interfaces;
using EasyPermissions.Shared.Entities;
using EasyPermissions.Shared.Enums;
using EasyPermissions.Shared.Responses;

namespace EasyPermissions.Backend.Helpers
{
    public class PermissionHelper: IPermissionHelper
    {
        private readonly IUsersUnitOfWork _usersUnitOfWork;
        private readonly IPermissionsUnitOfWork _permissionsUnitOfWork;
        private readonly ICategoryPermissionsUnitOfWork _categoryPermissionsUnitOfWork;
        private readonly IMailHelper _mailHelper;

        public PermissionHelper(IUsersUnitOfWork usersUnitOfWork, IPermissionsUnitOfWork permissionsUnitOfWork, ICategoryPermissionsUnitOfWork categoryPermissionsUnitOfWork, IMailHelper mailHelper)
        {
            _usersUnitOfWork = usersUnitOfWork;
            _permissionsUnitOfWork = permissionsUnitOfWork;
            _categoryPermissionsUnitOfWork = categoryPermissionsUnitOfWork;
            _mailHelper = mailHelper;
        }

        public async Task<ActionResponse<bool>> ProcessPermissionAsync(string email, int categoryPermissionId, string description)
        {
            var user = await _usersUnitOfWork.GetDetailAsync(email);

            if (user == null)
            {
                return new ActionResponse<bool>
                {
                    WasSuccess = false,
                    Message = "Usuario no válido"
                };
            }

            var categoryPerm = await _categoryPermissionsUnitOfWork.GetAsync(categoryPermissionId);

            if (categoryPerm == null)
            {
                return new ActionResponse<bool>
                {
                    WasSuccess = false,
                    Message = "Categoría de permiso no válido"
                };
            }

            var permission = new Permission
            {
                Date = DateTime.UtcNow,
                User = user,
                CategoryPermissionId = categoryPermissionId,
                Description = description,
                Status = PermissionStatus.Pending,
                DateStatus = DateTime.UtcNow,
                PermissionDetails = new List<PermissionDetail>(),
                Leader = user?.Area?.User,
                LimitDays = categoryPerm?.Result?.LimitDays
            };

            permission.PermissionDetails.Add(new PermissionDetail
            {
                Date = DateTime.UtcNow,
                Status = PermissionStatus.Pending
            });

            _mailHelper.SendMail(user?.FullName!, user?.Email!,
             "Permisos Fácil - Registro de permiso",
             $@"
            <div style='font-family: Arial, sans-serif; color: #333; max-width: 600px; margin: auto; padding: 20px; border: 1px solid #ddd; border-radius: 10px;'>
                <h1 style='color: #4CAF50; text-align: center;'>Permisos Fácil - Registro de Permiso</h1>
        
                <p>Hola <strong>{user?.FullName}</strong>,</p>
        
                <p>Nos complace informarle que se ha registrado un nuevo permiso en el sistema con el estado <strong>Pendiente</strong>.</p>
        
                <p>Recibirá una notificación cuando el estado de su permiso cambie. Mientras tanto, puede consultar el estado actual en su cuenta en Permisos Fácil.</p>

                <hr style='border: none; border-top: 1px solid #ddd; margin: 20px 0;'>

                <p style='font-size: 12px; color: #666;'>Este mensaje fue enviado automáticamente por el sistema de Permisos Fácil. Si no realizó esta solicitud, puede ignorar este correo.</p>
            </div>");


            if (user?.Area?.User != null)
            {
                _mailHelper.SendMail(user?.Area?.User?.FullName!, user?.Area?.User?.Email!,
                "Permisos Fácil - Registro de Permiso",
                $@"
                <div style='font-family: Arial, sans-serif; color: #333; max-width: 600px; margin: auto; padding: 20px; border: 1px solid #ddd; border-radius: 10px;'>
                    <h1 style='color: #4CAF50; text-align: center;'>Permisos Fácil - Registro de Permiso</h1>
        
                    <p>Hola <strong>{user?.Area?.User?.FullName}</strong>,</p>
        
                    <p>Se ha registrado un nuevo permiso en el sistema con el estado <strong>Pendiente</strong>. A continuación, se muestran los detalles del registro:</p>

                    <div style='background-color: #f9f9f9; padding: 15px; border: 1px solid #ddd; border-radius: 5px; margin: 15px 0;'>
                        <p><strong>Colaborador:</strong> {user?.FullName}</p>
                        <p><strong>Fecha de Registro:</strong> {DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(-5)).ToString("yyyy-MM-dd HH:mm")} (UTC-5)</p>
                        <p><strong>Estado:</strong> Pendiente</p>
                    </div>
        
                    <p>Recibirá una notificación adicional cuando el estado del permiso cambie.</p>

                    <hr style='border: none; border-top: 1px solid #ddd; margin: 20px 0;'>

                    <p style='font-size: 12px; color: #666;'>Este mensaje fue enviado automáticamente por el sistema de Permisos Fácil. Si no está relacionado con esta solicitud, puede ignorar este correo.</p>
                </div>");

            }

            var adminList = await _usersUnitOfWork.GetAllAdminAsync();

            if (adminList != null)
            {
                foreach (var ele in adminList)
                {
                    _mailHelper.SendMail(ele?.FullName!, ele?.Email!,
                "Permisos Fácil - Registro de Permiso",
                $@"
                <div style='font-family: Arial, sans-serif; color: #333; max-width: 600px; margin: auto; padding: 20px; border: 1px solid #ddd; border-radius: 10px;'>
                    <h1 style='color: #4CAF50; text-align: center;'>Permisos Fácil - Registro de Permiso</h1>
        
                    <p>Hola <strong>{ele?.FullName}</strong>,</p>
        
                    <p>Se ha registrado un nuevo permiso en el sistema con el estado <strong>Pendiente</strong>. A continuación, se muestran los detalles del registro:</p>

                    <div style='background-color: #f9f9f9; padding: 15px; border: 1px solid #ddd; border-radius: 5px; margin: 15px 0;'>
                        <p><strong>Área:</strong> {user?.Area?.Name}</p>
                        <p><strong>Líder:</strong> {user?.Area?.User?.FullName!}</p>
                        <p><strong>Colaborador:</strong> {user?.FullName}</p>
                        <p><strong>Fecha de Registro:</strong> {DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(-5)).ToString("yyyy-MM-dd HH:mm")} (UTC-5)</p>
                        <p><strong>Estado:</strong> Pendiente</p>
                    </div>
        
                    <p>Recibirá una notificación adicional cuando el estado del permiso cambie.</p>

                    <hr style='border: none; border-top: 1px solid #ddd; margin: 20px 0;'>

                    <p style='font-size: 12px; color: #666;'>Este mensaje fue enviado automáticamente por el sistema de Permisos Fácil. Si no está relacionado con esta solicitud, puede ignorar este correo.</p>
                </div>");
                }
            }

            var response = await _permissionsUnitOfWork.AddAsync(permission);
            return new ActionResponse<bool>
            {
                WasSuccess = response.WasSuccess,
                Message = response.Message
            };
        }
    }
}

