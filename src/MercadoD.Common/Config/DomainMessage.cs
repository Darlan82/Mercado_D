namespace MercadoD.Common.Config
{
    public static class DomainMessage
    {
        public static DomainMessageTypeInfra TypeInfra
        {
            get
            {
                var typeInfra = Environment.GetEnvironmentVariable("DomainMessageTypeInfra");
                if (string.IsNullOrEmpty(typeInfra))
                    return DomainMessageTypeInfra.ServiceBus;
                return Enum.TryParse(typeInfra, out DomainMessageTypeInfra result) ? result : DomainMessageTypeInfra.ServiceBus;
            }
            set
            {
                Environment.SetEnvironmentVariable("DomainMessageTypeInfra", value.ToString());
            }
        }


        public enum DomainMessageTypeInfra
        {
            ServiceBus = 1,
            RabbitMQ = 2,
        }
    }
}
