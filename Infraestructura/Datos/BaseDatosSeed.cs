using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Entidades;
using Microsoft.Extensions.Logging;

namespace Infraestructura.Datos
{
    public class BaseDatosSeed
    {
        public static async Task SeedAsync(ApplicationDbContext context, ILoggerFactory loggerFactory)
        {
            try
            { //se pregunta si nuestra tabla tiene datos
                if(!context.Pais.Any()){
                    var paisData = File.ReadAllText("../Infraestructura/Datos/SeedData/paises.json");
                    var paises = JsonSerializer.Deserialize<List<Pais>>(paisData);

                    foreach (var item in paises)
                    {
                        await context.Pais.AddAsync(item);
                    }
                    await context.SaveChangesAsync();
                }

                if(!context.Categoria.Any()){
                    var categoriaData = File.ReadAllText("../Infraestructura/Datos/SeedData/categorias.json");
                    var categorias = JsonSerializer.Deserialize<List<Categoria>>(categoriaData);

                    foreach (var item in categorias)
                    {
                        await context.Categoria.AddAsync(item);
                    }
                    await context.SaveChangesAsync();
                }

                if(!context.lugar.Any()){
                    var lugaresData = File.ReadAllText("../Infraestructura/Datos/SeedData/lugares.json");
                    var lugares = JsonSerializer.Deserialize<List<Lugar>>(lugaresData);

                    foreach (var item in lugares)
                    {
                        await context.lugar.AddAsync(item);
                    }
                    await context.SaveChangesAsync();
                }
            }
            catch (System.Exception ex)
            {
                var logger = loggerFactory.CreateLogger<BaseDatosSeed>();
                logger.LogError(ex.Message);
            }
        }
    }
}