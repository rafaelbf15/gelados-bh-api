using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeladosBH.Domain.Models.Validations
{
    public class CarrinhoValidation : AbstractValidator<Carrinho>
    {
        public CarrinhoValidation()
        {
            RuleFor(c => c.ColaboradorId)
                .NotEqual(Guid.Empty)
                .WithMessage("Carrinho deve estar associado ao vendedor");
        }
    }
}
