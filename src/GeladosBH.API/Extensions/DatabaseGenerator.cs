using GeladosBH.Data;
using GeladosBH.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeladosBH.API.Extensions
{
    public class DataGenerator
    {
        public static void Generate(IServiceProvider serviceProvider)
        {
            using (var context = new GeladosBHContext(
                serviceProvider.GetRequiredService<DbContextOptions<GeladosBHContext>>()))
            {
                if (context.Produtos.Any())
                {
                    return; 
                }

                context.Produtos.AddRange(
                new Produto("Sorvete", "Sorvete de flocos 200g", true, (decimal)5.00, null, 200, 4),
                new Produto("Sorvete", "Sorvete de morango 200g", true, (decimal)7.60, null, 140, 4),
                new Produto("Sorvete", "Sorvete de chocolate 200g", true, (decimal)8.00, null, 127, 4),
                new Produto("Picolé", "Picolé de chocolate", true, (decimal)4.60, null, 351, 5),
                new Produto("Picolé", "Picolé de morango", true, (decimal)5.20, null, 402, 5),
                new Produto("Açai", "Açaí com frutas 200g", true, (decimal)4.10, null, 50, 3),
                new Produto("Picolé", "Picolé de limão com leite", true, (decimal)5.46, null, 274, 5));

                context.SaveChanges();
            }
        }
    }
}
