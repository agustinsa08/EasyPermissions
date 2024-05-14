using System.ComponentModel;

namespace EasyPermissions.Shared.Enums
{
    public enum UserType
    {
        [Description("Administrador")]
        Admin,

        [Description("Lider")]
        Leader,

        [Description("Usuario")]
        User,
    }
}