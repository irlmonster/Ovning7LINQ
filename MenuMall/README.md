# Övning 7 - LINQ

En konsolapp i C#, övningar på LINQ

Har återanvänt min meny från garageövningarna (som nu är en universalmeny)

## Funktioner

- (Where) Äldre än 20år och mer än 80 poäng.
- (Select) Visa bara namn och namn och poäng i anonym typ
- (OrderBy) Sortera efter ålder och sortera efter poäng, högst först
- (Count) Räkna hur många studenter som finns och hur många som går i klass A
- (Take) Ta ut de tre första studenterna och ta ut de två bästa poängen efter sortering
- (Distinct) Visa alla unika klassnamn och visa alla unika åldrar.
- (Any/All) Finns någon student med 100p? Har alla studenter minst 50p?
- (FirstOrDefault / Last / Single) Hämta första i klass B, sista i listan och visst id
- (GroupBy) Gruppera per klass och varje klass + namn i gruppen.
- (Sum / Average / MIN / MAX) Summera alla poäng, räkna ut medelpoäng och lägsta och högsta poäng.
- En undermeny för att skriva ut Ascending eller Descending

## Struktur

```
MenuMall.slnx
MenuMall/
  ├── Exercises/      Metoder med övningar
  ├── Models/         Student
  ├── UI/             ConsoleMenu, ConsoleInput, ConsoleOutput
  ├── Reflection/     ValueParser
  └── Program.cs      Testdata och meny

```

## Kom igång

```bash
git clone https://github.com/irlmonster/Ovning7LINQ.git
cd Ovning7LINQ
dotnet run --project MenuMall
```