using MenuMall.Models;
using MenuMall.UI;

namespace MenuMall.Exercises
{
    public class LinqExercises
    {

        // En klass för metoder att köra på lista av studenter
        private readonly List<Student> _studenter;

        public LinqExercises(List<Student> studenter)
        {
            _studenter = studenter;
        }


        // Skriv metoder för att gå över listan med studenter


        // -- Where --
        // Ålder över 20 (Where)
        public void AlderOver20PoangOver80()
        {
            var aldre = _studenter.Where(s => s.Alder > 20);
            var points = _studenter.Where(s => s.Poang > 80);

            ConsoleOutput.WriteHeader($"\nÖver 20 år:");
            foreach (var s in aldre)
            {
                Console.WriteLine($"{s.Namn}, Ålder: {s.Alder} år");

            }
            ConsoleOutput.WriteHeader($"\nÖver 80 poäng: ");
            foreach (var s in points)
            {
                Console.WriteLine($"{s.Namn} : {s.Poang} poäng");
            }
        }

        // -- Select --
        // Visa bara namn på alla studenter

        public void ListaPaNamn()
        {
            var namn = _studenter.Select(s => s.Namn);
            ConsoleOutput.WriteHeader($"\nNamn: ");
            foreach (var n in namn)
            {
                Console.WriteLine(n);
            }
        }


        // visa namn och poäng i en ny anonym typ.
        public void NamnOchPoangAnonymTyp()
        {
            var namnOchPoang = _studenter.Select(s => new
            {
                s.Namn,
                s.Poang
            });
            ConsoleOutput.WriteHeader($"\nNamn och poäng: ");
            foreach (var x in namnOchPoang)
            {
                Console.WriteLine($"{x.Namn}, {x.Poang} poäng.");
            }


        }


        // -- OrderBy --
        //Sortera studenter efter ålder
        public void SortPaAlderOPoang()
        {
            var sortAlder = _studenter.OrderBy(s => s.Alder).ToList();
            var sortPoints = _studenter.OrderByDescending(s => s.Poang).ToList();

            ConsoleOutput.WriteHeader("\nSorterade på ålder:");
            foreach (var student in sortAlder)
            {
                Console.WriteLine($"{student.Namn}, Ålder: {student.Alder} ");
            }


            ConsoleOutput.WriteHeader("\nSorterade på poäng:");
            foreach (var student in sortPoints)
            {
                Console.WriteLine($"{student.Namn}, Ålder: {student.Poang} ");
            }



        }



        // -- Count --
        // Räkna hur många studenter som finns totalt och i klass A
        public void CountStudents()
        {
            int antalStudenter = _studenter.Count();
            int antalKlassA = _studenter.Count();

            Console.WriteLine($"Totalt antal studenter: {antalStudenter}");
            Console.WriteLine($"Antal studenter i klass A: {antalKlassA}");
            
        }

        // -- Take -- 
        // Ta ut de första 3 studenterna och de 2 bästa poängen
        public void TaUtPoangOchTop3Studenter()
        {
            var forsta3 = _studenter.Take(3).ToList();
            var top2 = _studenter.OrderByDescending(s => s.Poang).Take(2).ToList();

            ConsoleOutput.WriteHeader($"\nFörsta 3: ");
            foreach (var student in forsta3)
            {
                Console.WriteLine($"{student.Namn}");
            }

            ConsoleOutput.WriteHeader($"\nTop 2: ");
            foreach (var student in top2)
            {
                Console.WriteLine($"{student.Namn} : {student.Poang} Poäng");
            }
            

        }


        // -- Distinct --
        // Visa alla unika klassnamn och alla unika åldrar
        public void UnikaAldrarOchKlassnamn()
        {
            var unikAlder = _studenter.Select(s => s.Alder).Distinct().ToList();
            var unikaKlasser= _studenter.Select(s => s.Klass).Distinct().ToList();
            
            
            ConsoleOutput.WriteHeader($"\nUnik ålder: ");
            foreach (var student in unikAlder)
            {
                Console.WriteLine($"{student}");
            }

            ConsoleOutput.WriteHeader($"\nUnika klasser: ");
            foreach (var klass in unikaKlasser)
            {
                Console.WriteLine($"{klass}");
            }
            
        }


