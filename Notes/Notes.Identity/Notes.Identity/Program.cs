using Microsoft.Extensions.DependencyInjection;
using IdentityServer4.Models;
using IdentityServer4;

var builder = WebApplication.CreateBuilder(args);


//ConfigureServices
builder.Services.AddIdentityServer()
                .AddInMemoryApiResources(new List<ApiResource>())
                .AddInMemoryIdentityResources(new List<IdentityResource>())
                .AddInMemoryApiScopes(new List<ApiScope>())
                .AddInMemoryClients(new List<Client>())
                .AddDeveloperSigningCredential();

builder.Services.AddControllers();

// Configure
var app = builder.Build();

app.UseRouting();
app.UseIdentityServer();
app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
});

app.Run();
