using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeladosBH.API.ViewModels
{
    public class CarrinhoViewModel
    {
        public Guid? Id { get; set; }
        public Guid ColaboradorId { get; set; }
        public DateTime? DataCadastro { get; set; }

        public CarrinhoStatusViewModel CarrinhoStatus { get; set; }

        public IEnumerable<CarrinhoItemViewModel> CarrinhoItens;
    }
}
