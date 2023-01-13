// Autor: Dominik Bujnowicz
// Zadanie: 1 Automat Deterministyczny
// Wersja programu: na ocenę bardzo dobrą

// Wyjątek obsługujący niepoprawny (nie znajdujący się w alfabecie DFA) symbol wprowadzony przez użytkownika.
[Serializable]
public class InvalidSymbolException : Exception
{
    public InvalidSymbolException() : base() { }
    public InvalidSymbolException(string message) : base(message) { }
    public InvalidSymbolException(string message, Exception inner) : base(message, inner) { }
}

// Wyjątek obsługujący nieistniejące przejście w tablicy przejść.
[Serializable]
public class UndefinedTransitionException : Exception
{
    public UndefinedTransitionException() : base() { }
    public UndefinedTransitionException(string message) : base(message) { }
    public UndefinedTransitionException(string message, Exception inner) : base(message, inner) { }
}

// Klasa reprezentująca symbol alfabetu.
public class Symbol
{
    public Symbol(string Name, string Content, string Description)
    {
        this.Name = Name;
        this.Content = Content;
        this.Description = Description;
    }

    public string Name { get; }
    public string Content { get; }
    public string Description { get; }
}

// Klasa reprezentująca stan automatu.
public class State
{
    public State(string Name, string Description)
    {
        this.Name = Name;
        this.Description = Description;
    }

    public string Name { get; }
    public string Description { get; }
}

// Klasa reprezentująca automat deterministyczny.
public class DFA
{
    public DFA(
        List<State> States,
        List<Symbol> Alphabet,
        State InitialState,
        List<State> AcceptingStates,
        Dictionary<State, Dictionary<Symbol, State>> TransitionTable)
    {
        this.States = States;
        this.Alphabet = Alphabet;
        this.InitialState = InitialState;
        this.AcceptingStates = AcceptingStates;
        this.TransitionTable = TransitionTable;

        this.CurrentState = this.InitialState;
        this.StateHistory.Add(this.CurrentState);
    }

    public List<State> States { get; } // Wszystkie możliwe stany
    public List<Symbol> Alphabet { get; } // Alfabet
    public State InitialState { get; } // Początkowy stan
    public List<State> AcceptingStates { get; } // Akceptujące stany
    public Dictionary<State, Dictionary<Symbol, State>> TransitionTable { get; } // Tablica przejść
    public State CurrentState { get; set; } // Obecny stan
    public List<State> StateHistory { get; set; } = new List<State>(); // Historia stanów

    // Metoda pokazująca wszystkie symbole obsługiwane (istniejące w alfabecie) przez automat.
    public void printPossibleSymbols()
    {
        Console.WriteLine("Możliwe symbole (\"symbol\" -> opis symbolu):");
        foreach (Symbol symbol in this.Alphabet) Console.WriteLine($"\"{symbol.Content}\" -> {symbol.Description}");
        Console.WriteLine();
    }

    // Metoda pokazująca stan, w którym znajduje się obecnie automat.
    public void printCurrentState()
    {
        Console.WriteLine($"Aktualny stan: {this.CurrentState.Name}");
        Console.WriteLine($"Opis Stanu:");
        Console.WriteLine(this.CurrentState.Description);
        Console.WriteLine();
    }

    // Metoda pokazująca w kolejności chronologicznej wszystkie stany, w których znajdował się automat.
    public void printStateHistory()
    {
        Console.WriteLine("Historia przejść stanów:");
        if (this.StateHistory.Count > 0) Console.Write(this.StateHistory[0].Name);
        else Console.WriteLine("Brak stanów!");
        for (int i = 1; i < this.StateHistory.Count; ++i) Console.Write($" -> {this.StateHistory[i].Name}");
        Console.WriteLine();
    }

    // Metoda sprawdzająca czy stan, w którym znajduje się obecnie automat jest stanem akceptującym.
    public bool isCurrentStateAccepting()
    {
        return this.AcceptingStates.Any(acceptingState => acceptingState == this.CurrentState);
    }

