using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace EasyPermissions.Frontend.Shared
{
    public partial class InputImg
    {
        private string? imageBase64;

        public string allowedFileTypes = ".jpg,.jpeg,.png";

        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;
        [Parameter] public string Label { get; set; } = "Imagen";
        [Parameter] public string? ImageURL { get; set; }
        [Parameter] public EventCallback<string> ImageSelected { get; set; }

        private async Task OnChange(InputFileChangeEventArgs e)
        {
            var imagenes = e.GetMultipleFiles();

            long maxFileSize = 2 * 1024 * 1024; // 2 MB en bytes

            foreach (var imagen in imagenes)
            {

                if (imagen.Size > maxFileSize)
                {
                    await SweetAlertService.FireAsync("Error", "El archivo es demasiado grande. El tamaño máximo permitido es 2MB", SweetAlertIcon.Error);
                    imageBase64 = null;
                    await ImageSelected.InvokeAsync(null);
                    StateHasChanged();
                    return;
                }

                var allowedTypes = allowedFileTypes.Split(',');

                var fileExtension = Path.GetExtension(imagen.Name).ToLower();

                if (!allowedFileTypes.Contains(fileExtension))
                {
                    imageBase64 = null;
                    await SweetAlertService.FireAsync("Error", "El tipo de archivo " + fileExtension + " no esta permitido", SweetAlertIcon.Error);
                    await ImageSelected.InvokeAsync(null);
                    StateHasChanged();
                    return;
                }


                var arrBytes = new byte[imagen.Size];
                await imagen.OpenReadStream(maxFileSize).ReadAsync(arrBytes);
                imageBase64 = Convert.ToBase64String(arrBytes);
                ImageURL = null;
                await ImageSelected.InvokeAsync(imageBase64);
                StateHasChanged();
            }
        }
    }
}