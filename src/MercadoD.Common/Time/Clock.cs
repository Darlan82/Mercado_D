using System.Text.RegularExpressions;

namespace MercadoD.Common.Time
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

        public static DateTime CreateDateUtc(int y, int m, int d) =>
            new(y, m, d, 0, 0, 0, DateTimeKind.Utc);

        public static DateTime CreateStringUtc(string sData)
        {
            var match = Regex.Match(sData, @"^(\d{4})-(\d{1,2})-(\d{1,2})$");
            if (!match.Success)
                throw new ArgumentException($"Invalid date '{sData}' format.");

            return CreateDateUtc(
                int.Parse(match.Groups[1].Value),
                int.Parse(match.Groups[2].Value),
                int.Parse(match.Groups[3].Value));
        }
    }

}
