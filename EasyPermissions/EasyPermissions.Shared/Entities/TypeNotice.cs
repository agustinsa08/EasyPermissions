using EasyPermissions.Shared.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace EasyPermissions.Shared.Entities
{
    public class TypeNotice : IEntityWithName
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
        public int Status { get; set; } = 1;

        public ICollection<CategoryNotice>? CategoryNotices { get; set; }

        [Display(Name = "Categorías")]
        public int CategoryNoticesNumber => CategoryNotices == null || CategoryNotices.Count == 0 ? 0 : CategoryNotices.Count;
    }
}