        // -- Any / All --
        //Finns det någon student med 100 poäng och har alla studenter minst 50 poäng?

        public void AnyStudentOver100AndAllStudentsOver50()
        {
            bool finns100 = _studenter.Any(s => s.Poang == 100);   
            bool allaMinst50 = _studenter.All(s => s.Poang >= 50);

            Console.WriteLine($"Finns någon med 100 poäng? {(finns100 ? "Ja" : "Nej")}");
            Console.WriteLine($"Har alla minst 50 poäng? {(allaMinst50 ? "Ja" : "Nej")}");
        }

        // -- FirstOrDefault / Last / Single --
        // Hämta första studenten i klass B, Hämta sista studenten i listan och hämta studenten med ett visst Id.
        
        public void hamtaMassaStudenter()
        {
            Student? forstaKlassB = _studenter.FirstOrDefault(s => s.Klass == "B");
            Student? id3 = _studenter.SingleOrDefault(s => s.Id == 3);
            Student lastStudent = _studenter.Last();

            Console.WriteLine($"Första elev i klass B: {forstaKlassB.Namn}");
            Console.WriteLine($"Elev på ID 3: {id3.Namn}");
            Console.WriteLine($"Eleven som är sist i listan: {lastStudent.Namn}");
        }

        // -- GroupBy --
        // Gruppera studenter per klass. Skriv ut varje klass och namnen i gruppen
        public void GrupperaPerKlassOchSkrivUtVarjeKlassOchNamn()
        {
            var grupperaPerKlass = _studenter.GroupBy(s => s.Klass);
            foreach (var grupp in grupperaPerKlass)
            {
                ConsoleOutput.WriteHeader($"Klass {grupp.Key}:");

                foreach (var student in grupp)
                {
                    Console.WriteLine($"  {student.Namn}");
                }
            }

        }


        // -- Sum / Average / Min / Max --
        // Summera alla poäng, Räkna ut medelpoäng och hitta lägsta och högsta poäng.

        public void SumRaknaUtMedelOchHittaMinMax()
        {
            int sum = _studenter.Sum(s => s.Poang);
            double average = _studenter.Average(s => s.Poang);
            int min = _studenter.Min(s => s.Poang);
            int max = _studenter.Max(s => s.Poang);

            Console.WriteLine();
            ConsoleOutput.WriteHeader($"Summa poäng: {sum}");
            ConsoleOutput.WriteWarning($"Medelpoäng: {average:F1}");
            ConsoleOutput.WriteError($"Lägsta poäng: {min}");
            ConsoleOutput.WriteSuccess($"Högsta poäng: {max}");
        }

        // skriva ut
        public void SkrivUtAlla()
        {
            ConsoleOutput.WriteHeader($"\nAlla studenter: ");
            foreach (var s in _studenter)
            {
                Console.WriteLine($"{s.Id}. {s.Namn}, {s.Alder} år, klass {s.Klass}, {s.Poang} poäng");
            }
        }

        // skriva ut dscending
        public void SkrivUtAllaFallande()
        {
            ConsoleOutput.WriteHeader($"\nAlla studenter (Ö-A): ");
            foreach (var s in _studenter.OrderByDescending(s => s.Namn))
            {
                Console.WriteLine($"{s.Id}. {s.Namn}, {s.Alder} år, klass {s.Klass}, {s.Poang} poäng");
            }
        }

        // skriva ut alla ascending
        public void SkrivUtAllaStigande()
        {
            ConsoleOutput.WriteHeader($"\nAlla studenter (A-Ö): ");
            foreach (var s in _studenter.OrderBy(s => s.Namn))
            {
                Console.WriteLine($"{s.Id}. {s.Namn}, {s.Alder} år, klass {s.Klass}, {s.Poang} poäng");
            }
        }


    }
}
