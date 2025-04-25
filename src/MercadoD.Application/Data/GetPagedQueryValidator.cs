using FluentValidation;

namespace MercadoD.Application.Data
{
    public abstract class GetPagedQueryValidator<T> : AbstractValidator<T>
        where T : GetPaginatedQuery

    {
        public GetPagedQueryValidator()
        {
            RuleFor(p => p.PaginaAtual).GreaterThanOrEqualTo(0).LessThanOrEqualTo(1000);
            RuleFor(p => p.QtdRegistros).GreaterThan(0).LessThanOrEqualTo(100);
        }
    }
}
