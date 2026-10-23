namespace MenuMall.UI
{
    // Återanvändbar meny, byggd ur MainMenu i Garaget/Universal.
    // Skillnaden: menyvalen är inte hårdkodade. Du registrerar en text och en metod per val,
    // och menyn sköter numrering, inläsning, felhantering och paus.
    public class ConsoleMenu
    {
        private readonly string _title;
        private readonly List<(string Label, Action Action)> _items = new();

        public ConsoleMenu(string title)
        {
            _title = title;
        }

        // Returnerar menyn själv så att man kan kedja: menu.Add(...).Add(...)
        public ConsoleMenu Add(string label, Action action)
        {
            _items.Add((label, action));
            return this;
        }

        // En undermeny är bara ett menyval som kör en annan meny
        public ConsoleMenu AddSubMenu(string label, ConsoleMenu subMenu)
        {
            return Add(label, subMenu.Run);
        }

        // Kör tills användaren väljer 0. I en undermeny betyder 0 "tillbaka".
        public void Run()
        {
            while (true)
            {
                ConsoleOutput.Clear();
                ConsoleOutput.WriteHeader($"=== {_title} ===");
                Console.WriteLine();

                // Select med index: varje val blir "1. text", "2. text"...
                foreach (var line in _items.Select((item, i) => $"{i + 1}. {item.Label}"))
                {
                    Console.WriteLine(line);
                }
                Console.WriteLine("0. Tillbaka / avsluta");
                Console.WriteLine();

                int choice = ConsoleInput.ReadInt($"Välj (0-{_items.Count}): ", 0, _items.Count);
                if (choice == 0)
                {
                    return;
                }

                var item = _items[choice - 1];
                Console.WriteLine();

                // En undermeny har egen loop och egen paus, så ingen extra paus efter den
                bool isSubMenu = item.Action.Target is ConsoleMenu;
                if (!isSubMenu)
                {
                    ConsoleOutput.WriteHeader(item.Label);
                }

                item.Action();

                if (!isSubMenu)
                {
                    ConsoleOutput.Pause();
                }
            }
        }
    }
}
