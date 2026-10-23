using MenuMall.Reflection;

namespace MenuMall.UI
{
    // Inläsning från konsolen. Varje metod frågar igen tills svaret är giltigt.
    public static class ConsoleInput
    {
        public static string ReadString(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string? input = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(input))
                {
                    return input.Trim();
                }

                ConsoleOutput.WriteError("Fältet får inte vara tomt.");
            }
        }

        // Tomt svar ger null, vilket betyder "hoppa över"
        public static string? ReadOptionalString(string prompt)
        {
            Console.Write(prompt);
            string? input = Console.ReadLine();
            return string.IsNullOrWhiteSpace(input) ? null : input.Trim();
        }

        public static int ReadInt(string prompt, int min, int max)
        {
            while (true)
            {
                Console.Write(prompt);
                string? input = Console.ReadLine();

                if (int.TryParse(input, out int value) && value >= min && value <= max)
                {
                    return value;
                }

                ConsoleOutput.WriteError($"Ange ett heltal mellan {min} och {max}.");
            }
        }

        // Visar en numrerad lista och returnerar index (0-baserat) för valet
        public static int ReadChoice(string prompt, IReadOnlyList<string> options)
        {
            // Select med index: (text, i) blir "1. text", "2. text"...
            foreach (var line in options.Select((option, i) => $"{i + 1}. {option}"))
            {
                Console.WriteLine(line);
            }

            return ReadInt(prompt, 1, options.Count) - 1;
        }

        // Generisk inläsning: frågar efter ett värde av valfri typ som ValueParser stödjer.
        // Enum visas som numrerad lista, bool som ja/nej, tal kontrolleras mot min/max.
        // hasDefault + defaultValue: tomt svar ger standardvärdet (för valfria konstruktorparametrar).
        public static object? ReadValue(Type type, string label, double min = double.NaN, double max = double.NaN,
                                        bool hasDefault = false, object? defaultValue = null)
        {
            var t = Nullable.GetUnderlyingType(type) ?? type;
            bool optional = hasDefault || Nullable.GetUnderlyingType(type) is not null;

            if (t.IsEnum)
            {
                return ReadEnum(t, label, optional, defaultValue);
            }

            string hint = BuildHint(t, min, max, optional, defaultValue);

            while (true)
            {
                Console.Write($"{label}{hint}: ");
                string? input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input) && optional)
                {
                    return hasDefault ? defaultValue : null;
                }

                if (ValueParser.TryParse(input, t, out var value) && value is not null)
                {
                    if (ValueParser.IsInRange(value, min, max))
                    {
                        return value;
                    }

                    ConsoleOutput.WriteError($"Värdet måste vara {RangeText(min, max)}.");
                    continue;
                }

                ConsoleOutput.WriteError(ErrorFor(t));
            }
        }

        private static object? ReadEnum(Type enumType, string label, bool optional, object? defaultValue)
        {
            var names = Enum.GetNames(enumType);
            var values = Enum.GetValues(enumType);

            Console.WriteLine($"{label}:");
            foreach (var line in names.Select((name, i) => $"  {i + 1}. {name}"))
            {
                Console.WriteLine(line);
            }

            while (true)
            {
                Console.Write(optional ? $"Välj (1-{names.Length}, tomt = hoppa över): " : $"Välj (1-{names.Length}): ");
                string? input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input) && optional)
                {
                    return defaultValue;
                }

                if (int.TryParse(input, out int choice) && choice >= 1 && choice <= names.Length)
                {
                    return values.GetValue(choice - 1);
                }

                // Det går också att skriva namnet direkt
                if (ValueParser.TryParse(input, enumType, out var parsed))
                {
                    return parsed;
                }

                ConsoleOutput.WriteError($"Ange en siffra mellan 1 och {names.Length}.");
            }
        }

        private static string BuildHint(Type t, double min, double max, bool optional, object? defaultValue)
        {
            var parts = new List<string>();

            if (t == typeof(bool)) parts.Add("ja/nej");
            if (t == typeof(DateOnly) || t == typeof(DateTime)) parts.Add("ÅÅÅÅ-MM-DD");
            string range = RangeText(min, max);
            if (range.Length > 0) parts.Add(range);
            if (optional) parts.Add(defaultValue is null ? "valfritt" : $"tomt = {ValueParser.Format(defaultValue)}");

            return parts.Count == 0 ? "" : $" ({string.Join(", ", parts)})";
        }

        // "1-5000", "minst 0", "högst 10" eller tomt om inga gränser
        private static string RangeText(double min, double max)
        {
            bool hasMin = !double.IsNaN(min), hasMax = !double.IsNaN(max);

            if (hasMin && hasMax) return $"{ValueParser.Format(min)}-{ValueParser.Format(max)}";
            if (hasMin) return $"minst {ValueParser.Format(min)}";
            if (hasMax) return $"högst {ValueParser.Format(max)}";
            return "";
        }

        private static string ErrorFor(Type t)
        {
            if (t == typeof(int) || t == typeof(long)) return "Ange ett heltal.";
            if (ValueParser.IsNumeric(t)) return "Ange ett tal.";
            if (t == typeof(bool)) return "Svara ja eller nej.";
            if (t == typeof(DateOnly) || t == typeof(DateTime)) return "Ange ett datum som ÅÅÅÅ-MM-DD.";
            return "Fältet får inte vara tomt.";
        }
    }
}