    // Funkcja przejścia automatu. Akceptuje wprowadzony przez użytkownika tekst.
    public void transition(string? input)
    {
        // Na podstawie wprowadzonego tekstu zostaje znaleziony odpowiedni symbol alfabetu.
        Symbol? inputSymbol = this.Alphabet.Find(alphabetSymbol => alphabetSymbol.Content == input);
        // W przypadku gdy wprowadzony tekst nie odpowiada żadnemu symbolowi, funkcja wyrzuca wyjątek.
        if (inputSymbol == null) throw new InvalidSymbolException($"\"{input}\" nie znajduje się w alfabecie automatu!");

        // Na podstawie obecnego stanu automatu i wprowadzonego symbolu, z tabeli przejść zostaje wyczytany następny stan automatu.
        // W przypadku gdy z tabeli przejść nie uda się wyczytać następnego stanu, funkcja wyrzuca wyjątek.
        if (this.TransitionTable.TryGetValue(this.CurrentState, out Dictionary<Symbol, State>? stateTransitions))
        {
            if (stateTransitions == null) throw new InvalidSymbolException($"Przejścia dla stanu {this.CurrentState.Name} nie są zdefiniowane!");
            if (stateTransitions.TryGetValue(inputSymbol, out State? nextState))
            {
                if (nextState == null) throw new InvalidSymbolException($"Przejście ze stanu {this.CurrentState.Name} i symbolu {inputSymbol.Name} nie jest zdefiniowane!");
                // Automat zmienia obecny stan na stan wyczytany z tabeli przejść i dodaje go do historii stanów automatu.
                this.CurrentState = nextState;
                this.StateHistory.Add(this.CurrentState);
            }
        }
    }
}

