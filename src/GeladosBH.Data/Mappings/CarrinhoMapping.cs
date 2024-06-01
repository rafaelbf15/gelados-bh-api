using GeladosBH.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeladosBH.Data.Mappings
{
    public class CarrinhoMapping : IEntityTypeConfiguration<Carrinho>
    {
        public void Configure(EntityTypeBuilder<Carrinho> builder)
        {
            builder.HasKey(c => c.Id);

            builder.HasMany(c => c.CarrinhoItens)
                .WithOne(ci => ci.Carrinho)
                .HasForeignKey(ci => ci.CarrinhoId);


            builder.ToTable("carrinhos");
        }
    }
}
