using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using EasyPermissions.Frontend.Repositories;
using EasyPermissions.Shared.DTOs;

namespace EasyPermissions.Frontend.Pages.Auth
{
    public partial class ResendConfirmationEmailToken
    {
        private EmailDTO emailDTO = new();
        private bool loading;

        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;
        [Inject] private IRepository Repository { get; set; } = null!;

        private async Task ResendConfirmationEmailTokenAsync()
        {
            loading = true;
            var responseHttp = await Repository.PostAsync("/api/accounts/ResedToken", emailDTO);
            loading = false;
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                loading = false;
                return;
            }

            await SweetAlertService.FireAsync("Confirmaci n", "Se te ha enviado un correo electr nico con las instrucciones para activar tu usuario.", SweetAlertIcon.Info);
            NavigationManager.NavigateTo("/");
        }
    }
}
