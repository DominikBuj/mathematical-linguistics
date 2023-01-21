// Autor: Dominik Bujnowicz
// Zadanie: 3 Maszyna Turinga
// Wersja programu: na ocenę dobrą

using System;
using System.Collections.Generic;
using System.Linq;

public class State
{
    public State(string name, string description)
    {
        this.Name = name;
        this.Description = description;
    }

    public string Name { get; }
    public string Description { get; }
}

public class TM
{
    public TM(
        List<State> states,
        List<char> alphabet,
        char blankSymbol,
        List<char> inputSymbols,
        State initialState,
        List<State> acceptingStates,
        Dictionary<State, Dictionary<char, Object[]>> transitionTable)
    {
        this.States = states;
        this.Alphabet = alphabet;
        this.BlankSymbol = blankSymbol;
        this.InputSymbols = inputSymbols;
        this.InitialState = initialState;
        this.AcceptingStates = acceptingStates;
        this.TransitionTable = transitionTable;
    }

    public List<State> States { get; } // Wszystkie możliwe stany
    public List<char> Alphabet { get; } // Alfabet
    public char BlankSymbol { get; } // Pusty symbol
    public List<char> InputSymbols { get; } // Zbiór symboli wejściowych
    public State InitialState { get; } // Początkowy stan
    public List<State> AcceptingStates { get; } // Akceptujące stany
    public Dictionary<State, Dictionary<char, object[]>> TransitionTable { get; } // Tablica przejść
    public State? CurrentState { get; set; } // Obecny stan
    public List<State>? StateHistory { get; set; } // Historia stanów
    public char[]? Tape { get; set; } // Taśma
    public int TapePosition { get; set; } // Pozycja na taśmie

    public void printNumber()
    {
        if (this.Tape == null) return;
        for (int i = (this.Tape[0] == '#' ? 1 : 0); i < this.Tape.Length; ++i) Console.Write(this.Tape[i]);
        Console.WriteLine();
    }

    public void printTape()
    {
        if (this.Tape == null) return;
        string symbolArrow = "        ";
        for (int difference = 0; difference < this.TapePosition; ++difference) symbolArrow += " ";
        symbolArrow += "V";
        Console.WriteLine(symbolArrow);
        Console.WriteLine($"Taśma = {new string(this.Tape)}");
    }

    public void printStateHistory()
    {
        if (this.StateHistory == null) return;
        Console.WriteLine("Historia przejść stanów:");
        if (this.StateHistory.Count > 0) Console.Write(this.StateHistory[0].Name);
        else Console.WriteLine("Brak stanów!");
        for (int i = 1; i < this.StateHistory.Count; ++i) Console.Write($" -> {this.StateHistory[i].Name}");
        Console.WriteLine();
        Console.WriteLine();
    }

    // Funkcja przejścia automatu. Akceptuje pojedyńczy symbol (obecny stan jest zapisany w klasie).
    public void transition(char symbol)
    {
        if (this.CurrentState == null || this.Tape == null || this.StateHistory == null) return;
        if (this.TransitionTable.TryGetValue(this.CurrentState, out Dictionary<char, object[]>? stateTransitions))
        {
            if (stateTransitions.TryGetValue(symbol, out object[]? operations))
            {
                // Z tabli przejść jest wyczytany wpisywany symbol, następny stan i kierunek ruchu taśmy.
                // W przypadku wyczytania '-' maszyna nie wpisuje symbolu / nie zmienia stanu / nie zmienia pozycji na taśmie.
                if ((char)operations[0] != '-')
                {
                    this.Tape[this.TapePosition] = (char)operations[0];
                    Console.WriteLine($"Symbol zapisany na taśmie = {(char)operations[0]}");
                }
                else Console.WriteLine("Symbol zapisany na taśmie = Bez zmian");
                if (operations[1].GetType() != typeof(char) || (char)operations[1] != '-')
                {
                    this.CurrentState = (State)operations[1];
                    Console.WriteLine($"Następny stan = {((State)operations[1]).Name}");
                }
                else Console.WriteLine("Następny stan = Bez zmian");
                if ((char)operations[2] != '-')
                {
                    switch ((char)operations[2])
                    {
                        case 'L':
                            {
                                Console.WriteLine("Kierunek ruchu głowicy = Lewo");
                                --this.TapePosition;
                                break;
                            }
                        case 'R':
                            {
                                Console.WriteLine("Kierunek ruchu głowicy = Prawo");
                                ++this.TapePosition;
                                break;
                            }
                    }
                }
                else Console.WriteLine("Kierunek ruchu głowicy = Bez ruchu");
                Console.WriteLine();
                // Stan jest dodany do historii stanów.
                this.StateHistory.Add(this.CurrentState);
            }
        }
    }

