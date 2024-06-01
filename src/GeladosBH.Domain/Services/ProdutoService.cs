using GeladosBH.Core.Services;
using GeladosBH.Domain.Interfaces;
using GeladosBH.Domain.Models;
using GeladosBH.Domain.Models.Validations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GeladosBH.Domain.Services
{
    public class ProdutoService : BaseService, IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IEstoqueService _estoqueService;

        public ProdutoService(IProdutoRepository produtoRepository, 
                              IEstoqueService estoqueService, 
                              INotificadorService notificadorService) : base(notificadorService)
        {
            _produtoRepository = produtoRepository;
            _estoqueService = estoqueService;
        }

        public async Task<bool> AdicionarProduto(Produto produto)
        {

            if (!ExecutarValidacao(new ProdutoValidation(), produto)) return false;

            _produtoRepository.AdicionarProduto(produto);

            return await _produtoRepository.UnitOfWork.Commit();
        }

        public async Task<bool> AtualizarProduto(Produto produto)
        {
            if (!ExecutarValidacao(new ProdutoValidation(), produto)) return false;

            _produtoRepository.AtualizarProduto(produto);

            return await _produtoRepository.UnitOfWork.Commit();
        }

        public async Task<bool> DebitarEstoque(Guid id, int quantidade)
        {
            if (!await _estoqueService.DebitarEstoque(id, quantidade))
            {
                Notificar("Falha ao debitar estoque");
                return false;
            }

            return true;
        }

        public async Task<bool> ReporEstoque(Guid id, int quantidade)
        {
            if (!await _estoqueService.ReporEstoque(id, quantidade))
            {
                Notificar("Falha ao repor estoque");
                return false;
            }

            return true;
        }

        public async Task<bool> ReporEstoque(List<ProdutoEstoqueRepo> produtoEstoqueRepos)
        {
            if (!await _estoqueService.ReporEstoque(produtoEstoqueRepos))
            {
                Notificar("Falha ao repor estoque");
                return false;
            }

            return true;
        }

        public async Task<bool> RemoverProduto(Produto produto)
        {
            _produtoRepository.RemoverProduto(produto);

            return await _produtoRepository.UnitOfWork.Commit();
        }


        public void Dispose()
        {
            _produtoRepository.Dispose();
        }
    }
}
