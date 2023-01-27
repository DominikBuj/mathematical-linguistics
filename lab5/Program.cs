// Autor: Dominik Bujnowicz
// Zadanie: 5 Analizator składniowy LL(1)

using System;
using System.Collections.Generic;

// Wyjątek niepopranego symbolu w wyrażeniu
[Serializable]
public class InvalidSymbolException : Exception
{
    public InvalidSymbolException() : base() { }
    public InvalidSymbolException(string message) : base(message) { }
    public InvalidSymbolException(string message, Exception inner) : base(message, inner) { }
}

class Program
{
    // Zbiory symboli pierwszych wszystkich możliwych produkcji
    // null reprezentuje symbol pusty
    static readonly List<char?> firstS = new List<char?> { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '(' };
    static readonly List<char?> firstZ = new List<char?> { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '(', null };
    static readonly List<char?> firstW = new List<char?> { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '(' };
    static readonly List<char?> firstWPrim = new List<char?> { null, '*', ':', '+', '-', '^' };
    static readonly List<char?> firstP = new List<char?> { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '(' };
    static readonly List<char?> firstR = new List<char?> { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
    static readonly List<char?> firstRPrim = new List<char?> { null, '.' };
    static readonly List<char?> firstL = new List<char?> { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
    static readonly List<char?> firstLPrim = new List<char?> { null, '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
    static readonly List<char?> firstC = new List<char?> { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
    static readonly List<char?> firstO = new List<char?> { '*', ':', '+', '-', '^' };

    class GrammarParser
    {

        public GrammarParser(string expression)
        {
            this.expression = expression;
            this.position = 0;
            this.next = (this.expression.Length > 0) ? expression[this.position] : null;
        }

        public string expression { get; set; } // Wyrażenie arytmetyczne
        public int position { get; set; } // Pozycja w wyrażeniu
        public char? next { get; set; } // Następny wczytany symbol

        // Wczytuje następny symbol wyrażenia
        public void ReadNext()
        {
            this.next = ((this.position + 1) >= this.expression.Length) ? null : this.expression[++this.position];
        }

        // Produkcja S (korzeń gramatyki)
        public bool ReadS()
        {
            if (firstW.Contains(this.next))
            {
                this.ReadW();
                if (this.next == ';')
                {
                    this.ReadNext();
                    if (firstS.Contains(this.next))
                    {
                        return this.ReadS();
                    }
                    else return true;
                }
                else throw new InvalidSymbolException("Wyrażenie arytmetyczne nie jest zakończone ;!");
            }
            else throw new InvalidSymbolException("Wczytano niepoprawny symbol pierwszy produkcji S!");
        }

        // Produkcja W
        public bool ReadW()
        {
            if (firstP.Contains(this.next))
            {
                this.ReadP();
                if (firstO.Contains(this.next))
                {
                    this.ReadO();
                    return this.ReadW();
                }
                else return true;
            }
            else throw new InvalidSymbolException("Wczytano niepoprawny symbol pierwszy produkcji W!");
        }

        // Produkcja P
        public bool ReadP()
        {
            if (firstR.Contains(this.next))
            {
                return this.ReadR();
            }
            else if (this.next == '(')
            {
                this.ReadNext();
                this.ReadW();
                if (this.next != ')') throw new InvalidSymbolException("Niedomknięty nawias w wyrażeniu!");
                this.ReadNext();
                return true;
            }
            else throw new InvalidSymbolException("Wczytano niepoprawny symbol pierwszy produkcji P!");
        }

        // Produkcja R
        public bool ReadR()
        {
            if (firstL.Contains(this.next))
            {
                this.ReadL();
                return this.ReadRPrim();
            }
            else throw new InvalidSymbolException("Wczytano niepoprawny symbol pierwszy produkcji R!");
        }

        // Produkcja R'
        public bool ReadRPrim()
        {
            if (this.next == '.')
            {
                this.ReadNext();
                return this.ReadL();
            }
            else return true;
        }

        // Produkcja L
        public bool ReadL()
        {
            if (firstC.Contains(this.next))
            {
                this.ReadC();
                if (firstL.Contains(this.next))
                {
                    return this.ReadL();
                }
                else return true;
            }
            else throw new InvalidSymbolException("Wczytano niepoprawny symbol pierwszy produkcji L!");
        }

        // Produkcja C
        public bool ReadC()
        {
            if (firstC.Contains(this.next))
            {
                this.ReadNext();
                return true;
            }
            else throw new InvalidSymbolException("Wczytano niepoprawny symbol pierwszy produkcji C!");
        }

        // Produkcja O
        public bool ReadO()
        {
            if (firstO.Contains(this.next))
            {
                this.ReadNext();
                return true;
            }
            else throw new InvalidSymbolException("Wczytano niepoprawny symbol pierwszy produkcji O!");
        }
    }

    static void Main(string[] args)
    {
        Console.WriteLine();
        Console.WriteLine("Program sprawdzający poprawność wyrażenia arytmetycznego za pomocą analizatora składniowego LL(1)");
        Console.WriteLine();
        while (true)
        {
            Console.WriteLine("Obsługiwane operacje: (*, :, +, -, ^)");
            Console.WriteLine("Wyrażenia muszą być zakończone ;");
            Console.WriteLine("Wpisz wyrażenie/wyrażenia arytmetyczne:");
            string? input = Console.ReadLine();
            Console.WriteLine();
            if (input == null)
            {
                Console.WriteLine("Puste wyrażenie!");
                Console.WriteLine();
                continue;
            }
            GrammarParser grammarParser = new GrammarParser(input);
            try
            {
                grammarParser.ReadS(); // Sprawdzanie czy wyrażenie jest zgodne z gramatyką
                Console.WriteLine("Wyrażenie arytmetyczne jest zgodne z gramatyką!");
                Console.WriteLine();
            }
            catch (InvalidSymbolException e)
            {
                Console.WriteLine("Wyrażenie arytmetyczne nie jest zgodne z gramatyką!");
                Console.WriteLine(e.Message);
                Console.WriteLine();
            }
        }
    }
}
