using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeladosBH.API.ViewModels
{
    public class CarrinhoItemViewModel
    {
        public Guid? Id { get; set; }
        public Guid CarrinhoId { get; set; }
        public Guid ProdutoId { get; set; }
        public int QuantidadeRetiradaVenda { get; set; }
        public int QuantidadeVendida { get; set; }
        public DateTime DataVenda { get; set; }

        public ProdutoViewModel Produto { get; set; }
    }
}
