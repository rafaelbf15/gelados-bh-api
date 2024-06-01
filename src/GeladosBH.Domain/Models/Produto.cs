using GeladosBH.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeladosBH.Domain.Models
{
    public class Produto : Entity, IAggregateRoot
    {
        public string Nome { get; private set; }
        public string Descricao { get; private set; }
        public bool Ativo { get; private set; }
        public decimal Valor { get; private set; }
        public DateTime DataCadastro { get; private set; }
        public string Imagem { get; private set; }
        public int QuantidadeEstoque { get; private set; }
        public decimal ValidadeForaDoFreezer { get; private set; }

        public Produto(string nome, 
                       string descricao, 
                       bool ativo, 
                       decimal valor,
                       string imagem, 
                       int quantidadeEstoque, 
                       decimal validadeForaDoFreezer)
        {
            Nome = nome;
            Descricao = descricao;
            Ativo = ativo;
            Valor = valor;
            Imagem = imagem;
            QuantidadeEstoque = quantidadeEstoque;
            ValidadeForaDoFreezer = validadeForaDoFreezer;
            DataCadastro = DateTime.UtcNow;
        }

        protected Produto() { }

        public void Ativar() => Ativo = true;

        public void Desativar() => Ativo = false;

        public void AlterarDescricao(string descricao)
        {
            Descricao = descricao;
        }

        public void DebitarEstoque(int quantidade)
        {
            if (quantidade < 0) quantidade *= -1;
            QuantidadeEstoque -= quantidade;
        }

        public void ReporEstoque(int quantidade)
        {
            QuantidadeEstoque += quantidade;
        }

        public bool PossuiEstoque(int quantidade)
        {
            return QuantidadeEstoque >= quantidade;
        }
    }
}
