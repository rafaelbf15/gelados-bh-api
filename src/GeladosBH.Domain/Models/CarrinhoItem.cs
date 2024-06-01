using GeladosBH.Core.DomainObjects;
using System;

namespace GeladosBH.Domain.Models
{
    public class CarrinhoItem : Entity
    {
        public Guid CarrinhoId { get; private set; }
        public Guid ProdutoId { get; private set; }
        public int QuantidadeRetiradaVenda { get; set; }
        public int QuantidadeVendida { get; set; }
        public DateTime DataVenda { get; set; }

        //EF Rel
        public Carrinho Carrinho { get; set; }

        public Produto Produto { get; set; }

        public CarrinhoItem(Guid carrinhoId, Guid produtoId, int quantidadeRetiradaVenda)
        {
            CarrinhoId = carrinhoId;
            ProdutoId = produtoId;
            QuantidadeRetiradaVenda = quantidadeRetiradaVenda;
        }

        protected CarrinhoItem() { }

    }
}