class Program
{
    // Możliwe stany automatu.
    static readonly State q_0 = new State("q_0", "Suma wrzuconych monet 0zł");
    static readonly State q_1 = new State("q_1", "Suma wrzuconych monet 1zł");
    static readonly State q_1_return1 = new State("q_1_return1", "Suma wrzuconych monet 1zł\nZwróć 1zł");
    static readonly State q_2 = new State("q_2", "Suma wrzuconych monet 2zł");
    static readonly State q_2_return2 = new State("q_2_return2", "Suma wrzuconych monet 2zł\nZwróć 2zł");
    static readonly State q_3 = new State("q_3", "Suma wrzuconych monet 3zł");
    static readonly State q_3_return3 = new State("q_3_return3", "Suma wrzuconych monet 3zł\nZwróć 3zł");
    static readonly State q_4 = new State("q_4", "Suma wrzuconych monet 4zł");
    static readonly State q_4_return4 = new State("q_4_return4", "Suma wrzuconych monet 4zł\nZwróć 4zł");
    static readonly State q_5 = new State("q_5", "Suma wrzuconych monet 5zł");
    static readonly State q_5_return5 = new State("q_5_return5", "Suma wrzuconych monet 5zł\nZwróć 5zł");
    static readonly State q_6 = new State("q_6", "Suma wrzuconych monet 6zł");
    static readonly State q_6_return6 = new State("q_6_return6", "Suma wrzuconych monet 6zł\nZwróć 6zł");
    static readonly State q_7 = new State("q_7", "Suma wrzuconych monet 7zł");
    static readonly State q_7_return7 = new State("q_7_return7", "Suma wrzuconych monet 7zł\nZwróć 7zł");
    static readonly State q_8 = new State("q_8", "Suma wrzuconych monet 8zł");
    static readonly State q_8_return8 = new State("q_8_return8", "Suma wrzuconych monet 8zł\nZwróć 8zł");
    static readonly State q_9 = new State("q_9", "Suma wrzuconych monet 9zł");
    static readonly State q_9_return9 = new State("q_9_return9", "Suma wrzuconych monet 9zł\nZwróć 9zł");
    static readonly State q_10 = new State("q_10", "Suma wrzuconych monet 10zł");
    static readonly State q_10_return10 = new State("q_10_return10", "Suma wrzuconych monet 10zł\nZwróć 10zł");
    static readonly State q_11 = new State("q_11", "Suma wrzuconych monet 11zł");
    static readonly State q_11_return11 = new State("q_11_return11", "Suma wrzuconych monet 11zł\nZwróć 11zł");
    static readonly State q_12 = new State("q_12", "Suma wrzuconych monet 12zł");
    static readonly State q_12_return12 = new State("q_12_return12", "Suma wrzuconych monet 12zł\nZwróć 12zł");
    static readonly State q_13 = new State("q_13", "Suma wrzuconych monet 13zł");
    static readonly State q_13_return13 = new State("q_13_return13", "Suma wrzuconych monet 13zł\nZwróć 13zł");
    static readonly State q_14 = new State("q_14", "Suma wrzuconych monet 14zł");
    static readonly State q_14_return14 = new State("q_14_return14", "Suma wrzuconych monet 14zł\nZwróć 14zł");
    static readonly State q_15 = new State("q_15", "Suma wrzuconych monet 15zł");
    static readonly State q_15_return15 = new State("q_15_return15", "Suma wrzuconych monet 15zł\nZwróć 15zł");
    static readonly State q_15_basic = new State("q_15_basic", "Suma wrzuconych monet 15zł\nWydaj bilet na mycie podstawowe");
    static readonly State q_16 = new State("q_16", "Suma wrzuconych monet 16zł");
    static readonly State q_16_return16 = new State("q_16_return16", "Suma wrzuconych monet 16zł\nZwróć 16zł");
    static readonly State q_16_basic_change1 = new State("q_16_basic_change1", "Suma wrzuconych monet 16zł\nWydaj bilet na mycie podstawowe\nWydaj resztę 1zł");
    static readonly State q_17 = new State("q_17", "Suma wrzuconych monet 17zł");
    static readonly State q_17_return17 = new State("q_17_return17", "Suma wrzuconych monet 17zł\nZwróć 17zł");
    static readonly State q_17_basic_change2 = new State("q_17_basic_change2", "Suma wrzuconych monet 17zł\nWydaj bilet na mycie podstawowe\nWydaj resztę 2zł");
    static readonly State q_18 = new State("q_18", "Suma wrzuconych monet 18zł");
    static readonly State q_18_return18 = new State("q_18_return18", "Suma wrzuconych monet 18zł\nZwróć 18zł");
    static readonly State q_18_basic_change3 = new State("q_18_basic_change3", "Suma wrzuconych monet 18zł\nWydaj bilet na mycie podstawowe\nWydaj resztę 3zł");
    static readonly State q_19 = new State("q_19", "Suma wrzuconych monet 19zł");
    static readonly State q_19_return19 = new State("q_19_return19", "Suma wrzuconych monet 19zł\nZwróć 19zł");
    static readonly State q_19_basic_change4 = new State("q_19_basic_change4", "Suma wrzuconych monet 19zł\nWydaj bilet na mycie podstawowe\nWydaj resztę 4zł");
    static readonly State q_20 = new State("q_20", "Suma wrzuconych monet 20zł");
    static readonly State q_20_return20 = new State("q_20_return20", "Suma wrzuconych monet 20zł\nZwróć 20zł");
    static readonly State q_20_basic_change5 = new State("q_20_basic_change5", "Suma wrzuconych monet 20zł\nWydaj bilet na mycie podstawowe\nWydaj resztę 5zł");
    static readonly State q_20_wax = new State("q_20_wax", "Suma wrzuconych monet 20zł\nWydaj bilet na mycie z woskowaniem");
    static readonly State q_21_return1 = new State("q_21_return1", "Suma wrzuconych monet 21zł\nZwróc 1zł");
    static readonly State q_22_return2 = new State("q_22_return2", "Suma wrzuconych monet 22zł\nZwróc 2zł");
    static readonly State q_23_return3 = new State("q_23_return3", "Suma wrzuconych monet 23zł\nZwróc 3zł");
    static readonly State q_24_return4 = new State("q_24_return4", "Suma wrzuconych monet 24zł\nZwróc 4zł");
    static readonly State q_25_return5 = new State("q_25_return5", "Suma wrzuconych monet 25zł\nZwróc 5zł");

