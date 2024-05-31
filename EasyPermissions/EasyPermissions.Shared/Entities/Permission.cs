using EasyPermissions.Shared.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyPermissions.Shared.Entities
{
    public class Permission
    {
        public int Id { get; set; }

        [Display(Name = "Descripción")]
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string Description { get; set; } = null!;

        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm tt}")]
        [Display(Name = "Fecha")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public DateTime Date { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm tt}")]
        [Display(Name = "Fecha estado")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public DateTime DateStatus { get; set; }

        [ForeignKey("UserCollaboratorId")]
        public User? UserCollaborator { get; set; }

        [Display(Name = "Colaborador")]
        public string UserCollaboratorId { get; set; }

        public PermissionStatus Status { get; set; } = PermissionStatus.Pending;

        [ForeignKey("CategoryPermissionId")]
        public CategoryPermission? CategoryPermission { get; set; }

        [Display(Name = "Categoría Permiso")]
        public int CategoryPermissionId { get; set; }

        public ICollection<PermissionDetail>? PermissionDetails { get; set; }

        [Display(Name = "Permisos")]
        public int PermissionDetailNumber => PermissionDetails == null || PermissionDetails.Count == 0 ? 0 : PermissionDetails.Count;
    }
}
