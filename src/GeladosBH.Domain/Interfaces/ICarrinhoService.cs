using GeladosBH.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GeladosBH.Domain.Interfaces
{
    public interface ICarrinhoService : IDisposable
    {
        Task<bool> AdicionarCarrinho(Carrinho carrinho);
        Task<bool> AdicionarCarrinhoItem(CarrinhoItem carrinhoItem);
        Task<bool> AtualizarCarrinho(Carrinho carrinho);
        Task<bool> RemoverCarrinho(Carrinho carrinho);
        Task<bool> RemoverCarrinhoItem(CarrinhoItem carrinhoItem);
        Task<bool> DespacharCarrinho(Carrinho carrinho);
        Task<bool> FinalizarCarrinho(Carrinho carrinho);

    }
}