    // Lista możliwych stanów automatu.
    static readonly List<State> states = new List<State>
    {
        q_0,
        q_1,
        q_1_return1,
        q_2,
        q_2_return2,
        q_3,
        q_3_return3,
        q_4,
        q_4_return4,
        q_5,
        q_5_return5,
        q_6,
        q_6_return6,
        q_7,
        q_7_return7,
        q_8,
        q_8_return8,
        q_9,
        q_9_return9,
        q_10,
        q_10_return10,
        q_11,
        q_11_return11,
        q_12,
        q_12_return12,
        q_13,
        q_13_return13,
        q_14,
        q_14_return14,
        q_15,
        q_15_return15,
        q_15_basic,
        q_16,
        q_16_return16,
        q_16_basic_change1,
        q_17,
        q_17_return17,
        q_17_basic_change2,
        q_18,
        q_18_return18,
        q_18_basic_change3,
        q_19,
        q_19_return19,
        q_19_basic_change4,
        q_20,
        q_20_return20,
        q_20_basic_change5,
        q_20_wax,
        q_21_return1,
        q_22_return2,
        q_23_return3,
        q_24_return4,
        q_25_return5
    };

    // Symbole należące do alfabetu automatu.
    static readonly Symbol s_1 = new Symbol("s_1", "1", "Moneta 1zł");
    static readonly Symbol s_2 = new Symbol("s_2", "2", "Moneta 2zł");
    static readonly Symbol s_5 = new Symbol("s_5", "5", "Moneta 5zł");
    static readonly Symbol s_return = new Symbol("s_return", "zwrot", "Zwrot zapłaconej kwoty");
    static readonly Symbol s_basic = new Symbol("s_basic", "podstawowe", "Mycie podstawowe");
    static readonly Symbol s_wax = new Symbol("s_wax", "woskowanie", "Mycie z woskowaniem");

    // Alfabet automatu.
    static readonly List<Symbol> alphabet = new List<Symbol> { s_1, s_2, s_5, s_return, s_basic, s_wax };

    // Początkowy stan automatu.
    static readonly State initialState = q_0;

    // Stany akceptujące automatu.
    static readonly List<State> acceptingStates = new List<State>
    {
        q_1_return1,
        q_2_return2,
        q_3_return3,
        q_4_return4,
        q_5_return5,
        q_6_return6,
        q_7_return7,
        q_8_return8,
        q_9_return9,
        q_10_return10,
        q_11_return11,
        q_12_return12,
        q_13_return13,
        q_14_return14,
        q_15_return15,
        q_15_basic,
        q_16_return16,
        q_16_basic_change1,
        q_17_return17,
        q_17_basic_change2,
        q_18_return18,
        q_18_basic_change3,
        q_19_return19,
        q_19_basic_change4,
        q_20_return20,
        q_20_basic_change5
    };

