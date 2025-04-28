namespace MercadoD.Domain.Loja.FluxoCaixa.Exceptions
{
    public class InvalidDtPagamentoDomainException : DomainException
    {
        public InvalidDtPagamentoDomainException(string fieldName)
            : base($"O campo do nome '{fieldName}' não é válido.")
        {
        }
    }
}
