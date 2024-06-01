using Bogus;
using GeladosBH.Core.Services;
using GeladosBH.Domain.Models;
using GeladosBH.Domain.Services;
using Moq.AutoMock;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace GeladosBH.Domain.Tests
{
    [CollectionDefinition(nameof(ProdutoTestsAutoMockerCollection))]
    public class ProdutoTestsAutoMockerCollection : ICollectionFixture<ProdutosTestsAutoMockerFixture>
    {

    }
    public class ProdutosTestsAutoMockerFixture
    {
        public AutoMocker Mocker;
        public ProdutoService ProdutoService;

        public ProdutoService ObterProdutoService()
        {

            Mocker = new AutoMocker();
            ProdutoService = Mocker.CreateInstance<ProdutoService>();

            return ProdutoService;
        }

        public Produto ObterProdutoInvalido()
        {
            var produto = new Faker<Produto>("pt_BR")
                .CustomInstantiator(f => new Produto(null, null, false, 0, null, 0, 0));

            return produto;
        }

        public Produto ObterProdutoValido()
        {
            var produto = new Faker<Produto>("pt_BR")
                .CustomInstantiator(f => 
                new Produto("Sorvete", 
                            "Sorvete de morango 200g", 
                            true, 10, null, 10, 4));

            return produto;
        }
    }
}