    // Tabela przejść automatu, zapisana przy użyciu słownika C#.
    // Każdy stan ma przypisany do siebie kolejny słownik,
    // w którym dla każdego symbolu jest przypisany odpowiedni następny stan automatu.    
    static readonly Dictionary<State, Dictionary<Symbol, State>> transitionTable = new Dictionary<State, Dictionary<Symbol, State>>
    {
        {
            q_0,
            new Dictionary<Symbol, State>
            {
                { s_1, q_1 },
                { s_2, q_2 },
                { s_5, q_5 },
                { s_return, q_0 },
                { s_basic, q_0 },
                { s_wax, q_0 }
            }
        },
        {
            q_1,
            new Dictionary<Symbol, State>
            {
                { s_1, q_2 },
                { s_2, q_3 },
                { s_5, q_6 },
                { s_return, q_1_return1 },
                { s_basic, q_1 },
                { s_wax, q_1 }
            }
        },
        {
            q_1_return1,
            new Dictionary<Symbol, State>
            {
                { s_1, q_1_return1 },
                { s_2, q_1_return1 },
                { s_5, q_1_return1 },
                { s_return, q_1_return1 },
                { s_basic, q_1_return1 },
                { s_wax, q_1_return1 }
            }
        },
        {
            q_2,
            new Dictionary<Symbol, State>
            {
                { s_1, q_3 },
                { s_2, q_4 },
                { s_5, q_7 },
                { s_return, q_2_return2 },
                { s_basic, q_2 },
                { s_wax, q_2 }
            }
        },
        {
            q_2_return2,
            new Dictionary<Symbol, State>
            {
                { s_1, q_2_return2 },
                { s_2, q_2_return2 },
                { s_5, q_2_return2 },
                { s_return, q_2_return2 },
                { s_basic, q_2_return2 },
                { s_wax, q_2_return2 }
            }
        },
        {
            q_3,
            new Dictionary<Symbol, State>
            {
                { s_1, q_4 },
                { s_2, q_5 },
                { s_5, q_8 },
                { s_return, q_3_return3 },
                { s_basic, q_3 },
                { s_wax, q_3 }
            }
        },
        {
            q_3_return3,
            new Dictionary<Symbol, State>
            {
                { s_1, q_3_return3 },
                { s_2, q_3_return3 },
                { s_5, q_3_return3 },
                { s_return, q_3_return3 },
                { s_basic, q_3_return3 },
                { s_wax, q_3_return3 }
            }
        },
        {
            q_4,
            new Dictionary<Symbol, State>
            {
                { s_1, q_5 },
                { s_2, q_6 },
                { s_5, q_9 },
                { s_return, q_4_return4 },
                { s_basic, q_4 },
                { s_wax, q_4 }
            }
        },
        {
            q_4_return4,
            new Dictionary<Symbol, State>
            {
                { s_1, q_4_return4 },
                { s_2, q_4_return4 },
                { s_5, q_4_return4 },
                { s_return, q_4_return4 },
                { s_basic, q_4_return4 },
                { s_wax, q_4_return4 }
            }
        },
        {
            q_5,
            new Dictionary<Symbol, State>
            {
                { s_1, q_6 },
                { s_2, q_7 },
                { s_5, q_10 },
                { s_return, q_5_return5 },
                { s_basic, q_5 },
                { s_wax, q_5 }
            }
        },
        {
            q_5_return5,
            new Dictionary<Symbol, State>
            {
                { s_1, q_5_return5 },
                { s_2, q_5_return5 },
                { s_5, q_5_return5 },
                { s_return, q_5_return5 },
                { s_basic, q_5_return5 },
                { s_wax, q_5_return5 }
            }
        },
        {
            q_6,
            new Dictionary<Symbol, State>
            {
                { s_1, q_7 },
                { s_2, q_8 },
                { s_5, q_11 },
                { s_return, q_6_return6 },
                { s_basic, q_6 },
                { s_wax, q_6 }
            }
        },
        {
            q_6_return6,
            new Dictionary<Symbol, State>
            {
                { s_1, q_6_return6 },
                { s_2, q_6_return6 },
                { s_5, q_6_return6 },
                { s_return, q_6_return6 },
                { s_basic, q_6_return6 },
                { s_wax, q_6_return6 }
            }
        },
        {
            q_7,
            new Dictionary<Symbol, State>
            {
                { s_1, q_8 },
                { s_2, q_9 },
                { s_5, q_12 },
                { s_return, q_7_return7 },
                { s_basic, q_7 },
                { s_wax, q_7 }
            }
        },
        {
            q_7_return7,
            new Dictionary<Symbol, State>
            {
                { s_1, q_7_return7 },
                { s_2, q_7_return7 },
                { s_5, q_7_return7 },
                { s_return, q_7_return7 },
                { s_basic, q_7_return7 },
                { s_wax, q_7_return7 }
            }
        },
        {
            q_8,
            new Dictionary<Symbol, State>
            {
                { s_1, q_9 },
                { s_2, q_10 },
                { s_5, q_13 },
                { s_return, q_8_return8 },
                { s_basic, q_8 },
                { s_wax, q_8 }
            }
        },
        {
            q_8_return8,
            new Dictionary<Symbol, State>
            {
                { s_1, q_8_return8 },
                { s_2, q_8_return8 },
                { s_5, q_8_return8 },
                { s_return, q_8_return8 },
                { s_basic, q_8_return8 },
                { s_wax, q_8_return8 }
            }
        },
        {
            q_9,
            new Dictionary<Symbol, State>
            {
                { s_1, q_10 },
                { s_2, q_11 },
                { s_5, q_14 },
                { s_return, q_9_return9 },
                { s_basic, q_9 },
                { s_wax, q_9 }
            }
        },
        {
            q_9_return9,
            new Dictionary<Symbol, State>
            {
                { s_1, q_9_return9 },
                { s_2, q_9_return9 },
                { s_5, q_9_return9 },
                { s_return, q_9_return9 },
                { s_basic, q_9_return9 },
                { s_wax, q_9_return9 }
            }
        },
        {
            q_10,
            new Dictionary<Symbol, State>
            {
                { s_1, q_11 },
                { s_2, q_12 },
                { s_5, q_15 },
                { s_return, q_10_return10 },
                { s_basic, q_10 },
                { s_wax, q_10 }
            }
        },
        {
            q_10_return10,
            new Dictionary<Symbol, State>
            {
                { s_1, q_10_return10 },
                { s_2, q_10_return10 },
                { s_5, q_10_return10 },
                { s_return, q_10_return10 },
                { s_basic, q_10_return10 },
                { s_wax, q_10_return10 }
            }
        },
        {
            q_11,
            new Dictionary<Symbol, State>
            {
                { s_1, q_12 },
                { s_2, q_13 },
                { s_5, q_16 },
                { s_return, q_11_return11 },
                { s_basic, q_11 },
                { s_wax, q_11 }
            }
        },
        {
            q_11_return11,
            new Dictionary<Symbol, State>
            {
                { s_1, q_11_return11 },
                { s_2, q_11_return11 },
                { s_5, q_11_return11 },
                { s_return, q_11_return11 },
                { s_basic, q_11_return11 },
                { s_wax, q_11_return11 }
            }
        },
        {
            q_12,
            new Dictionary<Symbol, State>
            {
                { s_1, q_13 },
                { s_2, q_14 },
                { s_5, q_17 },
                { s_return, q_12_return12 },
                { s_basic, q_12 },
                { s_wax, q_12 }
            }
        },
        {
            q_12_return12,
            new Dictionary<Symbol, State>
            {
                { s_1, q_12_return12 },
                { s_2, q_12_return12 },
                { s_5, q_12_return12 },
                { s_return, q_12_return12 },
                { s_basic, q_12_return12 },
                { s_wax, q_12_return12 }
            }
        },
        {
            q_13,
            new Dictionary<Symbol, State>
            {
                { s_1, q_14 },
                { s_2, q_15 },
                { s_5, q_18 },
                { s_return, q_13_return13 },
                { s_basic, q_13 },
                { s_wax, q_13 }
            }
        },
        {
            q_13_return13,
            new Dictionary<Symbol, State>
            {
                { s_1, q_13_return13 },
                { s_2, q_13_return13 },
                { s_5, q_13_return13 },
                { s_return, q_13_return13 },
                { s_basic, q_13_return13 },
                { s_wax, q_13_return13 }
            }
        },
        {
            q_14,
            new Dictionary<Symbol, State>
            {
                { s_1, q_15 },
                { s_2, q_16 },
                { s_5, q_19 },
                { s_return, q_14_return14 },
                { s_basic, q_14 },
                { s_wax, q_14 }
            }
        },
        {
            q_14_return14,
            new Dictionary<Symbol, State>
            {
                { s_1, q_14_return14 },
                { s_2, q_14_return14 },
                { s_5, q_14_return14 },
                { s_return, q_14_return14 },
                { s_basic, q_14_return14 },
                { s_wax, q_14_return14 }
            }
        },
        {
            q_15,
            new Dictionary<Symbol, State>
            {
                { s_1, q_16 },
                { s_2, q_17 },
                { s_5, q_20 },
                { s_return, q_15_return15 },
                { s_basic, q_15_basic },
                { s_wax, q_15 }
            }
        },
        {
            q_15_return15,
            new Dictionary<Symbol, State>
            {
                { s_1, q_15_return15 },
                { s_2, q_15_return15 },
                { s_5, q_15_return15 },
                { s_return, q_15_return15 },
                { s_basic, q_15_return15 },
                { s_wax, q_15_return15 }
            }
        },
        {
            q_15_basic,
            new Dictionary<Symbol, State>
            {
                { s_1, q_15_basic },
                { s_2, q_15_basic },
                { s_5, q_15_basic },
                { s_return, q_15_basic },
                { s_basic, q_15_basic },
                { s_wax, q_15_basic }
            }
        },
        {
            q_16,
            new Dictionary<Symbol, State>
            {
                { s_1, q_17 },
                { s_2, q_18 },
                { s_5, q_21_return1 },
                { s_return, q_16_return16 },
                { s_basic, q_16_basic_change1 },
                { s_wax, q_16 }
            }
        },
        {
            q_16_return16,
            new Dictionary<Symbol, State>
            {
                { s_1, q_16_return16 },
                { s_2, q_16_return16 },
                { s_5, q_16_return16 },
                { s_return, q_16_return16 },
                { s_basic, q_16_return16 },
                { s_wax, q_16_return16 }
            }
        },
        {
            q_16_basic_change1,
            new Dictionary<Symbol, State>
            {
                { s_1, q_16_basic_change1 },
                { s_2, q_16_basic_change1 },
                { s_5, q_16_basic_change1 },
                { s_return, q_16_basic_change1 },
                { s_basic, q_16_basic_change1 },
                { s_wax, q_16_basic_change1 }
            }
        },
        {
            q_17,
            new Dictionary<Symbol, State>
            {
                { s_1, q_18 },
                { s_2, q_19 },
                { s_5, q_22_return2 },
                { s_return, q_17_return17 },
                { s_basic, q_17_basic_change2 },
                { s_wax, q_17 }
            }
        },
        {
            q_17_return17,
            new Dictionary<Symbol, State>
            {
                { s_1, q_17_return17 },
                { s_2, q_17_return17 },
                { s_5, q_17_return17 },
                { s_return, q_17_return17 },
                { s_basic, q_17_return17 },
                { s_wax, q_17_return17 }
            }
        },
        {
            q_17_basic_change2,
            new Dictionary<Symbol, State>
            {
                { s_1, q_17_basic_change2 },
                { s_2, q_17_basic_change2 },
                { s_5, q_17_basic_change2 },
                { s_return, q_17_basic_change2 },
                { s_basic, q_17_basic_change2 },
                { s_wax, q_17_basic_change2 }
            }
        },
        {
            q_18,
            new Dictionary<Symbol, State>
            {
                { s_1, q_19 },
                { s_2, q_20 },
                { s_5, q_23_return3 },
                { s_return, q_18_return18 },
                { s_basic, q_18_basic_change3 },
                { s_wax, q_18 }
            }
        },
        {
            q_18_return18,
            new Dictionary<Symbol, State>
            {
                { s_1, q_18_return18 },
                { s_2, q_18_return18 },
                { s_5, q_18_return18 },
                { s_return, q_18_return18 },
                { s_basic, q_18_return18 },
                { s_wax, q_18_return18 }
            }
        },
        {
            q_18_basic_change3,
            new Dictionary<Symbol, State>
            {
                { s_1, q_18_basic_change3 },
                { s_2, q_18_basic_change3 },
                { s_5, q_18_basic_change3 },
                { s_return, q_18_basic_change3 },
                { s_basic, q_18_basic_change3 },
                { s_wax, q_18_basic_change3 }
            }
        },
        {
            q_19,
            new Dictionary<Symbol, State>
            {
                { s_1, q_20 },
                { s_2, q_21_return1 },
                { s_5, q_24_return4 },
                { s_return, q_19_return19 },
                { s_basic, q_19_basic_change4 },
                { s_wax, q_19 }
            }
        },
        {
            q_19_return19,
            new Dictionary<Symbol, State>
            {
                { s_1, q_19_return19 },
                { s_2, q_19_return19 },
                { s_5, q_19_return19 },
                { s_return, q_19_return19 },
                { s_basic, q_19_return19 },
                { s_wax, q_19_return19 }
            }
        },
        {
            q_19_basic_change4,
            new Dictionary<Symbol, State>
            {
                { s_1, q_19_basic_change4 },
                { s_2, q_19_basic_change4 },
                { s_5, q_19_basic_change4 },
                { s_return, q_19_basic_change4 },
                { s_basic, q_19_basic_change4 },
                { s_wax, q_19_basic_change4 }
            }
        },
        {
            q_20,
            new Dictionary<Symbol, State>
            {
                { s_1, q_21_return1 },
                { s_2, q_22_return2 },
                { s_5, q_25_return5 },
                { s_return, q_20_return20 },
                { s_basic, q_20_basic_change5 },
                { s_wax, q_20_wax }
            }
        },
        {
            q_20_return20,
            new Dictionary<Symbol, State>
            {
                { s_1, q_20_return20 },
                { s_2, q_20_return20 },
                { s_5, q_20_return20 },
                { s_return, q_20_return20 },
                { s_basic, q_20_return20 },
                { s_wax, q_20_return20 }
            }
        },
        {
            q_20_basic_change5,
            new Dictionary<Symbol, State>
            {
                { s_1, q_20_basic_change5 },
                { s_2, q_20_basic_change5 },
                { s_5, q_20_basic_change5 },
                { s_return, q_20_basic_change5 },
                { s_basic, q_20_basic_change5 },
                { s_wax, q_20_basic_change5 }
            }
        },
        {
            q_20_wax,
            new Dictionary<Symbol, State>
            {
                { s_1, q_20_wax },
                { s_2, q_20_wax },
                { s_5, q_20_wax },
                { s_return, q_20_wax },
                { s_basic, q_20_wax },
                { s_wax, q_20_wax }
            }
        },
        {
            q_21_return1,
            new Dictionary<Symbol, State>
            {
                { s_1, q_21_return1 },
                { s_2, q_22_return2 },
                { s_5, q_25_return5 },
                { s_return, q_20_return20 },
                { s_basic, q_20_basic_change5 },
                { s_wax, q_20_wax }
            }
        },
        {
            q_22_return2,
            new Dictionary<Symbol, State>
            {
                { s_1, q_21_return1 },
                { s_2, q_22_return2 },
                { s_5, q_25_return5 },
                { s_return, q_20_return20 },
                { s_basic, q_20_basic_change5 },
                { s_wax, q_20_wax }
            }
        },
        {
            q_23_return3,
            new Dictionary<Symbol, State>
            {
                { s_1, q_21_return1 },
                { s_2, q_22_return2 },
                { s_5, q_25_return5 },
                { s_return, q_20_return20 },
                { s_basic, q_20_basic_change5 },
                { s_wax, q_20_wax }
            }
        },
        {
            q_24_return4,
            new Dictionary<Symbol, State>
            {
                { s_1, q_21_return1 },
                { s_2, q_22_return2 },
                { s_5, q_25_return5 },
                { s_return, q_20_return20 },
                { s_basic, q_20_basic_change5 },
                { s_wax, q_20_wax }
            }
        },
        {
            q_25_return5,
            new Dictionary<Symbol, State>
            {
                { s_1, q_21_return1 },
                { s_2, q_22_return2 },
                { s_5, q_25_return5 },
                { s_return, q_20_return20 },
                { s_basic, q_20_basic_change5 },
                { s_wax, q_20_wax }
            }
        }
    };

