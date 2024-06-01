using GeladosBH.Core.Services;
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

    [Collection(nameof(ProdutoTestsAutoMockerCollection))]
    public class ProdutoServiceTests
    {
        private readonly ProdutosTestsAutoMockerFixture _produtoTestsAutoMockerFixture;
        private readonly ProdutoService _produtoService;

        public ProdutoServiceTests(ProdutosTestsAutoMockerFixture produtoTestsAutoMockerFixture)
        {
            _produtoTestsAutoMockerFixture = produtoTestsAutoMockerFixture;
            _produtoService = _produtoTestsAutoMockerFixture.ObterProdutoService();
        }

        [Fact(DisplayName = "Adicionar produto válido deve retornar true")]
        [Trait("Categoria", "Produto")]
        public void AdicionarProdutoValido_NovoProduto_DeveRetornarTrue()
        {
            //Arrange
            var produto = _produtoTestsAutoMockerFixture.ObterProdutoValido();

            //Act
            _produtoTestsAutoMockerFixture.Mocker.GetMock<IProdutoRepository>().Setup(r => r.UnitOfWork.Commit()).Returns(Task.FromResult(true));
            var result = _produtoService.AdicionarProduto(produto).Result;

            //Assert
            Assert.True(result);
            _produtoTestsAutoMockerFixture.Mocker.GetMock<IProdutoRepository>().Verify(m => m.AdicionarProduto(It.IsAny<Produto>()), Times.Once);

        }

        [Fact(DisplayName = "Adicionar produto inválido deve retornar false")]
        [Trait("Categoria", "Produto")]
        public void AdicionarProdutoInvalido_NovoProduto_DeveRetornarFalse()
        {
            //Arrange
            var produto = _produtoTestsAutoMockerFixture.ObterProdutoInvalido();

            //Act
            _produtoTestsAutoMockerFixture.Mocker.GetMock<IProdutoRepository>().Setup(r => r.UnitOfWork.Commit()).Returns(Task.FromResult(true));
            var result = _produtoService.AdicionarProduto(produto).Result;

            //Assert
            Assert.False(result);
            _produtoTestsAutoMockerFixture.Mocker.GetMock<IProdutoRepository>().Verify(m => m.AdicionarProduto(It.IsAny<Produto>()), Times.Never);
        }

        [Fact(DisplayName = "Criar produto inválido deve retornar mensagens de validação")]
        [Trait("Categoria", "Produto")]
        public void CriarProdutoInvalido_NovoProduto_DeveRetornarMensagensValidacao()
        {
            //Arrange
            var produto = _produtoTestsAutoMockerFixture.ObterProdutoInvalido();
            var validator = new ProdutoValidation();

            //Act
            var validationResult = validator.Validate(produto);

            //Assert
            Assert.NotEmpty(validationResult.Errors);


        }
    }
}
