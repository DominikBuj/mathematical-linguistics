public class Symbol
{
    public Symbol(string Content, string Description)
    {
        this.Content = Content;
        this.Description = Description;
    }

    public string Content { get; }
    public string Description { get; }
}

public class State
{
    public State(string Description)
    {
        this.Description = Description;
    }

    public string Description { get; }
}

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
    }

    public List<State> States { get; }
    public List<Symbol> Alphabet { get; }
    public State InitialState { get; }
    public List<State> AcceptingStates { get; }
    public Dictionary<State, Dictionary<Symbol, State>> TransitionTable { get; }

    public void printPossibleSymbols()
    {
        Console.WriteLine("Możliwe symbole: (\"symbol\" -> opis symbolu)");
        foreach (Symbol symbol in this.Alphabet) Console.WriteLine($"\"{symbol.Content}\" -> {symbol.Description}");
        Console.WriteLine();
    }

    public bool isSymbolInAlphabet(string? symbol)
    {
        return this.Alphabet.Any(alphabetSymbol => alphabetSymbol.Content == symbol);
    }
}

class Program
{
    static readonly Symbol s_1 = new Symbol("1", "Moneta 1zł");
    static readonly Symbol s_2 = new Symbol("2", "Moneta 2zł");
    static readonly Symbol s_5 = new Symbol("5", "Moneta 5zł");
    static readonly Symbol s_return = new Symbol("zwrot", "Zwrot zapłaconej kwoty");
    static readonly Symbol s_basic = new Symbol("podstawowe", "Mycie podstawowe");
    static readonly Symbol s_wax = new Symbol("woskowanie", "Mycie z woskowaniem");

    static readonly State q_0 = new State("Suma wrzuconych monet 0zł");
    static readonly State q_1 = new State("Suma wrzuconych monet 1zł");
    static readonly State q_1_return1 = new State("Suma wrzuconych monet 1zł\nZwróć 1zł");
    static readonly State q_2 = new State("Suma wrzuconych monet 2zł");
    static readonly State q_2_return2 = new State("Suma wrzuconych monet 2zł\nZwróć 2zł");
    static readonly State q_3 = new State("Suma wrzuconych monet 3zł");
    static readonly State q_3_return3 = new State("Suma wrzuconych monet 3zł\nZwróć 3zł");
    static readonly State q_4 = new State("Suma wrzuconych monet 4zł");
    static readonly State q_4_return4 = new State("Suma wrzuconych monet 4zł\nZwróć 4zł");
    static readonly State q_5 = new State("Suma wrzuconych monet 5zł");
    static readonly State q_5_return5 = new State("Suma wrzuconych monet 5zł\nZwróć 5zł");
    static readonly State q_6 = new State("Suma wrzuconych monet 6zł");
    static readonly State q_6_return6 = new State("Suma wrzuconych monet 6zł\nZwróć 6zł");
    static readonly State q_7 = new State("Suma wrzuconych monet 7zł");
    static readonly State q_7_return7 = new State("Suma wrzuconych monet 7zł\nZwróć 7zł");
    static readonly State q_8 = new State("Suma wrzuconych monet 8zł");
    static readonly State q_8_return8 = new State("Suma wrzuconych monet 8zł\nZwróć 8zł");
    static readonly State q_9 = new State("Suma wrzuconych monet 9zł");
    static readonly State q_9_return9 = new State("Suma wrzuconych monet 9zł\nZwróć 9zł");
    static readonly State q_10 = new State("Suma wrzuconych monet 10zł");
    static readonly State q_10_return10 = new State("Suma wrzuconych monet 10zł\nZwróć 10zł");
    static readonly State q_1_return11 = new State("Suma wrzuconych monet 11zł");
    static readonly State q_11_return11 = new State("Suma wrzuconych monet 11zł\nZwróć 1zł");
    static readonly State q_1_return13 = new State("Suma wrzuconych monet 12zł");
    static readonly State q_12_return12 = new State("Suma wrzuconych monet 12zł\nZwróć 12zł");
    static readonly State q_1_return15 = new State("Suma wrzuconych monet 13zł");
    static readonly State q_13_return13 = new State("Suma wrzuconych monet 13zł\nZwróć 13zł");
    static readonly State q_14 = new State("Suma wrzuconych monet 14zł");
    static readonly State q_14_return14 = new State("Suma wrzuconych monet 14zł\nZwróć 14zł");
    static readonly State q_15 = new State("Suma wrzuconych monet 15zł");
    static readonly State q_15_return15 = new State("Suma wrzuconych monet 15zł\nZwróć 15zł");
    static readonly State q_15_basic = new State("Suma wrzuconych monet 15zł\nWydaj bilet na mycie podstawowe");
    static readonly State q_16 = new State("Suma wrzuconych monet 16zł");
    static readonly State q_16_return16 = new State("Suma wrzuconych monet 16zł\nZwróć 16zł");
    static readonly State q_16_basic_change1 = new State("Suma wrzuconych monet 16zł\nWydaj bilet na mycie podstawowe\nWydaj resztę 1zł");
    static readonly State q_17 = new State("Suma wrzuconych monet 17zł");
    static readonly State q_17_return17 = new State("Suma wrzuconych monet 17zł\nZwróć 17zł");
    static readonly State q_17_basic_change2 = new State("Suma wrzuconych monet 17zł\nWydaj bilet na mycie podstawowe\nWydaj resztę 2zł");
    static readonly State q_18 = new State("Suma wrzuconych monet 18zł");
    static readonly State q_18_return18 = new State("Suma wrzuconych monet 18zł\nZwróć 18zł");
    static readonly State q_18_basic_change3 = new State("Suma wrzuconych monet 18zł\nWydaj bilet na mycie podstawowe\nWydaj resztę 3zł");
    static readonly State q_19 = new State("Suma wrzuconych monet 19zł");
    static readonly State q_19_return19 = new State("Suma wrzuconych monet 19zł\nZwróć 19zł");
    static readonly State q_19_basic_change4 = new State("Suma wrzuconych monet 19zł\nWydaj bilet na mycie podstawowe\nWydaj resztę 4zł");
    static readonly State q_20 = new State("Suma wrzuconych monet 20zł");
    static readonly State q_20_return20 = new State("Suma wrzuconych monet 20zł\nZwróć 20zł");
    static readonly State q_20_basic_change5 = new State("Suma wrzuconych monet 20zł\nWydaj bilet na mycie podstawowe\nWydaj resztę 5zł");
    static readonly State q_20_wax = new State("Suma wrzuconych monet 20zł\nWydaj bilet na mycie z woskowaniem");
    static readonly State q_21_return1 = new State("Suma wrzuconych monet 21zł\nZwróc 1zł");
    static readonly State q_22_return2 = new State("Suma wrzuconych monet 22zł\nZwróc 2zł");
    static readonly State q_23_return3 = new State("Suma wrzuconych monet 23zł\nZwróc 3zł");
    static readonly State q_24_return4 = new State("Suma wrzuconych monet 24zł\nZwróc 4zł");
    static readonly State q_25_return5 = new State("Suma wrzuconych monet 25zł\nZwróc 5zł");

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
        q_1_return11,
        q_11_return11,
        q_1_return13,
        q_12_return12,
        q_1_return15,
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

