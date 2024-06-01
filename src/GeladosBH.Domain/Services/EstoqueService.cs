using GeladosBH.Core.Services;
using GeladosBH.Domain.Interfaces;
using GeladosBH.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GeladosBH.Domain.Services
{
    public class EstoqueService : BaseService, IEstoqueService
    {
        private readonly IProdutoRepository _produtoRepository;

        public EstoqueService(IProdutoRepository produtoRepository, INotificadorService notificadorService) : base(notificadorService)
        {
            _produtoRepository = produtoRepository;
        }
        public async Task<bool> DebitarEstoque(Guid produtoId, int quantidade)
        {
            if (!await DebitarItemEstoque(produtoId, quantidade)) return false;

            return await _produtoRepository.UnitOfWork.Commit();
        }

        private async Task<bool> DebitarItemEstoque(Guid produtoId, int quantidade)
        {
            var produto = await _produtoRepository.ObterProdutoPorId(produtoId);

            if (produto == null) return false;

            if (!produto.PossuiEstoque(quantidade))
            {
                Notificar($"Produto - {produto.Nome} sem estoque");
                return false;
            }

            produto.DebitarEstoque(quantidade);

            _produtoRepository.AtualizarProduto(produto);
            return true;
        }

        public async Task<bool> ReporEstoque(Guid produtoId, int quantidade)
        {
            var sucesso = await ReporItemEstoque(produtoId, quantidade);

            if (!sucesso) return false;

            return await _produtoRepository.UnitOfWork.Commit();
        }

        private async Task<bool> ReporItemEstoque(Guid produtoId, int quantidade)
        {
            var produto = await _produtoRepository.ObterProdutoPorId(produtoId);

            if (produto == null) return false;
            produto.ReporEstoque(quantidade);

            _produtoRepository.AtualizarProduto(produto);

            return true;
        }
        public void Dispose()
        {
            _produtoRepository.Dispose();
        }

        public async Task<bool> ReporEstoque(List<ProdutoEstoqueRepo> produtoEstoqueRepos)
        {
            foreach (var item in produtoEstoqueRepos)
            {
                var sucesso = await ReporItemEstoque(item.ProdutoId, item.Quantidade);

                if (!sucesso) return false;
            }

            return await _produtoRepository.UnitOfWork.Commit();
        }
    }
}
