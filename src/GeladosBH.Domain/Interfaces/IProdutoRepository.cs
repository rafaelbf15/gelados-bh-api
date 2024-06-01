using GeladosBH.Core.DataObjects;
using GeladosBH.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GeladosBH.Domain.Interfaces
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        Task<IEnumerable<Produto>> ObterProdutos();
        Task<Produto> ObterProdutoPorId(Guid id);

        void AdicionarProduto(Produto produto);
        void AdicionarProdutos(IEnumerable<Produto> produtos);

        void AtualizarProduto(Produto produto);
        void AtualizarProdutos(IEnumerable<Produto> produtos);

        void RemoverProduto(Produto produto);
        void RemoverProdutos(IEnumerable<Produto> produtos);
    }
}
