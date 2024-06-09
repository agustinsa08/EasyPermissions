using CurrieTechnologies.Razor.SweetAlert2;
using EasyPermissions.Frontend.Repositories;
using EasyPermissions.Shared.DTOs;
using EasyPermissions.Shared.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

namespace EasyPermissions.Frontend.Pages.Permissions
{
    [Authorize(Roles = "Admin, Leader, Collaborator")]
    public partial class PermissionCreate
    {
        public PermissionDTO PermissionDTO { get; set; } = new();
        private PermissionForm? permissionForm;
        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;

        private async Task CreateAsync()
        {
            var responseHttp = await Repository.PostAsync("/api/permissions", PermissionDTO);
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
            permissionForm!.FormPostedSuccessfully = true;
            NavigationManager.NavigateTo("/permissions");
        }
    }
}

