// Autor: Dominik Bujnowicz
// Zadanie: 3 Maszyna Turinga
// Wersja programu: na ocenę dobrą

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
        State initialState,
        List<State> acceptingStates,
        Dictionary<State, Dictionary<char, Object[]>> transitionTable,
        char blankSymbol)
    {
        this.States = states;
        this.Alphabet = alphabet;
        this.InitialState = initialState;
        this.AcceptingStates = acceptingStates;
        this.TransitionTable = transitionTable;
        this.BlankSymbol = blankSymbol;

        this.CurrentState = this.InitialState;
        this.StateHistory.Add(this.CurrentState);
    }

    public List<State> States { get; }
    public List<char> Alphabet { get; }
    public State InitialState { get; }
    public List<State> AcceptingStates { get; }
    public Dictionary<State, Dictionary<char, object[]>> TransitionTable { get; }
    public char BlankSymbol { get; }
    public State CurrentState { get; set; } // Obecny stan
    public List<State> StateHistory { get; } = new List<State>(); // Historia stanów
    public char[]? Tape { get; set; }
    public int TapePosition { get; set; }

    public void printTape()
    {
        if (this.Tape == null) return;
        Console.Write("Liczba binarna po zwiększeniu = ");
        for (int i = (this.Tape[0] == '#' ? 1 : 0); i < this.Tape.Length; ++i) Console.Write(this.Tape[i]);
        Console.WriteLine();
    }

    public void printStateHistory()
    {
        Console.WriteLine("Historia przejść stanów:");
        if (this.StateHistory.Count > 0) Console.Write(this.StateHistory[0].Name);
        else Console.WriteLine("Brak stanów!");
        for (int i = 1; i < this.StateHistory.Count; ++i) Console.Write($" -> {this.StateHistory[i].Name}");
        Console.WriteLine();
        Console.WriteLine();
    }

    public void transition(char symbol)
    {
        if (this.Tape == null) return;
        if (this.TransitionTable.TryGetValue(this.CurrentState, out Dictionary<char, object[]>? stateTransitions))
        {
            if (stateTransitions.TryGetValue(symbol, out object[]? operations))
            {
                if ((char)operations[0] != '-') this.Tape[this.TapePosition] = (char)operations[0];
                if (operations[1].GetType() != typeof(char) || (char)operations[1] != '-') this.CurrentState = (State)operations[1];
                if ((char)operations[2] != '-')
                {
                    switch ((char)operations[2])
                    {
                        case 'L':
                        {
                            --this.TapePosition;
                            break;
                        }
                        case 'R':
                        {
                            ++this.TapePosition;
                            break;
                        }
                    }
                }
                this.StateHistory.Add(this.CurrentState);
            }
        }
    }

    public void analyzeBinaryNumber(char[] tape)
    {
        Console.WriteLine($"Początkowy stan = {this.CurrentState.Name}");

        this.Tape = tape;
        this.TapePosition = this.Tape.Length - 1;

        while (true)
        {
            char symbol = this.Tape[this.TapePosition];
            Console.Write($"Wczytany symbol = \'{symbol}\' ");
            this.transition(symbol);
            Console.WriteLine($"Aktualny stan = {this.CurrentState.Name}");
            if (symbol == this.BlankSymbol) break;
        }
        Console.WriteLine();

        this.printTape();
        this.printStateHistory();
    }
}

class Program
{
    // Możliwe stany automatu.
    static readonly State q0 = new State("q0", "Pozycja pierwszej cyfry");
    static readonly State q1 = new State("q1", "Pozycja drugiej cyfry bez przeniesienia");
    static readonly State q2 = new State("q2", "Pozycja drugiej cyfry z przeniesieniem");
    static readonly State q3 = new State("q3", "Pozycja trzeciej lub dalszej cyfry bez przeniesienia");
    static readonly State q4 = new State("q4", "Pozycja trzeciej lub dalszej cyfry z przeniesieniem");
    static readonly State q5 = new State("q5", "Niepoprawna liczba binarna");

    // Lista możliwych stanów automatu.
    static readonly List<State> states = new List<State>
    {
        q0,
        q1,
        q2,
        q3,
        q4,
        q5
    };

    // Alfabet automatu.
    static readonly List<char> alphabet = new List<char> { '0', '1', '#' };

    // Początkowy stan automatu.
    static readonly State initialState = q0;

    // Stany akceptujące automatu.
    static readonly List<State> acceptingStates = new List<State>
    {
        q3,
        q4
    };

    // Tabela przejść automatu.
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

        TM tm = new TM(states, alphabet, initialState, acceptingStates, transitionTable, '#');

        char[] tape;
        while (true)
        {
            Console.Write("Wpisz liczbę binarną: ");
            string? input = Console.ReadLine();

            if (input != null && input.All(symbol => "01#".Contains(symbol)))
            {
                tape = new char[input.Length + 1];
                tape[0] = '#';
                for (int i = 1, j = 0; i < tape.Length && j < input.Length; ++i, ++j) tape[i] = input[j];
                break;
            }

            Console.WriteLine("Niepoprawna liczba binarna!");
            Console.WriteLine();
        }
        Console.WriteLine();

        tm.analyzeBinaryNumber(tape);

        Console.WriteLine("Maszyna zakończyła działanie!");
        Console.WriteLine();
    }
}
