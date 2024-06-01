using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace GeladosBH.Domain.Models.Validations
{
    public class ProdutoValidation : AbstractValidator<Produto>
    {
        public ProdutoValidation()
        {
            RuleFor(p => p.Nome)
                .NotNull()
                .NotEmpty()
                .WithMessage("O campo Nome do produto não pode estar vazio");

            RuleFor(p => p.Descricao)
                .NotNull()
                .NotEmpty()
                .WithMessage("O campo Descrição do produto não pode estar vazio");

            RuleFor(p => p.Valor)
                .GreaterThan(0)
                .WithMessage("O campo Valor do produto deve ser maior que 0.");

            RuleFor(p => p.ValidadeForaDoFreezer)
                .GreaterThan(0)
                .WithMessage("O campo (Validade fora do freezer em Horas) do produto deve ser maior que 0.");
        }
    }
}
