using System.Globalization;

namespace MenuMall.Reflection
{
    // Gör om text till rätt typ och tillbaka. Det är här du lägger till stöd för fler typer
    // om du vill ha t.ex. TimeSpan eller Guid i dina konstruktorer.
    public static class ValueParser
    {
        private static readonly string[] DateFormats = { "yyyy-MM-dd", "yyyyMMdd" };

        private static readonly Type[] NumericTypes =
        {
            typeof(int), typeof(long), typeof(double), typeof(float), typeof(decimal)
        };

        private static readonly Type[] OtherSupportedTypes =
        {
            typeof(string), typeof(bool), typeof(DateTime), typeof(DateOnly)
        };

        // Vilka typer konsolen kan fråga efter. Enum och Nullable av dessa fungerar också.
        // Lägg till en typ i listorna ovan (och i TryParse) för att stödja den.
        public static bool IsSupported(Type type)
        {
            var t = Nullable.GetUnderlyingType(type) ?? type;
            return t.IsEnum || NumericTypes.Contains(t) || OtherSupportedTypes.Contains(t);
        }

        public static bool IsNumeric(Type type)
        {
            var t = Nullable.GetUnderlyingType(type) ?? type;
            return NumericTypes.Contains(t);
        }

        // Försöker göra om input till typen. Både komma och punkt fungerar som decimaltecken.
        public static bool TryParse(string? input, Type type, out object? value)
        {
            value = null;
            var nullableOf = Nullable.GetUnderlyingType(type);
            var t = nullableOf ?? type;

            if (string.IsNullOrWhiteSpace(input))
            {
                return nullableOf is not null; // tomt är ok bara om typen är nullable, t.ex. int?
            }

            var text = input.Trim();
            var number = text.Replace(',', '.');
            var inv = CultureInfo.InvariantCulture;

            if (t == typeof(string)) { value = text; return true; }

            if (t == typeof(int) && int.TryParse(number, NumberStyles.Integer, inv, out var i)) { value = i; return true; }
            if (t == typeof(long) && long.TryParse(number, NumberStyles.Integer, inv, out var l)) { value = l; return true; }
            if (t == typeof(double) && double.TryParse(number, NumberStyles.Float, inv, out var d)) { value = d; return true; }
            if (t == typeof(float) && float.TryParse(number, NumberStyles.Float, inv, out var f)) { value = f; return true; }
            if (t == typeof(decimal) && decimal.TryParse(number, NumberStyles.Number, inv, out var m)) { value = m; return true; }

            if (t == typeof(bool) && TryParseBool(text, out var b)) { value = b; return true; }

            if (t == typeof(DateOnly) && DateOnly.TryParseExact(text, DateFormats, inv, DateTimeStyles.None, out var date)) { value = date; return true; }
            if (t == typeof(DateTime) && DateTime.TryParse(text, inv, DateTimeStyles.None, out var dt)) { value = dt; return true; }

            if (t.IsEnum && Enum.TryParse(t, text, ignoreCase: true, out var e) && Enum.IsDefined(t, e!)) { value = e; return true; }

            return false;
        }

        public static bool TryParseBool(string text, out bool value)
        {
            switch (text.Trim().ToLowerInvariant())
            {
                case "ja": case "j": case "true": case "1": case "yes": case "y":
                    value = true; return true;
                case "nej": case "n": case "false": case "0": case "no":
                    value = false; return true;
                default:
                    value = false; return false;
            }
        }

        // Kollar att ett tal ligger inom [min, max]. NaN betyder ingen gräns.
        public static bool IsInRange(object value, double min, double max)
        {
            if (!IsNumeric(value.GetType())) return true;

            double number = Convert.ToDouble(value, CultureInfo.InvariantCulture);
            return (double.IsNaN(min) || number >= min) && (double.IsNaN(max) || number <= max);
        }

        // Hur ett värde skrivs ut
        public static string Format(object? value) => value switch
        {
            null => "-",
            bool b => b ? "ja" : "nej",
            DateOnly d => d.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
            DateTime dt => dt.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture),
            IFormattable f => f.ToString(null, CultureInfo.InvariantCulture),
            _ => value.ToString() ?? "-"
        };

        // Matchar söktermen ett värde? Termen tolkas som samma typ som värdet,
        // så "12,5" matchar 12.5 och "ja" matchar true.
        public static bool ValueMatches(object? value, string term)
        {
            if (value is null) return false;

            if (value is string s)
            {
                return s.Equals(term.Trim(), StringComparison.OrdinalIgnoreCase);
            }

            if (TryParse(term, value.GetType(), out var parsed) && parsed is not null)
            {
                if (IsNumeric(value.GetType()))
                {
                    return Convert.ToDecimal(value, CultureInfo.InvariantCulture) == Convert.ToDecimal(parsed, CultureInfo.InvariantCulture);
                }

                return value.Equals(parsed);
            }

            return false;
        }
    }
}
