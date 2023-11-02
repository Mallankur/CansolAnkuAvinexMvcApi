using IdentityServer4.Models;
using IDSERVER;
using IDSERVER.iDCONFIG;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);
//builder.Services
//    .AddIdentity<YourUser, IdentityRole>()
//    .AddEntityFrameworkStores<YourDbContext>()
//    .AddDefaultTokenProviders();

builder.Services
            .AddIdentityServer()
            .AddInMemoryClients(IDCLIENT.Get())


            .AddInMemoryIdentityResources(IDSERVER.Resources.GetIdentityResources())
            .AddInMemoryApiResources(IDSERVER.Resources.GetApiResources())
            .AddInMemoryApiScopes(Scopes.GetApiScopes())
            .AddTestUsers(Useres.Get())
            .AddDeveloperSigningCredential();
            

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseStaticFiles();

app.UseRouting();

app.UseIdentityServer();

//app.UseEndpoints(endpoints => endpoints.MapDefaultControllerRoute());

//app.MapGet("/", () => "Hello World!");

app.Run();
