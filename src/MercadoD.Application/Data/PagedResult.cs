namespace MercadoD.Application.Data
{
    public sealed class PagedResult<TEntity>
    {
        public int PaginaAtual { get; private set; }
        public int QtdRegistros { get; private set; }
        public int QtdTotal { get; private set; }

        public IEnumerable<TEntity> Registros { get; private set; }

        public PagedResult(int paginaAtual, int qtdRegistros, int qtdTotal, IEnumerable<TEntity> registros)
        {
            PaginaAtual = paginaAtual;
            QtdRegistros = qtdRegistros;
            QtdTotal = qtdTotal;
            Registros = registros ?? throw new ArgumentNullException(nameof(registros));
        }
    }
}
