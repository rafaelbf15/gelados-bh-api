using GeladosBH.Core.DataObjects;
using GeladosBH.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GeladosBH.Domain.Interfaces
{
    public interface ICarrinhoRepository : IRepository<Carrinho>
    {
        Task<IEnumerable<Carrinho>> ObterCarrinhos();
        Task<IEnumerable<CarrinhoItem>> ObterCarrinhoItensPorCarrinhoId(Guid carrinhoId);
        Task<Carrinho> ObterCarrinhoPorId(Guid id);
        Task<Carrinho> ObterCarrinhoPorColaboradorId(Guid colaboradorId);
        Task<CarrinhoItem> ObterCarrinhoItenPorId(Guid id);
        

        void AdicionarCarrinho(Carrinho carrinho);
        void AdicionarCarrinhoItem(CarrinhoItem carrinhoItem);
        void AdicionarCarrinhoItens(IEnumerable<CarrinhoItem> carrinhoItens);

        void AtualizarCarrinho(Carrinho carrinho);
        void AtualizarCarrinhoItens(IEnumerable<CarrinhoItem> carrinhoItens);

        void RemoverCarrinho(Carrinho carrinho);
        void RemoverCarrinhoItem(CarrinhoItem carrinhoIten);
    }
}
