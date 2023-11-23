var builder = WebApplication.CreateBuilder(args);
builder.Services.AddIdentityServer()
     .AddInMemoryClients(Config.Clients)
    .AddInMemoryApiScopes(Config.ApiScopes)
   .AddDeveloperSigningCredential();
var app = builder.Build();


// HttpRequest_PipeLine 
app.MapGet("/", () => "Hello World!");
app.UseIdentityServer();
app.Run();
