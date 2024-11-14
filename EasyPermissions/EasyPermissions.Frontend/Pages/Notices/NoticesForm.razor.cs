using CurrieTechnologies.Razor.SweetAlert2;
using EasyPermissions.Frontend.Repositories;
using EasyPermissions.Shared.Entities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;

namespace EasyPermissions.Frontend.Pages.Notices
{
    public partial class NoticesForm
    {
        private EditContext editContext = null!;

        [EditorRequired, Parameter] public Notice notices { get; set; } = null!;
        [EditorRequired, Parameter] public EventCallback OnValidSubmit { get; set; }
        [EditorRequired, Parameter] public EventCallback ReturnAction { get; set; }
        [Inject] public SweetAlertService SweetAlertService { get; set; } = null!;
        [Inject] private IRepository Repository { get; set; } = null!;

        public bool FormPostedSuccessfully { get; set; }

        private List<TypeNotice>? types;

        private ICollection<CategoryNotice>? categories;

        private int? typeId;

        protected override void OnInitialized()
        {
            editContext = new(notices);
        }

        protected override async Task OnInitializedAsync()
        {
            await LoadTypesAsync();

            if (notices.CategoryNotice is null) return;

            await LoadCategoriesAsync(notices.CategoryNotice.TypeNoticeId);



        }

        private async Task LoadTypesAsync()
        {
            var responseHttp = await Repository.GetAsync<List<TypeNotice>>("/api/TypeNotices/full");
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return;
            }

            types = responseHttp.Response;
        }

        private async Task OnChangeTypeAsync (ChangeEventArgs e)
        {
            int value = Convert.ToInt32(e.Value!);
            typeId = value;
            notices.CategoryNoticeId = 0;
            await LoadCategoriesAsync(value);
        }

        private bool disabledCategory()
        {
            if(typeId is null || typeId == 0)
            {
                return true;

            }

            return false;
        }

        private async Task LoadCategoriesAsync(int typeId)
        {
            var responseHttp = await Repository.GetAsync<TypeNotice>($"/api/TypeNotices/{typeId}");
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return;
            }

            if (responseHttp.Response == null)
            {
                await SweetAlertService.FireAsync("Error", "No data received from the server.", SweetAlertIcon.Error);
                return;
            }

            categories = responseHttp.Response.CategoryNotices;
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
    }
}


