using GeladosBH.Domain.Interfaces;
using GeladosBH.Domain.Models;
using GeladosBH.Domain.Models.Validations;
using GeladosBH.Domain.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GeladosBH.Domain.Tests
{

    [Collection(nameof(CarrinhoTestsAutoMockerCollection))]
    public class CarrinhoServiceTests
    {

        private readonly CarrinhoTestsAutoMockerFixture _carrinhoTestsAutoMockerFixture;
        private readonly CarrinhoService _carrinhoService;

        public CarrinhoServiceTests(CarrinhoTestsAutoMockerFixture carrinhoTestsAutoMockerFixture)
        {
            _carrinhoTestsAutoMockerFixture = carrinhoTestsAutoMockerFixture;
            _carrinhoService = _carrinhoTestsAutoMockerFixture.ObterCarrinhoService();
        }

        [Fact(DisplayName = "Adicionar carrinho válido deve retornar true")]
        [Trait("Categoria", "Carrinho")]
        public void AdicionarCarrinhoValido_NovoCarrinho_DeveRetornarTrue()
        {
            //Arrange
            var carrinho = _carrinhoTestsAutoMockerFixture.ObterCarrinhoValido();

            //Act
            _carrinhoTestsAutoMockerFixture.Mocker.GetMock<ICarrinhoRepository>().Setup(r => r.UnitOfWork.Commit()).Returns(Task.FromResult(true));
            var result = _carrinhoService.AdicionarCarrinho(carrinho).Result;

            //Assert
            Assert.True(result);
            _carrinhoTestsAutoMockerFixture.Mocker.GetMock<ICarrinhoRepository>().Verify(m => m.AdicionarCarrinho(It.IsAny<Carrinho>()), Times.Once);

        }

        [Fact(DisplayName = "Adicionar carrinho inválido deve retornar false")]
        [Trait("Categoria", "Carrinho")]
        public void AdicionarCarrinhoInvalido_NovoCarrinho_DeveRetornarFalse()
        {
            //Arrange
            var carrinho = _carrinhoTestsAutoMockerFixture.ObterCarrinhoInvalido();

            //Act
            _carrinhoTestsAutoMockerFixture.Mocker.GetMock<ICarrinhoRepository>().Setup(r => r.UnitOfWork.Commit()).Returns(Task.FromResult(true));
            var result = _carrinhoService.AdicionarCarrinho(carrinho).Result;

            //Assert
            Assert.False(result);
            _carrinhoTestsAutoMockerFixture.Mocker.GetMock<ICarrinhoRepository>().Verify(m => m.AdicionarCarrinho(It.IsAny<Carrinho>()), Times.Never);

        }

        [Fact(DisplayName = "Criar carrinho inválido deve retornar mensagens de validação")]
        [Trait("Categoria", "Carrinho")]
        public void CriarCarrinhoInvalido_NovoCarrinho_DeveRetornarMensagensValidacao()
        {
            //Arrange
            var carrinho = _carrinhoTestsAutoMockerFixture.ObterCarrinhoInvalido();
            var validator = new CarrinhoValidation();

            //Act
            var validationResult = validator.Validate(carrinho);

            //Assert
            Assert.NotEmpty(validationResult.Errors);
        }

        [Fact(DisplayName = "Adicionar carrinho item válidos deve retornar true")]
        [Trait("Categoria", "Carrinho Item")]
        public void AdicionarCarrinhoItemValido_NovoCarrinhoItem_DeveRetornarTrue()
        {
            //Arrange
            var carrinhoItens = _carrinhoTestsAutoMockerFixture.ObterCarrinhoItensValidos(1);
            var carrinhoItem = carrinhoItens.FirstOrDefault();

            //Act
            _carrinhoTestsAutoMockerFixture.Mocker.GetMock<ICarrinhoRepository>().Setup(r => r.UnitOfWork.Commit()).Returns(Task.FromResult(true));
            var result = _carrinhoService.AdicionarCarrinhoItem(carrinhoItem).Result;

            //Assert
            _carrinhoTestsAutoMockerFixture.Mocker.GetMock<ICarrinhoRepository>().Verify(m => m.AdicionarCarrinhoItem(It.IsAny<CarrinhoItem>()), Times.Once);

        }

        [Fact(DisplayName = "Adicionar carrinho item inválidos deve retornar false")]
        [Trait("Categoria", "Carrinho Item")]
        public void AdicionarCarrinhoItemInvalido_NovoCarrinhoItem_DeveRetornarFalse()
        {
            //Arrange
            var carrinhoItens = _carrinhoTestsAutoMockerFixture.ObterCarrinhoItensInvalidos(1);
            var carrinhoItem = carrinhoItens.FirstOrDefault();

            //Act
            _carrinhoTestsAutoMockerFixture.Mocker.GetMock<ICarrinhoRepository>().Setup(r => r.UnitOfWork.Commit()).Returns(Task.FromResult(true));
            var result = _carrinhoService.AdicionarCarrinhoItem(carrinhoItem).Result;

            //Assert
            Assert.False(result);
            _carrinhoTestsAutoMockerFixture.Mocker.GetMock<ICarrinhoRepository>().Verify(m => m.AdicionarCarrinhoItem(It.IsAny<CarrinhoItem>()), Times.Never);

        }

        [Fact(DisplayName = "Criar carrinho item inválido deve retornar mensagens de validação")]
        [Trait("Categoria", "Carrinho Item")]
        public void CriarCarrinhoItemInvalido_NovoCarrinhoItem_DeveRetornarMensagensValidacao()
        {
            //Arrange
            var carrinhoItens = _carrinhoTestsAutoMockerFixture.ObterCarrinhoItensInvalidos(1);
            var carrinhoItem = carrinhoItens.FirstOrDefault();
            var validator = new CarrinhoItemValidation();

            //Act
            var validationResult = validator.Validate(carrinhoItem);

            //Assert
            Assert.NotEmpty(validationResult.Errors);
        }

    }
}
