using GeladosBH.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GeladosBH.Domain.Interfaces
{
    public interface IProdutoService : IDisposable
    {
        Task<bool> AdicionarProduto(Produto produto);
        Task<bool> AtualizarProduto(Produto produto);
        Task<bool> RemoverProduto(Produto produto);
        Task<bool> DebitarEstoque(Guid id, int quantidade);
        Task<bool> ReporEstoque(Guid id, int quantidade);
        Task<bool> ReporEstoque(List<ProdutoEstoqueRepo> produtoEstoqueRepos);
    }
}