    static readonly List<Symbol> alphabet = new List<Symbol> { s_1, s_2, s_5, s_return, s_basic, s_wax };

    static readonly State initialState = q_0;

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
        q_20_basic_change5,
        q_21_return1,
        q_22_return2,
        q_23_return3,
        q_24_return4,
        q_25_return5
    };

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
                { s_5, q_1_return11 },
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
                { s_5, q_1_return13 },
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
                { s_5, q_1_return15 },
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
                { s_2, q_1_return11 },
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
                { s_1, q_1_return11 },
                { s_2, q_1_return13 },
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
            q_1_return11,
            new Dictionary<Symbol, State>
            {
                { s_1, q_1_return13 },
                { s_2, q_1_return15 },
                { s_5, q_16 },
                { s_return, q_11_return11 },
                { s_basic, q_1_return11 },
                { s_wax, q_1_return11 }
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
            q_1_return13,
            new Dictionary<Symbol, State>
            {
                { s_1, q_1_return15 },
                { s_2, q_14 },
                { s_5, q_17 },
                { s_return, q_12_return12 },
                { s_basic, q_1_return13 },
                { s_wax, q_1_return13 }
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
            q_1_return15,
            new Dictionary<Symbol, State>
            {
                { s_1, q_14 },
                { s_2, q_15 },
                { s_5, q_18 },
                { s_return, q_13_return13 },
                { s_basic, q_1_return15 },
                { s_wax, q_1_return15 }
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

        DFA dfa = new DFA(states, alphabet, initialState, acceptingStates, transitionTable);

        Console.WriteLine("Automat pobierający opłaty w myjni samochodowej");
        Console.WriteLine();
        dfa.printPossibleSymbols();

        while (true)
        {

            string? input = Console.ReadLine();
            Console.WriteLine();

            if (!dfa.isSymbolInAlphabet(input))
            {
                Console.WriteLine($"Symbol \"{input}\" nie znajduje się w alfabecie automatu.");
                Console.WriteLine();
                dfa.printPossibleSymbols();
                continue;
            }

            // Console.WriteLine("Yay!");

        }

        // Console.WriteLine("Program zakończony!");
        // Console.WriteLine($"Stan końcowy: {}");

    }
}
