using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using EasyPermissions.Frontend.Repositories;
using EasyPermissions.Frontend.Shared;
using EasyPermissions.Shared.Entities;

namespace EasyPermissions.Frontend.Pages.CategoryNotices
{
    public partial class CategoryNoticesCreate
    {
        private CategoryNotice categoryNotices = new();
        private CategoryNoticesForm? categoryNoticesForm;

        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;
        [Parameter] public int TypeNoticesId { get; set; }

        private async Task CreateAsync()
        {
            categoryNotices.TypeNoticeId = TypeNoticesId;

            Console.WriteLine(TypeNoticesId);
            var responseHttp = await Repository.PostAsync("/api/CategoryNotices", categoryNotices);
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
            categoryNoticesForm!.FormPostedSuccessfully = true;
            NavigationManager.NavigateTo($"/typeNotices/details/{TypeNoticesId}");
        }
    }
}
