using AutoMapper;
using GeladosBH.API.Controllers;
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
    [Route("api/v{version:apiVersion}/carrinho")]
    public class CarrinhoController : MainController
    {
        private readonly ICarrinhoService _carrinhoService;
        private readonly ICarrinhoRepository _carrinhoRepository;
        private readonly IMapper _mapper;

        public CarrinhoController(ICarrinhoService carrinhoService,
                                  ICarrinhoRepository carrinhoRepository,
                                  IMapper mapper,
                                  INotificadorService notificadorService,
                                  IUser user) : base(notificadorService, user)
        {
            _carrinhoService = carrinhoService;
            _carrinhoRepository = carrinhoRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("carrinhos")]
        public async Task<IActionResult> ObterCarrinho()
        {

            var carrinhos = await _carrinhoRepository.ObterCarrinhos();

            if (!carrinhos.Any()) return CustomResponse();

            var carrinhosViewModel = _mapper.Map<IEnumerable<CarrinhoViewModel>>(carrinhos);

            return CustomResponse(carrinhosViewModel);
        }

        [HttpGet]
        [Route("carrinhos/{carrinhoId:guid}")]
        public async Task<IActionResult> ObterCarrinho(Guid carrinhoId)
        {

            var carrinho = await _carrinhoRepository.ObterCarrinhoPorId(carrinhoId);

            if (carrinho == null) return CustomResponse();

            var carrinhoViewModel = _mapper.Map<CarrinhoViewModel>(carrinho);

            return CustomResponse(carrinhoViewModel);
        }

        [HttpPost]
        [Route("carrinhos")]
        public async Task<IActionResult> AdicionarCarrinho(CarrinhoViewModel carrinhoViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(carrinhoViewModel);

            if (carrinhoViewModel == null) return CustomResponse(carrinhoViewModel);

            var carrinho = _mapper.Map<Carrinho>(carrinhoViewModel);

            if (!await _carrinhoService.AdicionarCarrinho(carrinho))
            {
                NotificarErro("Erro ao adicionar carrinho, tente novamente!");
                return CustomResponse(carrinhoViewModel);
            }
            return CustomResponse(carrinhoViewModel);
        }

        [HttpPost]
        [Route("carrinhos/{carrinhoId:guid}/itens")]
        public async Task<IActionResult> AdicionarCarrinhoItem(Guid carrinhoId, CarrinhoItemViewModel carrinhoItemViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(carrinhoItemViewModel);

            if (carrinhoItemViewModel == null) return CustomResponse(carrinhoItemViewModel);

            if (carrinhoId != carrinhoItemViewModel.CarrinhoId) return CustomResponse(carrinhoItemViewModel);

            var carrinhoItem = _mapper.Map<CarrinhoItem>(carrinhoItemViewModel);

            if (!await _carrinhoService.AdicionarCarrinhoItem(carrinhoItem))
            {
                NotificarErro("Erro ao adicionar item ao carrinho, tente novamente!");
                return CustomResponse(carrinhoItemViewModel);
            }
            return CustomResponse(carrinhoItemViewModel);
        }

        [HttpDelete]
        [Route("carrinhos/{carrinhoId:guid}/itens/{itemId:guid}")]
        public async Task<IActionResult> RemoverCarrinhoItem(Guid itemId)
        {
            var carrinhoItem = await _carrinhoRepository.ObterCarrinhoItenPorId(itemId);

            if (carrinhoItem == null)
            {
                NotificarErro("Erro ao remover item, tente novamente!");
                return CustomResponse();
            }

            if (!await _carrinhoService.RemoverCarrinhoItem(carrinhoItem))
            {
                NotificarErro("Erro ao remover item, tente novamente!");
                return CustomResponse();
            }
            return CustomResponse();
        }

        [HttpPost]
        [Route("carrinhos/{carrinhoId:guid}/distribuicao")]
        public async Task<IActionResult> DespacharCarrinho(Guid carrinhoId, CarrinhoViewModel carrinhoViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(carrinhoViewModel);

            if (carrinhoViewModel == null) return CustomResponse(carrinhoViewModel);

            if (carrinhoId != carrinhoViewModel.Id) return CustomResponse(carrinhoViewModel);

            var carrinho = _mapper.Map<Carrinho>(carrinhoViewModel);

            if (!await _carrinhoService.DespacharCarrinho(carrinho))
            {
                NotificarErro("Erro ao despachar carrinho, tente novamente!");
                return CustomResponse(carrinhoViewModel);
            }
            return CustomResponse(carrinhoViewModel);
        }

        [HttpPost]
        [Route("carrinhos/{carrinhoId:guid}/finalizado")]
        public async Task<IActionResult> FanalizarCarrinho(Guid carrinhoId, CarrinhoViewModel carrinhoViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(carrinhoViewModel);

            if (carrinhoViewModel == null) return CustomResponse(carrinhoViewModel);

            if (carrinhoId != carrinhoViewModel.Id) return CustomResponse(carrinhoViewModel);

            var carrinho = _mapper.Map<Carrinho>(carrinhoViewModel);

            if (!await _carrinhoService.FinalizarCarrinho(carrinho))
            {
                NotificarErro("Erro ao finalizar carrinho, tente novamente!");
                return CustomResponse(carrinhoViewModel);
            }
            return CustomResponse(carrinhoViewModel);
        }
    }
}
