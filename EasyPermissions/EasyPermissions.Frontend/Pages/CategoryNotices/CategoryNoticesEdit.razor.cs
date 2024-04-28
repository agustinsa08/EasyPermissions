using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using EasyPermissions.Frontend.Repositories;
using EasyPermissions.Frontend.Shared;
using EasyPermissions.Shared.Entities;
using System.Net;

namespace EasyPermissions.Frontend.Pages.CategoryNotices
{
    public partial class CategoryNoticesEdit
    {
        private CategoryNotice? categoryNotices;
        private FormWithName<CategoryNotice>? categoryNoticesForm;

        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;

        [Parameter] public int CategoryNoticesId { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            var responseHttp = await Repository.GetAsync<CategoryNotice>($"/api/CategoryNotices/{CategoryNoticesId}");
            if (responseHttp.Error)
            {
                if (responseHttp.HttpResponseMessage.StatusCode == HttpStatusCode.NotFound)
                {
                    Return();
                }
                var message = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return;
            }
            categoryNotices = responseHttp.Response;
        }

        private async Task SaveAsync()
        {
            var response = await Repository.PutAsync($"/api/CategoryNotices", categoryNotices);
            if (response.Error)
            {
                var message = await response.GetErrorMessageAsync();
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
            await toast.FireAsync(icon: SweetAlertIcon.Success, message: "Cambios guardados con éxito.");
        }

        private void Return()
        {
            categoryNoticesForm!.FormPostedSuccessfully = true;
            NavigationManager.NavigateTo($"/typeNotices/details/{categoryNotices!.TypeNoticeId}");
        }
    }
}
