using EasyPermissions.Shared.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace EasyPermissions.Shared.Entities
{
    public class CategoryNotice : IEntityWithName
    {
        public int Id { get; set; }

        [Display(Name = "Nombre")]
        [MaxLength(100, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string Name { get; set; } = null!;

        [Display(Name = "Descripción")]
        public string Description { get; set; } = null!;

        [Display(Name = "Estado")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public int status { get; set; } = 1;

        public ICollection<TypeNotice>? TypeNotices { get; set; }

        [Display(Name = "Tipos")]
        public int TypeNoticesNumber => TypeNotices == null || TypeNotices.Count == 0 ? 0 : TypeNotices.Count;

        public ICollection<Notice>? Notices { get; set; }

        [Display(Name = "Noticias")]
        public int NoticeNumber => Notices == null || Notices.Count == 0 ? 0 : Notices.Count;

    }
}
