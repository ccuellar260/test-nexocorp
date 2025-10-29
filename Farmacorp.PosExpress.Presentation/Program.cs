
using Farmacorp.PosExpress.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

Console.WriteLine("HOLA MUNDO !!!!!!!");

var builder = Host.CreateApplicationBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

Console.WriteLine($"connectionString: {connectionString}");


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString)
);


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    try
    {
        // conectar a la base de datos
        await db.Database.CanConnectAsync();
        Console.WriteLine("Base de datos conectada correctamente.");

    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error al conectar la db: {ex.Message}");
        Console.WriteLine($"StackTrace: {ex.StackTrace}");
    }
}