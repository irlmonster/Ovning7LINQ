using MenuMall.Models;
using System;
using System.Collections.Generic;
using System.Text;

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

            foreach (var s in aldre)
            {
                Console.WriteLine($"{s.Namn}, Ålder: {s.Alder} år");
            }
            
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

            Console.WriteLine("\nSorterade på ålder:");
            foreach (var student in sortAlder)
            {
                Console.WriteLine($"{student.Namn}, Ålder: {student.Alder} ");
            }


            Console.WriteLine("\nSorterade på poäng:");
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

            Console.WriteLine($"Våra tre första studenter är: {forsta3}");
            Console.WriteLine($"Poäng : {top2}");

        }


        // -- Distinct --
        // Visa alla unika klassnamn och alla unika åldrar
        public void UnikaAldrarOchKlassnamn()
        {
            var unikAlder = _studenter.Select(s => s.Alder).Distinct().ToList();
            var unikaKlasser= _studenter.Select(s => s.Klass).Distinct().ToList();

            Console.WriteLine($"Ålder: {unikAlder}");
            Console.WriteLine($"Unika klasser: {unikaKlasser}");
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
                Console.WriteLine($"Klass {grupp.Key}:");

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

            Console.WriteLine($"Summa poäng: {sum}");
            Console.WriteLine($"Medelpoäng: {sum:F1}");
            Console.WriteLine($"Lägsta poäng: {min}");
            Console.WriteLine($"Högsta poäng: {max}");
        }
    }
}
