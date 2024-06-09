using CurrieTechnologies.Razor.SweetAlert2;
using EasyPermissions.Frontend.Repositories;
using EasyPermissions.Shared.DTOs;
using EasyPermissions.Shared.Entities;
using EasyPermissions.Shared.Enums;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;

namespace EasyPermissions.Frontend.Pages.Permissions
{
    public partial class PermissionForm
    {

        private List<TypePermission>? typePermissions;
        private List<CategoryPermission>? categoryPermissions;

        private EditContext editContext = null!;

        [EditorRequired, Parameter] public PermissionDTO PermissionDTO { get; set; } = null!;
        [EditorRequired, Parameter] public EventCallback OnValidSubmit { get; set; }
        [EditorRequired, Parameter] public EventCallback ReturnAction { get; set; }
        [Inject] public SweetAlertService SweetAlertService { get; set; } = null!;
        [Inject] private IRepository Repository { get; set; } = null!;

        public bool FormPostedSuccessfully { get; set; }


        protected override async Task OnInitializedAsync()
        {
            editContext = new(PermissionDTO);
            await LoadTypePermissionsAsync();
        }

        private void printAction()
        {
        }

        private async Task OnBeforeInternalNavigation(LocationChangingContext context)
        {
            var formWasEdited = editContext.IsModified();
            if (!formWasEdited || FormPostedSuccessfully)
            {
                return;
            }

            var result = await SweetAlertService.FireAsync(new SweetAlertOptions
            {
                Title = "Confirmación",
                Text = "¿Deseas abandonar la página y perder los cambios?",
                Icon = SweetAlertIcon.Question,
                ShowCancelButton = true,
            });
            var confirm = !string.IsNullOrEmpty(result.Value);
            if (confirm)
            {
                return;
            }

            context.PreventNavigation();
        }

        private async Task LoadTypePermissionsAsync()
        {
            var responseHttp = await Repository.GetAsync<List<TypePermission>>("/api/TypePermissions");
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return;
            }

            typePermissions = responseHttp.Response;
        }

        private async Task TypePermissionChangedAsync(ChangeEventArgs e)
        {
            var selectedTypePermission = Convert.ToInt32(e.Value!);
            categoryPermissions = null;
            PermissionDTO.CategoryPermissionId = 0;
            await LoadCategoryPermissionsAsync(selectedTypePermission);
        }

        private async Task LoadCategoryPermissionsAsync(int typePermissionId)
        {
            var responseHttp = await Repository.GetAsync<List<CategoryPermission>>($"/api/CategoryPermissions/combo/{typePermissionId}");
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return;
            }

            categoryPermissions = responseHttp.Response;
        }
    }
}

