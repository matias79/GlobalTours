using Core.Interfaces;
using Infraestructura.Datos;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectionString = builder.Configuration.GetConnectionString("connectiondefaul");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseMySql(connectionString,
                    ServerVersion.AutoDetect(connectionString)));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ILugarRepositorio, LugarRepositorio>(); //EL REPOSITORIO Y SU CLASE

var app = builder.Build();

//aplicar nuevas migraciones al ejecutar la aplicacion y alimentar la base de datos
using(var scope = app.Services.CreateAsyncScope())
{
    var services = scope.ServiceProvider;
    var loggerFactory = services.GetRequiredService<ILoggerFactory>();
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        await context.Database.MigrateAsync();
        await BaseDatosSeed.SeedAsync(context, loggerFactory);
    }
    catch (System.Exception ex)
    {
        var logger = loggerFactory.CreateLogger<Program>(); 
        logger.LogError(ex, "error durante la migracion");
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
