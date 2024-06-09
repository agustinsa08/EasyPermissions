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

namespace EasyPermissions.Frontend.Pages.Permissions
{
    [Authorize(Roles = "Admin, Leader, Collaborator")]
    public partial class PermissionDetails
    {
        [Parameter] public int permissionId { get; set; }
        private PermissionDTO PermissionDTO { get; set; } = new();

        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;


        private List<PermissionStatus>? permissionStatus = Enum.GetValues(typeof(PermissionStatus)).Cast<PermissionStatus>().ToList();


        protected override async Task OnInitializedAsync()
        {

        }


        protected override async Task OnParametersSetAsync()
        {
            var responseHttp = await Repository.GetAsync<PermissionDTO>($"/api/permissions/{permissionId}");
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
                PermissionDTO = responseHttp.Response!;

                definedPermissions(PermissionDTO.Status);

            }
        }

        private void definedPermissions(int permissionStatus)
        {
            Console.WriteLine(permissionStatus);

        }

    }

}