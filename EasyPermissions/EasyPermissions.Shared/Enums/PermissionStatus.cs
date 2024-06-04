using System.ComponentModel;

namespace EasyPermissions.Shared.Enums
{
    public enum PermissionStatus
    {
        [Description("Pendiente")]
        Pending,
        [Description("Recibido")]
        Received,
        [Description("Aprobado")]
        Approved,
        [Description("No aprobado")]
        NotApproved,
    }
}
