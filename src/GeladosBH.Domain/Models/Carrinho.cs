using GeladosBH.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeladosBH.Domain.Models
{
    public class Carrinho : Entity, IAggregateRoot
    {
        public Guid ColaboradorId { get; private set; }
        public DateTime DataCadastro { get; private set; }

        public CarrinhoStatus CarrinhoStatus { get; private set; }

        public IEnumerable<CarrinhoItem> CarrinhoItens { get; set; }

        public Carrinho(Guid colaboradorId)
        {
            ColaboradorId = colaboradorId;
        }

        protected Carrinho() { }

        public void Abastecendo()
        {
            CarrinhoStatus = CarrinhoStatus.Abastecimento;
        }

        public void Distribuindo()
        {
            CarrinhoStatus = CarrinhoStatus.Distribuicao;
        }

        public void FinalizarCarrinho()
        {
            CarrinhoStatus = CarrinhoStatus.Finalizado;
        }
    }
}
