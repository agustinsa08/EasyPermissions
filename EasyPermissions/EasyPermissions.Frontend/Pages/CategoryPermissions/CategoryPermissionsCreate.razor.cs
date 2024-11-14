using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using EasyPermissions.Frontend.Repositories;
using EasyPermissions.Frontend.Shared;
using EasyPermissions.Shared.Entities;

namespace EasyPermissions.Frontend.Pages.CategoryPermissions
{
    public partial class CategoryPermissionsCreate
    {
        private CategoryPermission categoryPermissions = new();
        private CategoryPermissionsForm? categoryPermissionsForm;

        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;
        [Parameter] public int TypePermissionsId { get; set; }

        private async Task CreateAsync()
        {
            categoryPermissions.TypePermissionId = TypePermissionsId;
            var responseHttp = await Repository.PostAsync("/api/CategoryPermissions", categoryPermissions);
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
            categoryPermissionsForm!.FormPostedSuccessfully = true;
            NavigationManager.NavigateTo($"/typePermissions/details/{TypePermissionsId}");
        }
    }
}
