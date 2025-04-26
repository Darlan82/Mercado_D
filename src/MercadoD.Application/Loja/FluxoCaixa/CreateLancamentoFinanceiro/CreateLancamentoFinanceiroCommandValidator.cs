using FluentValidation;
using MercadoD.Domain.Loja.FluxoCaixa;

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
