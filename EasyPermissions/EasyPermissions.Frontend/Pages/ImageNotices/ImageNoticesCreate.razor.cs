using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using EasyPermissions.Frontend.Repositories;
using EasyPermissions.Frontend.Shared;
using EasyPermissions.Shared.Entities;

namespace EasyPermissions.Frontend.Pages.ImageNotices
{
    public partial class ImageNoticesCreate
    {
        private ImageNotice imageNotices = new();
        private FormWithName<ImageNotice>? imageNoticesForm;

        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;
        [Parameter] public int NoticesId { get; set; }

        private async Task CreateAsync()
        {
            imageNotices.NoticeId = NoticesId;
            var responseHttp = await Repository.PostAsync("/api/ImageNotices", imageNotices);
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return;
            }
            Return();
            var toast = SweetAlertService.Mixin(new SweetAlertOptions
            {
                Toast = true,
                Position = SweetAlertPosition.BottomEnd,
                ShowConfirmButton = true,
                Timer = 3000
            });
            await toast.FireAsync(icon: SweetAlertIcon.Success, message: "Registro creado con éxito.");
        }

        private void Return()
        {
            imageNoticesForm!.FormPostedSuccessfully = true;
            NavigationManager.NavigateTo($"/imageNotices/details/{NoticesId}");
        }
    }
}
