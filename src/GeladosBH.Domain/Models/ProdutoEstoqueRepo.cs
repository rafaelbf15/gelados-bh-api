using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GeladosBH.Domain.Models
{
    [NotMapped]
    public class ProdutoEstoqueRepo
    {
        public Guid ProdutoId { get; set; }
        public int Quantidade { get; set; }
    }
}
