using FluentValidation;
using MercadoD.Domain.Loja.FluxoCaixa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MercadoD.Application.Loja.FluxoCaixa.CreateLancamentoFinanceiro
{
    public class CreateLancamentoFinanceiroCommandValidator : AbstractValidator<CreateLancamentoFinanceiroCommand>, IValidator
    {
        public CreateLancamentoFinanceiroCommandValidator()
        {
            RuleFor(p => p.Descricao).NotEmpty().MaximumLength(FluxoCaixaConstants.LancamentoFinanceiro.DescricaoMaxLength)
                .MinimumLength(FluxoCaixaConstants.LancamentoFinanceiro.DescricaoMinLength);
        }
    }
}
