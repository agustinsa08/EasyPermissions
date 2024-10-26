using CurrieTechnologies.Razor.SweetAlert2;
using EasyPermissions.Frontend.Repositories;
using EasyPermissions.Shared.Entities;
using Microsoft.AspNetCore.Components;
using System.Net;
using Blazored.Modal.Services;
using Blazored.Modal;
using Microsoft.AspNetCore.Authorization;
using EasyPermissions.Shared.DTOs;
using EasyPermissions.Shared.Enums;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Authorization;
using System.Text.Json;
using System.ComponentModel;

namespace EasyPermissions.Frontend.Pages.Permissions
{
    [Authorize(Roles = "Admin, Leader, User")]
    public partial class PermissionDetails
    {

        private string? photoUser;

        private PermissionDTO PermissionDTO = new();
        private Permission permission = null!;
        private List<PermissionDetail> permissionDetails = null!;
        private List<PermissionStatus>? permissionStatus = Enum.GetValues(typeof(PermissionStatus)).Cast<PermissionStatus>().ToList();



        [Parameter] public int permissionId { get; set; }

        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;

        [CascadingParameter] private Task<AuthenticationState> AuthenticationStateTask { get; set; } = null!;



        protected override void OnInitialized()
        {
        }


        protected override async Task OnParametersSetAsync()
        {
            await GetPermissionAsync();
            await GetPermissionDetailsAsync();
            await GetPhotoUserAsync();
        }

        private async Task GetPermissionAsync()
        {
            var responseHttp = await Repository.GetAsync<Permission>($"/api/permissions/{permissionId}");
            if (responseHttp.Error)
            {
                if (responseHttp.HttpResponseMessage.StatusCode == HttpStatusCode.NotFound)
                {
                    NavigationManager.NavigateTo("/Permissions");
                }
                else
                {
                    var messsage = await responseHttp.GetErrorMessageAsync();
                    await SweetAlertService.FireAsync("Error", messsage, SweetAlertIcon.Error);
                }
            }
            else
            {
                permission = responseHttp.Response!;
               // string jsonString = JsonSerializer.Serialize(permission);
               
            }
        }

        private async Task GetPermissionDetailsAsync()
        {
            var responseHttp = await Repository.GetAsync<List<PermissionDetail>>($"/api/permissionDetails/combo/{permissionId}");
            if (responseHttp.Error)
            {
                if (responseHttp.HttpResponseMessage.StatusCode == HttpStatusCode.NotFound)
                {
                    NavigationManager.NavigateTo("/Permissions");
                }
                else
                {
                    var messsage = await responseHttp.GetErrorMessageAsync();
                    await SweetAlertService.FireAsync("Error", messsage, SweetAlertIcon.Error);
                }
            }
            else
            {
                permissionDetails = responseHttp.Response!;
            }
        }

        private async Task StatusChangedAsync(ChangeEventArgs e)
        {

            var statusValue = Convert.ToInt32(e.Value!);

            PermissionDTO.Id = permission.Id;
            PermissionDTO.Status = (PermissionStatus)statusValue;
            PermissionDTO.Date = permission.Date;
            PermissionDTO.CategoryPermissionId = permission.CategoryPermissionId;
            PermissionDTO.Description = permission.Description;
            PermissionDTO.DateStatus = DateTime.UtcNow;

            var beforeStatus = permission.Status;

            permission.Status = (PermissionStatus)statusValue;

            Console.WriteLine(beforeStatus);

            var responseHttp = await Repository.PutAsync($"/api/permissions", PermissionDTO);
            if (responseHttp.Error)
            {
                permission.Status = beforeStatus;
                var message = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return;
            }

            await GetPermissionDetailsAsync();

            var toast = SweetAlertService.Mixin(new SweetAlertOptions
            {
                Toast = true,
                Position = SweetAlertPosition.BottomEnd,
                ShowConfirmButton = true,
                Timer = 3000
            });


            await toast.FireAsync(icon: SweetAlertIcon.Success, message: "Cambios guardados con éxito.");

        }

        protected async Task GetPhotoUserAsync()
        {
            var authenticationState = await AuthenticationStateTask;
            var claims = authenticationState.User.Claims.ToList();
            var photoClaim = claims.FirstOrDefault(x => x.Type == "Photo");
            if (photoClaim is not null)
            {
                photoUser = photoClaim.Value;
            }
        }

        private void ReturnPermissions()
        {
            NavigationManager.NavigateTo($"/permissions");
        }

        public static string GetDescription(PermissionStatus value)
        {
            var fieldInfo = value.GetType().GetField(value.ToString());
            var attributes = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

            return attributes.Length > 0 ? attributes[0].Description : value.ToString();
        }

        private DateTime? ApplyUtc(DateTime date)
        {

            if (DateTime.MinValue == date)
            {
                return null;
            }

            DateTime newDate;

            if (DateTime.TryParse(date.ToString() + 'Z', out newDate))
            {
                return newDate;
            }

            return null;

        }

    }

}