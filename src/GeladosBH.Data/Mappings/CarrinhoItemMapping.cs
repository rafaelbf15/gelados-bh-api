using GeladosBH.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeladosBH.Data.Mappings
{
    public class CarrinhoItemMapping : IEntityTypeConfiguration<CarrinhoItem>
    {
        public void Configure(EntityTypeBuilder<CarrinhoItem> builder)
        {
            builder.HasKey(ci => ci.Id);


            builder.ToTable("carrinho_itens");
        }
    }
}
