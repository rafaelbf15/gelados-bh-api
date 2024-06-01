using GeladosBH.Core.Services;
using GeladosBH.Domain.Interfaces;
using GeladosBH.Domain.Models;
using GeladosBH.Domain.Models.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeladosBH.Domain.Services
{
    public class CarrinhoService : BaseService, ICarrinhoService
    {
        private readonly ICarrinhoRepository _carrinhoRepository;
        private readonly IProdutoService _produtoService;

        public CarrinhoService(ICarrinhoRepository carrinhoRepository,
                               IProdutoService produtoService,
                               INotificadorService notificadorService) : base(notificadorService)
        {
            _carrinhoRepository = carrinhoRepository;
            _produtoService = produtoService;
        }
        public async Task<bool> AdicionarCarrinho(Carrinho carrinho)
        {
            if (!ExecutarValidacao(new CarrinhoValidation(), carrinho)) return false;

            carrinho.Abastecendo();

            _carrinhoRepository.AdicionarCarrinho(carrinho);

            return await _carrinhoRepository.UnitOfWork.Commit();
        }

        public async Task<bool> AdicionarCarrinhoItem(CarrinhoItem carrinhoItem)
        {
            if (!ExecutarValidacao(new CarrinhoItemValidation(), carrinhoItem)) return false;

            _carrinhoRepository.AdicionarCarrinhoItem(carrinhoItem);

            return await _produtoService
                .DebitarEstoque(carrinhoItem.ProdutoId, carrinhoItem.QuantidadeRetiradaVenda);
        }

        public async Task<bool> RemoverCarrinhoItem(CarrinhoItem carrinhoItem)
        {
            _carrinhoRepository.RemoverCarrinhoItem(carrinhoItem);

            return await _produtoService
                .ReporEstoque(carrinhoItem.ProdutoId, carrinhoItem.QuantidadeRetiradaVenda);
        }
        public async Task<bool> AtualizarCarrinho(Carrinho carrinho)
        {
            if (!ExecutarValidacao(new CarrinhoValidation(), carrinho)) return false;
            _carrinhoRepository.AtualizarCarrinho(carrinho);

            return await _carrinhoRepository.UnitOfWork.Commit();
        }

        public async Task<bool> RemoverCarrinho(Carrinho carrinho)
        {
            _carrinhoRepository.RemoverCarrinho(carrinho);

            return await _carrinhoRepository.UnitOfWork.Commit();
        }
        public async Task<bool> DespacharCarrinho(Carrinho carrinho)
        {
            if (!ExecutarValidacao(new CarrinhoValidation(), carrinho)) return false;

            carrinho.Distribuindo();

            _carrinhoRepository.AtualizarCarrinho(carrinho);

            return await _carrinhoRepository.UnitOfWork.Commit();
        }

        public async  Task<bool> FinalizarCarrinho(Carrinho carrinho)
        {
            if (!ExecutarValidacao(new CarrinhoValidation(), carrinho)) return false;

            carrinho.FinalizarCarrinho();
            _carrinhoRepository.AtualizarCarrinho(carrinho);


            var itens = carrinho.CarrinhoItens;
            _carrinhoRepository.AtualizarCarrinhoItens(itens);


            var itenReporEstoque = itens.Where(i => (i.QuantidadeRetiradaVenda - i.QuantidadeVendida) > 0)
                .Select(i => new ProdutoEstoqueRepo{ ProdutoId = i.ProdutoId, Quantidade = (i.QuantidadeRetiradaVenda - i.QuantidadeVendida) }).ToList();

            if (itenReporEstoque.Any()) return await _produtoService.ReporEstoque(itenReporEstoque);

            return await _carrinhoRepository.UnitOfWork.Commit();
        }

        public void Dispose()
        {
            _carrinhoRepository.Dispose();
        }

    }
}
