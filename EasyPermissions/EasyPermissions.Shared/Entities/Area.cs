using EasyPermissions.Shared.Interfaces;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EasyPermissions.Shared.Entities
{
    public class Area : IEntityWithName
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
        [DefaultValue(1)]
        public int? Status { get; set; } = 1;

        [Display(Name = "Líder")]
        public string? UserId { get; set; }

        public User? User { get; set; }

        public ICollection<User>? Users { get; set; }
    }
}
