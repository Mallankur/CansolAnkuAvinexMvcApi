using CansolveANK.AnkurLibservises;
using Microsoft.OpenApi.Models;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson.Serialization;
using MongoDB.Bson;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
BsonSerializer.RegisterSerializer(new Decimal128Serializer());
BsonSerializer.RegisterSerializer(new NullableSerializer<Decimal128>(new Decimal128Serializer()));

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null; 
    });
 //Genrate the token 
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(
    options =>
    { 
       options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
       ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:key"]))



      };

}
    );

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<ICan, AnkurServises>();
builder.Services.Configure<MongoSocket>(builder.Configuration.GetSection("MongoObject"));
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "CansolvDataAPI", Version = "v1@ank" });
    // Configure XML comments for your controllers and models here
    // c.IncludeXmlComments("your-xml-comments-file.xml");
});
builder.Services.AddMvc().AddXmlSerializerFormatters();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CansolvAnkur"));
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
