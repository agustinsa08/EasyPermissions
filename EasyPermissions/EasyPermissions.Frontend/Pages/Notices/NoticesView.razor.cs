using Blazored.Modal;
using Blazored.Modal.Services;
using CurrieTechnologies.Razor.SweetAlert2;
using EasyPermissions.Frontend.Repositories;
using EasyPermissions.Shared.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using System.Net;

namespace EasyPermissions.Frontend.Pages.Notices
{
    public partial class NoticesView
    {
        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;

        [Parameter] public int noticeId { get; set; }

        private Notice? notice { get; set; }
        private string RawHtmlContent = "";

        protected override async Task OnInitializedAsync()
        {
            await LoadNoticeAsync();
        }

        private MarkupString SanitizeHtml(string html)
        {
            // Elimina las etiquetas <script> y <style>
            html = System.Text.RegularExpressions.Regex.Replace(html, @"<script[^>]*>.*?</script>", string.Empty, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            html = System.Text.RegularExpressions.Regex.Replace(html, @"<style[^>]*>.*?</style>", string.Empty, System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            html = html.Replace("\n", "<br />");

            // Puedes agregar más reglas para eliminar otras etiquetas o atributos peligrosos
            return new MarkupString(html);
        }

        private void ReturnHome()
        {
            NavigationManager.NavigateTo($"/");
        }

        private async Task LoadNoticeAsync()
        {
            var url = $"api/Notices/{noticeId}";

            var responseHttp = await Repository.GetAsync<Notice>(url);
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
            }
            notice = responseHttp.Response;
        }

       
    }
}