    static void Main()
    {
        // Inicjalizacja obiektu reprezentującego automat DFA.
        DFA dfa = new DFA(states, alphabet, initialState, acceptingStates, transitionTable);

        // Pokazanie obsługiwanych symboli i stanu początkowego.
        Console.WriteLine("Automat pobierający opłaty w myjni samochodowej");
        Console.WriteLine();
        dfa.printPossibleSymbols();
        dfa.printCurrentState();

        // Główna pętla programu obsługująca input użytkownika.
        while (true)
        {
            // Wczytanie inputu użytkownika.
            Console.WriteLine("----------------------------------------");
            string? input = Console.ReadLine();
            Console.WriteLine();

            try
            {
                // Uruchomienie funkcji przejścia automatu.
                dfa.transition(input);
            }
            catch (InvalidSymbolException e)
            {
                // Użytkownik wprowadził niepoprawny symbol.
                Console.WriteLine(e.Message);
                Console.WriteLine();
                dfa.printPossibleSymbols();
                dfa.printCurrentState();
                continue;
            }
            catch (UndefinedTransitionException e)
            {
                // Nie znaleziono przejścia.
                Console.WriteLine(e.Message);
                return;
            }

            // Pokazanie obencego stanu automatu.
            dfa.printCurrentState();

            // Wyjście z pętli w przypadku stanu akceptującego.
            if (dfa.isCurrentStateAccepting()) break;
        }

        // Pokazanie historii stanów automatu.
        Console.WriteLine("Automat zakończył działanie!");
        Console.WriteLine();
        dfa.printStateHistory();
    }
}
