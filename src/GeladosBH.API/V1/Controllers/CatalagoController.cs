using AutoMapper;
using GeladosBH.API.Controllers;
using GeladosBH.API.Extensions;
using GeladosBH.API.ViewModels;
using GeladosBH.Business.Intefaces;
using GeladosBH.Core.Services;
using GeladosBH.Domain.Interfaces;
using GeladosBH.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeladosBH.API.V1.Controllers
{

    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/catalogo")]
    public class CatalagoController : MainController
    {
        private readonly IProdutoService _produtoService;
        private readonly IProdutoRepository _produtoRepository;
        private readonly IMapper _mapper;

        public CatalagoController(IProdutoService produtoService, 
                                  IMapper mapper, 
                                  IProdutoRepository produtoRepository, 
                                  INotificadorService notificadorService,
                                  IUser user) : base(notificadorService, user)
        {
            _produtoService = produtoService;
            _produtoRepository = produtoRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("produtos")]
        public async Task<IActionResult> ObterProdutos()
        {

            var produtos = await _produtoRepository.ObterProdutos();

            if (!produtos.Any()) return CustomResponse();

            var produtosViewModel = _mapper.Map<IEnumerable<ProdutoViewModel>>(produtos);

            return CustomResponse(produtosViewModel);
        }

        [HttpPost]
        // [ClaimsAuthorize("ADMIN", "ADD")]
        [Route("produtos")]
        public async Task<IActionResult> AdicionarProduto(ProdutoViewModel produtoViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(produtoViewModel);

            if (produtoViewModel == null) return CustomResponse(produtoViewModel);

            var produto = _mapper.Map<Produto>(produtoViewModel);

            if(!await _produtoService.AdicionarProduto(produto))
            {
                NotificarErro("Erro ao adicionar produto, tente novamente!");
                return CustomResponse(produtoViewModel);
            }
            return CustomResponse(produtoViewModel);
        }

        [HttpPut]
        // [ClaimsAuthorize("ADMIN", "ED")]
        [Route("produtos/{produtoId:guid}")]
        public async Task<IActionResult> AtualizarProduto(Guid produtoId, ProdutoViewModel produtoViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(produtoViewModel);

            if (produtoViewModel == null) return CustomResponse(produtoViewModel);

            if (produtoId != produtoViewModel.Id) return CustomResponse(produtoViewModel);

            var produto = _mapper.Map<Produto>(produtoViewModel);

            if (!await _produtoService.AtualizarProduto(produto))
            {
                NotificarErro("Erro ao atualizar produto, tente novamente!");
                return CustomResponse(produtoViewModel);
            }
            return CustomResponse(produtoViewModel);
        }

        [HttpDelete]
        // [ClaimsAuthorize("ADMIN", "DEL")]
        [Route("produtos/{produtoId:guid}")]
        public async Task<IActionResult> RemoverProduto(Guid produtoId)
        {

            var produto = await _produtoRepository.ObterProdutoPorId(produtoId);

            if (produto == null) return CustomResponse();

            if (!await _produtoService.RemoverProduto(produto))
            {
                NotificarErro("Erro ao remover produto, tente novamente!");
                return CustomResponse();
            }

            return CustomResponse();
        }
    }
}
