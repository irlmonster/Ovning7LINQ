using MenuMall.Models;
using MenuMall.Exercises;
using MenuMall.UI;

namespace MenuMall
{
    class Program
    {
        static void Main(string[] args)
        {


            Console.OutputEncoding = System.Text.Encoding.UTF8;

            List<Student> studenter = new List<Student>
                 {
                 new Student { Id = 1, Namn = "Anna", Alder = 19, Klass = "A", Poang = 85 },
                 new Student { Id = 2, Namn = "Bertil", Alder = 22, Klass = "B", Poang = 72 },
                 new Student { Id = 3, Namn = "Cecilia", Alder = 20, Klass = "A", Poang = 91 },
                 new Student { Id = 4, Namn = "David", Alder = 24, Klass = "C", Poang = 64 },
                 new Student { Id = 5, Namn = "Eva", Alder = 19, Klass = "B", Poang = 85 },
                 new Student { Id = 6, Namn = "Felix", Alder = 21, Klass = "A", Poang = 77 },
                 new Student { Id = 7, Namn = "Gustav", Alder = 23, Klass = "C", Poang = 58 },
                 new Student { Id = 8, Namn = "Hanna", Alder = 20, Klass = "B", Poang = 95 }
                 };

            var ex = new LinqExercises(studenter);

            // Bygg menyn här. Varje Add tar en text och en metod (eller lambda) som körs när valet görs.
            // AddSubMenu lägger en hel meny som ett val, 0 i undermenyn går tillbaka.
            //
            // Byt ut exemplen nedan mot dina egna val.
            var menu = new ConsoleMenu("Studentlistan")
                .Add("(Where) Äldre än 20år och mer än 80 poäng.", () =>
                {
                    ex.AlderOver20PoangOver80();
                })

                .Add("(Select) Visa bara namn och namn och poäng i annonym typ", () =>
                {
                    ex.ListaPaNamn();
                    ex.NamnOchPoangAnonymTyp();
                })

                .Add("(OrderBy) Sortera efter ålder och sortera efter poäng, högst först", () =>
                {
                    ex.SortPaAlderOPoang();
                })

                .Add("(Count) Räkna hur många studenter som finns och hur många som går i klass A", () =>
                {
                    ex.CountStudents();
                })

                .Add("(Take) Ta ut de tre första studenterna och ta ut de två poängen efter sortering", () =>
                {
                    ex.TaUtPoangOchTop3Studenter();
                })

                .Add("(Distinct) Visa alla unika klassnamn och visa alla unika åldrar.", () =>
                {
                    ex.UnikaAldrarOchKlassnamn();
                })

                .Add("(Any/All) Finns någon student med 100p? Har alla studenter minst 50p?", () =>
                {
                    ex.AnyStudentOver100AndAllStudentsOver50();
                })

                .Add("(FirstOrDefault / Last / Single) Hämta första i klass b, sista i listan och visst id", () =>
                {
                    ex.hamtaMassaStudenter();
                })

                .Add("(GroupBy) Gruppera per klass och varje klass + namn i gruppen.", () =>
                {
                    ex.GrupperaPerKlassOchSkrivUtVarjeKlassOchNamn();
                })

                .Add("(Sum / Average / MIN / MAX) Summera alla poäng, räkna ut medelpoäng och lägsta och hösta poäng.", () =>
                {
                    ex.SumRaknaUtMedelOchHittaMinMax();
                })
                
                .AddSubMenu("Skriv ut hela listan: ", new ConsoleMenu("Undermeny")
                    .Add("Fallande", () => ex.SkrivUtAllaFallande())
                    .Add("Stigande", () => ex.SkrivUtAllaStigande()));

            menu.Run();
            ConsoleOutput.WriteSuccess("Hej då!");
        }
    }
}
