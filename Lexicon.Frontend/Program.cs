using Lexicon.Frontend.Components;
using Lexicon.Frontend.Services;
using Lexicon.Frontend.ServicesImp;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Net.Http;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();


builder.Services.AddMvc();
builder.Services.AddHttpClient<IUnitOfWork, UnitOfWork> (client =>
{
    var apiBaseUrl = builder.Configuration["ApiBaseUrl"];

    if (string.IsNullOrEmpty(apiBaseUrl))
    {
        throw new InvalidOperationException("ApiBaseurl configuration is missing");
    }

    client.BaseAddress = new Uri(apiBaseUrl);
}).ConfigurePrimaryHttpMessageHandler(() =>
{
    return new HttpClientHandler
    {
        ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
    };
});


builder.Services.AddServerSideBlazor(options =>
{
    options.DetailedErrors = true;
});

//builder.Services.AddAuthentication(options =>
//{
//	options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//	options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//}).AddJwtBearer(options =>
//{
//	options.TokenValidationParameters = new TokenValidationParameters
//	{
//		ValidateIssuerSigningKey = true,
//		IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("l6WeAA9dzO7YCWEVYxYPBIwjQN4PZV6mnCLWbmB")),
//		ValidateIssuer = false,
//		ValidateAudience = false,
//	};

//    //options.TokenValidationParameters.RoleClaimType = "role";
//}); 

builder.Services.AddScoped<CustomAuthenticationStateProvider>();

builder.Services.AddAuthorization();
builder.Services.AddCascadingAuthenticationState();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
