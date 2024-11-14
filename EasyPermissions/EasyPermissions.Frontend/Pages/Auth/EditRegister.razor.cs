using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using EasyPermissions.Frontend.Repositories;
using EasyPermissions.Frontend.Services;
using EasyPermissions.Shared.DTOs;
using EasyPermissions.Shared.Entities;
using EasyPermissions.Shared.Enums;
using System.ComponentModel;
using System.Net;

namespace EasyPermissions.Frontend.Pages.Auth
{
    public partial class EditRegister
    {
        [Parameter] public string userId { get; set; }

        private User? user;
        private List<Country>? countries;
        private List<State>? states;
        private List<City>? cities;
        private List<Area>? areas;
        private bool loading;
        private string? imageUrl;
        private List<UserType>? userTypes = Enum.GetValues(typeof(UserType)).Cast<UserType>().ToList();

        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;
        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] private ILoginService LoginService { get; set; } = null!;


        protected override async Task OnInitializedAsync()
        {
            await LoadUserAsyc(userId);
            await LoadCountriesAsync();
            await LoadStatesAsyn(user!.City!.State!.Country!.Id);
            await LoadCitiesAsyn(user!.City!.State!.Id);

            if (!string.IsNullOrEmpty(user!.Photo))
            {
                imageUrl = user.Photo;
                user.Photo = null;
            }

            if (user is null) return;

        }

       

        private async Task LoadUserAsyc(string id)
        {
            var responseHttp = await Repository.GetAsync<User>($"/api/Users/getDetail/{id}");
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
            user = responseHttp.Response;
        }

        private void ImageSelected(string imagenBase64)
        {
            user.Photo = imagenBase64;
            imageUrl = null;
        }

        private async Task LoadCountriesAsync()
        {
            var responseHttp = await Repository.GetAsync<List<Country>>("/api/countries/combo");
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return;
            }

            countries = responseHttp.Response;
        }

        private async Task CountryChangedAsync(ChangeEventArgs e)
        {
            var selectedCountry = Convert.ToInt32(e.Value!);
            states = null;
            cities = null;
            user.CityId = 0;
            await LoadStatesAsyn(selectedCountry);
        }

        private async Task LoadStatesAsyn(int countryId)
        {
            var responseHttp = await Repository.GetAsync<List<State>>($"/api/states/combo/{countryId}");
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return;
            }

            states = responseHttp.Response;
        }

        private async Task StateChangedAsync(ChangeEventArgs e)
        {
            var selectedState = Convert.ToInt32(e.Value!);
            cities = null;
            user.CityId = 0;
            await LoadCitiesAsyn(selectedState);
        }

        private async Task LoadCitiesAsyn(int stateId)
        {
            var responseHttp = await Repository.GetAsync<List<City>>($"/api/cities/combo/{stateId}");
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return;
            }

            cities = responseHttp.Response;
        }
        private async Task LoadAreasAsync()
        {
            var responseHttp = await Repository.GetAsync<List<Area>>($"/api/areas/full");
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return;
            }

            areas = responseHttp.Response;
        }


        private async Task SaveUserAsync()
        {
            var responseHttp = await Repository.PutAsync<User, TokenDTO>($"/api/Users/{userId}", user!);
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return;
            }

            await LoginService.LoginAsync(responseHttp.Response!.Token);
            ReturnIndex();
        }

        private void ReturnIndex()
        {
            NavigationManager.NavigateTo($"/users");
        }

        private static string GetDescription(UserType value)
        {
            var fieldInfo = value.GetType().GetField(value.ToString());
            var attributes = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

            return attributes.Length > 0 ? attributes[0].Description : value.ToString();
        }

        private bool disableAreaField()
        {
            return user.UserType <= 0;
        }


        private async Task ChangeTypeAsync(ChangeEventArgs e)
        {

            var index = Convert.ToInt32(e.Value!);

            user.UserType = (UserType)index;

            if (index == 1)
            {
                await LoadAvailableAreasAsync();
                return;
            }

            await LoadAreasAsync();

        }

        private void OnChangeArea(ChangeEventArgs e)
        {
            if (e.Value is null)
            {
                user.AreaId = 0;
                return;
            };

            var areaId = Convert.ToInt32(e.Value!);
            user.AreaId = areaId;

        }

        private async Task LoadAvailableAreasAsync()
        {
            var responseHttp = await Repository.GetAsync<List<Area>>($"/api/Areas/withoutLeader");
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return;
            }

            areas = responseHttp.Response;
        }



    }
}