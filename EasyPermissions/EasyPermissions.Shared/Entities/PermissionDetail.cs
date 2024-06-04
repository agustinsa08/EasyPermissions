using EasyPermissions.Shared.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyPermissions.Shared.Entities
{
    public class PermissionDetail
    {
        public int Id { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm tt}")]
        [Display(Name = "Fecha")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public DateTime Date { get; set; }

        public PermissionStatus Status { get; set; }

        public Permission? Permission { get; set; }

        public int PermissionId { get; set; }

    }
}
