using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using EasyPermissions.Frontend;
using EasyPermissions.Frontend.Repositories;
using EasyPermissions.Frontend.AuthenticationProviders;
using Microsoft.AspNetCore.Components.Authorization;
using Blazored.Modal;
using EasyPermissions.Frontend.Services;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//var uriBack = "https://easypermissionsbackend.azurewebsites.net/";
var uriBack = "https://localhost:7262/";

builder.Services.AddSingleton(sp => new HttpClient { BaseAddress = new Uri(uriBack) });
builder.Services.AddScoped<IRepository, Repository>();
builder.Services.AddSweetAlert2();
builder.Services.AddAuthorizationCore();
builder.Services.AddBlazoredModal();
builder.Services.AddMudServices();

builder.Services.AddScoped<AuthenticationProviderJWT>();
builder.Services.AddScoped<AuthenticationStateProvider, AuthenticationProviderJWT>(x => x.GetRequiredService<AuthenticationProviderJWT>());
builder.Services.AddScoped<ILoginService, AuthenticationProviderJWT>(x => x.GetRequiredService<AuthenticationProviderJWT>());

await builder.Build().RunAsync();
