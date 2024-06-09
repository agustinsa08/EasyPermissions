using EasyPermissions.Shared.Entities;
using EasyPermissions.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace EasyPermissions.Shared.DTOs
{
    public class PermissionDTO
    {
        public int Id { get; set; }

        [Display(Name = "Categoría Permiso")]
        public int CategoryPermissionId { get; set; }

        [Display(Name = "Descripción")]
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string Description { get; set; } = null!;

        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm tt}")]
        [Display(Name = "Fecha")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public DateTime Date { get; set; }

        public PermissionStatus Status { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm tt}")]
        [Display(Name = "Fecha estado")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public DateTime DateStatus { get; set; }

    }
}