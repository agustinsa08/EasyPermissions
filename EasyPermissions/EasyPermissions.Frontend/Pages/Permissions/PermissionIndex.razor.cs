using CurrieTechnologies.Razor.SweetAlert2;
using EasyPermissions.Frontend.Repositories;
using EasyPermissions.Shared.Entities;
using Microsoft.AspNetCore.Components;
using System.Net;
using Blazored.Modal.Services;
using Blazored.Modal;
using Microsoft.AspNetCore.Authorization;
using EasyPermissions.Shared.Enums;
using System.ComponentModel;
using MudBlazor.Extensions;
using Microsoft.AspNetCore.Components.Authorization;

namespace EasyPermissions.Frontend.Pages.Permissions
{
    [Authorize(Roles = "Admin, Leader, User")]
    public partial class PermissionIndex
    {
        private int currentPage = 1;
        private int totalPages;
        private string userId;

        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;

        [Parameter, SupplyParameterFromQuery] public string Page { get; set; } = string.Empty;
        [Parameter, SupplyParameterFromQuery] public string Filter { get; set; } = string.Empty;
        [Parameter, SupplyParameterFromQuery] public int RecordsNumber { get; set; } = 10;
        [CascadingParameter] IModalService Modal { get; set; } = default!;


        public List<Permission>? Permissions { get; set; }

        [CascadingParameter] private Task<AuthenticationState> AuthenticationStateTask { get; set; } = null!;

        protected override async Task OnInitializedAsync()
        {
            await LoadAsync();
           await LoadUserAsyc();
        }

        private async Task LoadUserAsyc()
        {
            var responseHttp = await Repository.GetAsync<User>($"/api/accounts");
            if (responseHttp.Error)
            {
                if (responseHttp.HttpResponseMessage.StatusCode == HttpStatusCode.NotFound)
                {
                    NavigationManager.NavigateTo("/");
                    return;
                }
                var messageError = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", messageError, SweetAlertIcon.Error);
                return;
            }

            if(responseHttp.Response is null) {
                return;
            }

            userId = responseHttp.Response.Id;

        }

        private async Task ShowModalAsync(int id = 0, bool isEdit = false)
        {
            IModalReference modalReference;

            if (isEdit)
            {
                modalReference = Modal.Show<PermissionEdit>(string.Empty, new ModalParameters().Add("Id", id));
            }
            else
            {
                modalReference = Modal.Show<PermissionCreate>();
            }

            var result = await modalReference.Result;
            await LoadAsync();
        }
        private async Task FilterCallBack(string filter)
        {
            Filter = filter;
            await ApplyFilterAsync();
            StateHasChanged();
        }


        private async Task SelectedPageAsync(int page)
        {
            await LoadAsync(page);
        }

        private async Task SelectedRecordsNumberAsync(int recordsnumber)
        {
            RecordsNumber = recordsnumber;
            int page = 1;
            await LoadAsync(page);
            await SelectedPageAsync(page);
        }

        private async Task LoadAsync(int page = 1)
        {
            currentPage = page;
            Console.WriteLine("Lo ejecuteeeeee");
            if (!string.IsNullOrWhiteSpace(Page))
            {
                page = Convert.ToInt32(Page);
            }

            var ok = await LoadListAsync(page);
            if (ok)
            {
                await LoadPagesAsync();
            }
        }
        private void ValidateRecordsNumber()
        {
            if (RecordsNumber == 0)
            {
                RecordsNumber = 10;
            }
        }
        private async Task<bool> LoadListAsync(int page)
        {

            ValidateRecordsNumber();
            Console.WriteLine("------------------ frontera-------------");
            var url = $"api/permissions?page={page}&recordsnumber={RecordsNumber}";
            if (!string.IsNullOrEmpty(Filter))
            {
                url += $"&filter={Filter}";
            }

            var responseHttp = await Repository.GetAsync<List<Permission>>(url);
            Console.WriteLine("Lo ejecuteeeeee malllll");

            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return false;
            }
            Console.WriteLine("Lo ejecuteeeeee biennnn");

            Permissions = responseHttp.Response;
            return true;
        }

        private async Task LoadPagesAsync()
        {
            var url = $"api/permissions/totalPages?recordsnumber={RecordsNumber}";
            if (!string.IsNullOrEmpty(Filter))
            {
                url += $"&filter={Filter}";
            }

            var responseHttp = await Repository.GetAsync<int>(url);
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return;
            }
            totalPages = responseHttp.Response;
        }

        private async Task ApplyFilterAsync()
        {
            int page = 1;
            await LoadAsync(page);
            await SelectedPageAsync(page);
        }

        private async Task DeleteAsycn(Permission permission)
        {
            var result = await SweetAlertService.FireAsync(new SweetAlertOptions
            {
                Title = "Confirmación",
                Text = $"¿Estas seguro de querer borrar el permiso: {permission.Id}?",
                Icon = SweetAlertIcon.Question,
                ShowCancelButton = true,
            });
            var confirm = string.IsNullOrEmpty(result.Value);
            if (confirm)
            {
                return;
            }

            var responseHttp = await Repository.DeleteAsync<Permission>($"api/permissions/{permission.Id}");
            if (responseHttp.Error)
            {
                if (responseHttp.HttpResponseMessage.StatusCode == HttpStatusCode.NotFound)
                {
                    NavigationManager.NavigateTo("/permissions");
                }
                else
                {
                    var mensajeError = await responseHttp.GetErrorMessageAsync();
                    await SweetAlertService.FireAsync("Error", mensajeError, SweetAlertIcon.Error);
                }
                return;
            }

            await LoadAsync();
            var toast = SweetAlertService.Mixin(new SweetAlertOptions
            {
                Toast = true,
                Position = SweetAlertPosition.BottomEnd,
                ShowConfirmButton = true,
                Timer = 3000
            });
            await toast.FireAsync(icon: SweetAlertIcon.Success, message: "Registro borrado con éxito.");
        }

        private void ShowDetail(int permissionId)
        {

            NavigationManager.NavigateTo($"/permissions/details/{permissionId}");
        }

        private void ShowPermissionsByUser()
        {
            Console.WriteLine($"------{userId}");
            NavigationManager.NavigateTo($"/permissions/{userId}");
        }


        private static string GetDescription(PermissionStatus value)
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