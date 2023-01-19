// Autor: Dominik Bujnowicz
// Zadanie: 4 Analizator składniowy wykorzystujący wyrażenia regularne
// Wersja programu: na ocenę dobrą

using System;
using PCRE;

class Program
{
    // Przekształcone wyrażenie regularne w standardzie PCRE
    static readonly PcreRegex regexPattern = new PcreRegex
    (@"(?x)^(?&multipleExpressions)$
    (?(DEFINE)
        (?<multipleExpressions>(?&expression)(;(?&expression))*)
        (?<expression>(?&parenthesesNumber)([-+*/^](?&parenthesesNumber))+)
        (?<parenthesesNumber>(-?\d+(\.\d+)?)|(\((?&expression)\)))
    )
    ");

    static void Main(string[] args)
    {
        Console.WriteLine();
        Console.WriteLine("Program sprawdzający poprawność wyrażenia arytmetycznego");
        Console.WriteLine();
        while (true)
        {
            Console.WriteLine("Wpisz wyrażenie arytmetyczne (pojedyńcze lub wiele rozdzielonych średnikiem):");
            string? input = Console.ReadLine();
            if (input == null)
            {
                Console.WriteLine("Puste wyrażenie!");
                continue;
            }
            // Sprawdzanie czy wpisane wyrażenie arytmetyczne jest zgodne z wyrażeniem regularnym
            Console.WriteLine("Wynik analizy: " + (regexPattern.IsMatch(input) ? "Wyrażenie poprawne" : "Wyrażenie niepoprawne"));
            Console.WriteLine();
        }
    }
}
