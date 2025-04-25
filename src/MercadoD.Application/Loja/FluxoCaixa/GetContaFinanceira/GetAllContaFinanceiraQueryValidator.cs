using FluentValidation;
using MercadoD.Application.Data;

namespace MercadoD.Application.Loja.FluxoCaixa.GetContaFinanceira
{
    public class GetAllContaFinanceiraQueryValidator : GetPagedQueryValidator<GetAllContaFinanceiraQuery>, IValidator
    {
        public GetAllContaFinanceiraQueryValidator()
            : base()
        {
            
        }
    }
}
