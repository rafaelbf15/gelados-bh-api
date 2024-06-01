using GeladosBH.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeladosBH.Data.Mappings
{
    public class ProdutoMapping : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Nome)
               .HasColumnType("varchar(500)");

            builder.Property(p => p.Descricao)
                .HasColumnType("varchar(500)");

            builder.Property(p => p.Valor)
               .HasColumnType("decimal(18,2)");

            builder.ToTable("produtos");
        }
    }
}
