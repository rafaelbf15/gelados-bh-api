using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeladosBH.Domain.Models.Validations
{
    public class CarrinhoItemValidation : AbstractValidator<CarrinhoItem>
    {
        public CarrinhoItemValidation()
        {

            RuleFor(ci => ci.QuantidadeRetiradaVenda)
             .GreaterThan(0)
             .WithMessage("Quantidade retirada para venda deve ser maior que 0");
        }
    }
}
