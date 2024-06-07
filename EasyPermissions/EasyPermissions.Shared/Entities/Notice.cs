using EasyPermissions.Shared.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace EasyPermissions.Shared.Entities
{
    public class Notice : IEntityWithName
    {
        public int Id { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string Name { get; set; } = null!;

        [Display(Name = "Descripción")]
        [Required(ErrorMessage = "El campo {1} es requerido.")]
        public string Description { get; set; } = null!;

        [Display(Name = "Estado")]
        [Required(ErrorMessage = "El campo {2} es requerido.")]
        public int Status { get; set; } = 1;

        [Required(ErrorMessage = "El campo {3} es requerido.")]
        public int CategoryNoticeId { get; set; }

        public CategoryNotice? CategoryNotice { get; set; }

        public ICollection<ImageNotice>? ImageNotices { get; set; }

        [Display(Name = "Imágenes")]
        public int ImageNoticesNumber => ImageNotices == null || ImageNotices.Count == 0 ? 0 : ImageNotices.Count;

        [Display(Name = "Imagen")]
        public string MainImage => ImageNotices == null || ImageNotices.Count == 0 ? string.Empty : ImageNotices.FirstOrDefault()!.File!;
    }
}
