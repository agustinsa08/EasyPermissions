using EasyPermissions.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace EasyPermissions.Shared.Entities
{
    public class Permission  
    {
        public int Id { get; set; }

        [Display(Name = "Categoría Permiso")]
        public int CategoryPermissionId { get; set; }

        public CategoryPermission? CategoryPermission { get; set; }

        [Display(Name = "Descripción")]
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string Description { get; set; } = null!;

        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm tt}")]
        [Display(Name = "Fecha")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public DateTime Date { get; set; }

        [Display(Name = "Colaborador")]
        public string? UserId { get; set; }

        public User? User { get; set; }

        [Display(Name = "Líder")]
        public string? LeaderId { get; set; }

        public User? Leader { get; set; }

        public PermissionStatus Status { get; set; } = PermissionStatus.Pending;

        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm tt}")]
        [Display(Name = "Fecha estado")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public DateTime DateStatus { get; set; }

        public ICollection<PermissionDetail>? PermissionDetails { get; set; }

        [Display(Name = "Detalles de Permiso")]
        public int PermissionDetailNumber => PermissionDetails == null || PermissionDetails.Count == 0 ? 0 : PermissionDetails.Count;

        [Display(Name = "Límites de días para respuesta")]
        [Required(ErrorMessage = "El campo {2} es requerido.")]
        public int? LimitDays { get; set; } = 5;
    }
}