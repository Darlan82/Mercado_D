namespace MercadoD.Infrastructure.Time
{
    public static class Clock
    {
        private static readonly AsyncLocal<Func<DateTime>> _current = new();

        static Clock() => _current.Value = () => DateTime.UtcNow;

        public static DateTime UtcNow => _current.Value?.Invoke() ?? DateTime.UtcNow;

        public static IDisposable Override(Func<DateTime> provider)
        {
            if (provider is null) throw new ArgumentNullException(nameof(provider));

            var previous = _current.Value;
            _current.Value = provider;
            return new Scope(() => _current.Value = previous);
        }

        private sealed record Scope(Action Dispose) : IDisposable
        {
            void IDisposable.Dispose() => Dispose();
        }
    }

}
