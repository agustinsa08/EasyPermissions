﻿using CurrieTechnologies.Razor.SweetAlert2;
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

namespace EasyPermissions.Frontend.Pages.Permissions
{
    [Authorize(Roles = "Admin, Leader, User")]
    public partial class PermissionDetails
    {
        private PermissionDTO PermissionDTO = new();
        private Permission permission = null!;
        private List<PermissionDetail> permissionDetails = null!;
        private List<PermissionStatus>? permissionStatus = Enum.GetValues(typeof(PermissionStatus)).Cast<PermissionStatus>().ToList();

        [Parameter] public int permissionId { get; set; }

        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;




        protected override void OnInitialized()
        {
        }


        protected override async Task OnParametersSetAsync()
        {
            await GetPermissionAsync();
            await GetPermissionDetailsAsync();
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

            var responseHttp = await Repository.PutAsync($"/api/permissions", PermissionDTO);
            if (responseHttp.Error)
            {
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

    }

}