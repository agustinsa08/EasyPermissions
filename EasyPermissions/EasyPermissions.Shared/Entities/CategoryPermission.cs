using EasyPermissions.Shared.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace EasyPermissions.Shared.Entities
{
    public class CategoryPermission : IEntityWithName
    {
        public int Id { get; set; }

        [Display(Name = "Nombre")]
        [MaxLength(100, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string Name { get; set; } = null!;

        [Display(Name = "Descripción")]
        public string? Description { get; set; } = null!;

        [Display(Name = "Estado")]
        [Required(ErrorMessage = "El campo {2} es requerido.")]
        public int? Status { get; set; } = 1;

        public int TypePermissionId { get; set; }

        public TypePermission? TypePermission { get; set; }

        public ICollection<Permission>? Permissions { get; set; }

        [Display(Name = "Permisos")]
        public int PermissionNumber => Permissions == null || Permissions.Count == 0 ? 0 : Permissions.Count;

    }
}