    public void analyzeBinaryNumber(char[] tape)
    {
        this.CurrentState = this.InitialState;
        this.StateHistory = new List<State> { this.CurrentState };
        this.Tape = tape;
        this.TapePosition = this.Tape.Length - 1;

        if (this.CurrentState == null) return;

        while (true)
        {
            Console.WriteLine($"Aktualny stan = {this.CurrentState.Name}");
            this.printTape();
            char symbol = this.Tape[this.TapePosition];
            Console.WriteLine($"Wczytany symbol = \'{symbol}\'");
            this.transition(symbol);
            if (symbol == this.BlankSymbol) break;
        }
    }
}

class Program
{
    // Możliwe stany automatu
    static readonly State q0 = new State("q0", "Pozycja pierwszej cyfry");
    static readonly State q1 = new State("q1", "Pozycja drugiej cyfry bez przeniesienia");
    static readonly State q2 = new State("q2", "Pozycja drugiej cyfry z przeniesieniem");
    static readonly State q3 = new State("q3", "Pozycja trzeciej lub dalszej cyfry bez przeniesienia");
    static readonly State q4 = new State("q4", "Pozycja trzeciej lub dalszej cyfry z przeniesieniem");
    static readonly State q5 = new State("q5", "Niepoprawna liczba binarna");

    // Lista możliwych stanów automatu
    static readonly List<State> states = new List<State>
    {
        q0,
        q1,
        q2,
        q3,
        q4,
        q5
    };

    // Alfabet taśmy automatu
    static readonly List<char> alphabet = new List<char> { '0', '1', '#' };

    // Pusty symbol.
    static readonly char blankSymbol = '#';

    // Zbiór symboli wejściowych
    static readonly List<char> inputSymbols = new List<char> { '0', '1' };

    // Początkowy stan automatu
    static readonly State initialState = q0;

    // Stany akceptujące automatu
    static readonly List<State> acceptingStates = new List<State>
    {
        q3,
        q4
    };

    // Tabela przejść automatu
    static readonly Dictionary<State, Dictionary<char, Object[]>> transitionTable = new Dictionary<State, Dictionary<char, Object[]>>
    {
        {
            q0,
            new Dictionary<char, Object[]>
            {
                { '0', new Object[3] { '1', q1, 'L' } },
                { '1', new Object[3] { '0', q2, 'L' } },
                { '#', new Object[3] { '-', q5, '-' } }
            }
        },
        {
            q1,
            new Dictionary<char, Object[]>
            {
                { '0', new Object[3] { '1', q3, 'L' } },
                { '1', new Object[3] { '0', q4, 'L' } },
                { '#', new Object[3] { '-', q5, '-' } }
            }
        },
        {
            q2,
            new Dictionary<char, Object[]>
            {
                { '0', new Object[3] { '-', q4, 'L' } },
                { '1', new Object[3] { '-', q4, 'L' } },
                { '#', new Object[3] { '-', q5, '-' } }
            }
        },
        {
            q3,
            new Dictionary<char, Object[]>
            {
                { '0', new Object[3] { '-', q3, 'L' } },
                { '1', new Object[3] { '-', q3, 'L' } },
                { '#', new Object[3] { '-', q3, '-' } }
            }
        },
        {
            q4,
            new Dictionary<char, Object[]>
            {
                { '0', new Object[3] { '1', q3, 'L' } },
                { '1', new Object[3] { '0', q4, 'L' } },
                { '#', new Object[3] { '1', q4, '-' } }
            }
        },
        {
            q5,
            new Dictionary<char, Object[]>
            {
                { '0', new Object[3] { '-', '-', 'L' } },
                { '1', new Object[3] { '-', '-', 'L' } },
                { '#', new Object[3] { '-', '-', '-' } }
            }
        }
    };

    static void Main(string[] args)
    {
        Console.WriteLine();
        Console.WriteLine("Program zwiększający wielocyfrową liczbę binarną o 3");
        Console.WriteLine();

        TM tm = new TM(states, alphabet, blankSymbol, inputSymbols, initialState, acceptingStates, transitionTable);

        while (true)
        {
            char[] tape;
            while (true)
            {
                Console.Write("Wpisz liczbę binarną: ");
                string? input = Console.ReadLine();

                if (input != null && input.All(symbol => inputSymbols.Contains(symbol)))
                {
                    tape = new char[input.Length + 1];
                    tape[0] = blankSymbol;
                    for (int i = 1, j = 0; i < tape.Length && j < input.Length; ++i, ++j) tape[i] = input[j];
                    break;
                }

                Console.WriteLine("Niepoprawna liczba binarna!");
                Console.WriteLine();
            }
            Console.WriteLine();

            tm.analyzeBinaryNumber(tape);
            Console.Write("Wynik dodawania = ");
            tm.printNumber();
            Console.WriteLine();
            tm.printStateHistory();

            Console.WriteLine("Maszyna zakończyła działanie!");
            Console.WriteLine();
        }
    }
}
