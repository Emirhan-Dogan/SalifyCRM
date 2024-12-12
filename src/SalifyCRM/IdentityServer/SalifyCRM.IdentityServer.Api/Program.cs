using Common.Middleware;
using Core.Utilities.Security.Hashing;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SalifyCRM.IdentityServer.Application;
using SalifyCRM.IdentityServer.Domain;
using SalifyCRM.IdentityServer.Persistence.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Ortamları doğru şekilde ayarlayalım
ConfigureServices(builder);

// Uygulama yapılandırmasını tamamlayalım
var app = builder.Build();
ConfigureMiddleware(app);

app.Run();

// -----------------------------
// Konfigürasyon Yöntemleri
// -----------------------------

void ConfigureServices(WebApplicationBuilder builder)
{

    // TokenOptions'ı appsettings.json'dan yükleyelim
    builder.Services.Configure<TokenOptions>(builder.Configuration.GetSection("TokenOptions"));
    builder.Services.AddScoped<ITokenHelper, JwtHelper>();

    // Controller'ları ekleyelim
    builder.Services.AddControllers();

    // Swagger/OpenAPI desteğini ekleyelim
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    // Persistence katmanını ekleyelim
    builder.Services.AddPersistenceServices(builder.Configuration);

    // MediatR'ı ekleyelim
    builder.Services.AddMediatR(Assembly.Load("SalifyCRM.IdentityServer.Application"));

}

void ConfigureMiddleware(WebApplication app)
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.UseMiddleware<TokenValidationMiddleware>();
    app.UseAuthorization();
    app.MapControllers();
}
