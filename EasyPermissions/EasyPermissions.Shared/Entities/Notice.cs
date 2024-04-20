using EasyPermissions.Shared.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace EasyPermissions.Shared.Entities
{
    public class Notice : IEntityWithName
    {
        public int Id { get; set; }

        [Display(Name = "Nombre")]
        [MaxLength(100, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string Name { get; set; } = null!;

        [Display(Name = "Descripción")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string Description { get; set; } = null!;

        [Display(Name = "Estado")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public int status { get; set; } = 1;

        public int CategoryNoticeId { get; set; }

        public CategoryNotice? CategoryNotice { get; set; }

        public ICollection<ImageNotice>? ImageNotices { get; set; }

        [Display(Name = "Imágenes")]
        public int ImageNoticesNumber => ImageNotices == null || ImageNotices.Count == 0 ? 0 : ImageNotices.Count;
    }
}
