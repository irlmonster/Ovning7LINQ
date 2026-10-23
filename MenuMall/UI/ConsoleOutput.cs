namespace MenuMall.UI
{
    // Hjälpklass för färgad utskrift i konsolen
    public static class ConsoleOutput
    {
        public static void WriteSuccess(string message) => WriteInColor(message, ConsoleColor.Green);
        public static void WriteError(string message) => WriteInColor(message, ConsoleColor.Red);
        public static void WriteWarning(string message) => WriteInColor(message, ConsoleColor.Yellow);
        public static void WriteHeader(string message) => WriteInColor(message, ConsoleColor.Cyan);

        // Stoppar tills användaren trycker på en tangent, så utskrifter hinner läsas
        public static void Pause()
        {
            WriteInColor("\nTryck på valfri tangent för att fortsätta...", ConsoleColor.DarkGray);

            // ReadKey fungerar inte när input kommer från en fil eller pipe (t.ex. i ett skript)
            if (Console.IsInputRedirected)
                Console.ReadLine();
            else
                Console.ReadKey(true);
        }

        public static void Clear()
        {
            if (!Console.IsOutputRedirected)
                Console.Clear();
        }

        // Enda stället som rör Console.ForegroundColor
        private static void WriteInColor(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}
