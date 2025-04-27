namespace MercadoD.Infrastructure.ValueType
{
    /// <summary>
    /// Representa uma data somente-dia (UTC) no formato inteiro yyyymmdd.
    /// </summary>
    public readonly struct DayStamp : IComparable<DayStamp>, IEquatable<DayStamp>
    {
        public int Value { get; }

        public DayStamp(int year, int month, int day)
        {
            // ✔️  Validação exaustiva
            var _ = new DateOnly(year, month, day);        // lança se for inválida
            Value = year * 10_000 + month * 100 + day;     // 20250430
        }
                
        
        public static DayStamp From(DateOnly d) => new(d.Year, d.Month, d.Day);
        public static DayStamp FromInt(int v) =>  
            new DayStamp(v / 10_000, (v / 100) % 100, v % 100);
        public static DayStamp FromDateTime(DateTime dtUtc)
            => From(DateOnly.FromDateTime(dtUtc.ToUniversalTime()));

        public static DayStamp TodayUtc() => From(DateOnly.FromDateTime(DateTime.UtcNow));
        public DateOnly ToDateOnly() =>
            new(Value / 10_000, (Value / 100) % 100, Value % 100);

        // Comparações
        public int CompareTo(DayStamp other) => Value.CompareTo(other.Value);
        public bool Equals(DayStamp other) => Value == other.Value;
        public override bool Equals(object? obj) => obj is DayStamp ds && Equals(ds);
        public override int GetHashCode() => Value;
        public override string ToString() => ToDateOnly().ToString("yyyy-MM-dd");

        #region -- Operadores de comparação --
        public static bool operator ==(DayStamp left, DayStamp right) => left.Value == right.Value;
        public static bool operator !=(DayStamp left, DayStamp right) => left.Value != right.Value;
        public static bool operator <(DayStamp left, DayStamp right) => left.Value < right.Value;
        public static bool operator <=(DayStamp left, DayStamp right) => left.Value <= right.Value;
        public static bool operator >(DayStamp left, DayStamp right) => left.Value > right.Value;
        public static bool operator >=(DayStamp left, DayStamp right) => left.Value >= right.Value;
        #endregion
    }

}
