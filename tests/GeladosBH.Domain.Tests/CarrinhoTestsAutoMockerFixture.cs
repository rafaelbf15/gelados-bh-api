using Bogus;
using GeladosBH.Domain.Models;
using GeladosBH.Domain.Services;
using Moq.AutoMock;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace GeladosBH.Domain.Tests
{
    [CollectionDefinition(nameof(CarrinhoTestsAutoMockerCollection))]
    public class CarrinhoTestsAutoMockerCollection : ICollectionFixture<CarrinhoTestsAutoMockerFixture>
    {

    }
    public class CarrinhoTestsAutoMockerFixture
    {
        public AutoMocker Mocker;
        public CarrinhoService CarrinhoService;


         public CarrinhoService ObterCarrinhoService()
        {

            Mocker = new AutoMocker();
            CarrinhoService = Mocker.CreateInstance<CarrinhoService>();

            return CarrinhoService;
        }


        public Carrinho ObterCarrinhoValido()
        {
            var carrinho = new Faker<Carrinho>("pt_BR")
                .CustomInstantiator(f => new Carrinho(Guid.NewGuid()));

            return carrinho;
        }

        public Carrinho ObterCarrinhoInvalido()
        {
            var carrinho = new Faker<Carrinho>("pt_BR")
                .CustomInstantiator(f => new Carrinho(Guid.Empty));

            return carrinho;
        }

        public IEnumerable<CarrinhoItem> ObterCarrinhoItensValidos(int quantidade)
        {
            var carrinhoItem = new Faker<CarrinhoItem>("pt_BR")
                .CustomInstantiator(f => new CarrinhoItem(Guid.NewGuid(), Guid.NewGuid(), 10));

            return carrinhoItem.Generate(quantidade);
        }

        public IEnumerable<CarrinhoItem> ObterCarrinhoItensInvalidos(int quantidade)
        {
            var carrinhoItem = new Faker<CarrinhoItem>("pt_BR")
                .CustomInstantiator(f => new CarrinhoItem(Guid.Empty, Guid.Empty, 0));

            return carrinhoItem.Generate(quantidade);
        }


    }
}
