namespace MercadoD.Application.Data
{
    public abstract record GetPaginatedQuery(int PaginaAtual, int QtdRegistros);

    //public abstract class GetPaginatedQuery<TFiltro> where TFiltro : class
    //{
    //    public TFiltro Filtro { get; set; }

    //    public int PaginaAtual { get; set; }
    //    public int QtdRegistros { get; set; }

    //    protected GetPaginatedQuery(TFiltro filtro, int paginaAtual, int? qtdRegistros)
    //    {
    //        Filtro = filtro ?? throw new ArgumentNullException(nameof(filtro));
    //        PaginaAtual = paginaAtual;
    //        QtdRegistros = qtdRegistros.GetValueOrDefault(10);
    //    }
    //}
}
