
using Farmacorp.PosExpress.Application.Services;
using Farmacorp.PosExpress.Domain.Interfaces;
using Farmacorp.PosExpress.Infrastructure.Data;
using Farmacorp.PosExpress.Presentation.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

Console.WriteLine("HOLA MUNDO !!!!!!!");

var builder = Host.CreateApplicationBuilder(args);

//por si no reconoce el appsettings.json, agregar manualmente la ruta base
builder.Environment.ContentRootPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) ?? Directory.GetCurrentDirectory();
builder.Configuration
   .SetBasePath(builder.Environment.ContentRootPath)
   .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
   .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true);
//end de agregar manualmente la ruta base


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

Console.WriteLine($"connectionString: {connectionString}");


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString)
);


builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

//registrar servicios 
builder.Services.AddScoped<ProductoService>();

// Registrar la aplicación principal
builder.Services.AddScoped<MainController>();


var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    try
    {
        // Intenta conectar a la base de datos
        await db.Database.CanConnectAsync();
        Console.WriteLine("Base de datos conectada correctamente.");

        // Ejecutar la aplicación
        var aplicacion = scope.ServiceProvider.GetRequiredService<MainController>();
        await aplicacion.Run();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
        Console.WriteLine($"Stack Trace: {ex.StackTrace}");
    }
}