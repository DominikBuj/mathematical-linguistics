// Autor: Dominik Bujnowicz
// Zadanie: 2 Automat Niedeterministyczny
// Wersja programu: na ocenę dobrą

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

class Program
{
    // Możliwe stany automatu.
    static readonly State q0 = new State("q0", "Słowo jest puste");
    static readonly State q1 = new State("q1", "W słowie znajduje się podsłowo \"0\"");
    static readonly State q2 = new State("q2", "W słowie znajduje się podsłowo \"1\"");
    static readonly State q3 = new State("q3", "W słowie znajduje się podsłowo \"2\"");
    static readonly State q4 = new State("q4", "W słowie znajduje się podsłowo \"3\"");
    static readonly State q5 = new State("q5", "W słowie znajduje się podsłowo \"a\"");
    static readonly State q6 = new State("q6", "W słowie znajduje się podsłowo \"b\"");
    static readonly State q7 = new State("q7", "W słowie znajduje się podsłowo \"c\"");
    static readonly State q8 = new State("q8", "W słowie znajduje się podsłowo \"00\"");
    static readonly State q9 = new State("q9", "W słowie znajduje się podsłowo \"11\"");
    static readonly State q10 = new State("q10", "W słowie znajduje się podsłowo \"22\"");
    static readonly State q11 = new State("q11", "W słowie znajduje się podsłowo \"33\"");
    static readonly State q12 = new State("q12", "W słowie znajduje się podsłowo \"aa\"");
    static readonly State q13 = new State("q13", "W słowie znajduje się podsłowo \"bb\"");
    static readonly State q14 = new State("q14", "W słowie znajduje się podsłowo \"cc\"");
    static readonly State q15 = new State("q15", "W słowie wystąpiło potrojenie składające się z cyfr");
    static readonly State q16 = new State("q16", "W słowie wystąpiło potrojenie składające się z liter");

    // Lista możliwych stanów automatu.
    static readonly List<State> states = new List<State>
    {
        q0,
        q1,
        q2,
        q3,
        q4,
        q5,
        q6,
        q7,
        q8,
        q9,
        q10,
        q11,
        q12,
        q13,
        q14,
        q15,
        q16
    };

    // Alfabet automatu.
    static readonly List<char> alphabet = new List<char> { '0', '1', '2', '3', 'a', 'b', 'c' };

    // Początkowy stan automatu.
    static readonly State initialState = q0;

    // Stany akceptujące automatu.
    static readonly List<State> acceptingStates = new List<State>
    {
        q15,
        q16
    };

    // Tabela przejść automatu.
    // Każdy stan dla obsługiwanych symboli ma przypisaną listę kolejnych stanów automatu.
    static readonly Dictionary<State, Dictionary<char, List<State>>> transitionTable = new Dictionary<State, Dictionary<char, List<State>>>
    {
        {
            q0,
            new Dictionary<char, List<State>>
            {
                { '0', new List<State> { q0, q1 } },
                { '1', new List<State> { q0, q2 } },
                { '2', new List<State> { q0, q3 } },
                { '3', new List<State> { q0, q4 } },
                { 'a', new List<State> { q0, q5 } },
                { 'b', new List<State> { q0, q6 } },
                { 'c', new List<State> { q0, q7 } },
            }
        },
        {
            q1,
            new Dictionary<char, List<State>>
            {
                { '0', new List<State> { q8 } }
            }
        },
        {
            q2,
            new Dictionary<char, List<State>>
            {
                { '1', new List<State> { q9 } }
            }
        },
        {
            q3,
            new Dictionary<char, List<State>>
            {
                { '2', new List<State> { q10 } }
            }
        },
        {
            q4,
            new Dictionary<char, List<State>>
            {
                { '3', new List<State> { q11 } }
            }
        },
        {
            q5,
            new Dictionary<char, List<State>>
            {
                { 'a', new List<State> { q12 } }
            }
        },
        {
            q6,
            new Dictionary<char, List<State>>
            {
                { 'b', new List<State> { q13 } }
            }
        },
        {
            q7,
            new Dictionary<char, List<State>>
            {
                { 'c', new List<State> { q14 } }
            }
        },
        {
            q8,
            new Dictionary<char, List<State>>
            {
                { '0', new List<State> { q15 } }
            }
        },
        {
            q9,
            new Dictionary<char, List<State>>
            {
                { '1', new List<State> { q15 } }
            }
        },
        {
            q10,
            new Dictionary<char, List<State>>
            {
                { '2', new List<State> { q15 } }
            }
        },
        {
            q11,
            new Dictionary<char, List<State>>
            {
                { '3', new List<State> { q15 } }
            }
        },
        {
            q12,
            new Dictionary<char, List<State>>
            {
                { 'a', new List<State> { q16 } }
            }
        },
        {
            q13,
            new Dictionary<char, List<State>>
            {
                { 'b', new List<State> { q16 } }
            }
        },
        {
            q14,
            new Dictionary<char, List<State>>
            {
                { 'c', new List<State> { q16 } }
            }
        },
        {
            q15,
            new Dictionary<char, List<State>>
            {
                { '0', new List<State> { q15 } },
                { '1', new List<State> { q15 } },
                { '2', new List<State> { q15 } },
                { '3', new List<State> { q15 } },
                { 'a', new List<State> { q15 } },
                { 'b', new List<State> { q15 } },
                { 'c', new List<State> { q15 } },
            }
        },
        {
            q16,
            new Dictionary<char, List<State>>
            {
                { '0', new List<State> { q16 } },
                { '1', new List<State> { q16 } },
                { '2', new List<State> { q16 } },
                { '3', new List<State> { q16 } },
                { 'a', new List<State> { q16 } },
                { 'b', new List<State> { q16 } },
                { 'c', new List<State> { q16 } },
            }
        },
    };

    // Klasa reprezentująca automat niedeterministyczny.
    public class NFA
    {
        public NFA(
            List<State> States,
            List<char> Alphabet,
            State InitialState,
            List<State> AcceptingStates,
            Dictionary<State, Dictionary<char, List<State>>> TransitionTable)
        {
            this.States = States;
            this.Alphabet = Alphabet;
            this.InitialState = InitialState;
            this.AcceptingStates = AcceptingStates;
            this.TransitionTable = TransitionTable;
        }

        public List<State> States { get; } // Wszystkie możliwe stany
        public List<char> Alphabet { get; } // Alfabet
        public State InitialState { get; } // Początkowy stan
        public List<State> AcceptingStates { get; } // Akceptujące stany
        public Dictionary<State, Dictionary<char, List<State>>> TransitionTable { get; } // Tablica przejść
        public List<State> CurrentStates { get; set; } = new List<State>(); // Obecny stan (stany)
        public List<List<State>> StatesHistory { get; set; } = new List<List<State>>(); // Historia stanów

        public string getCurrentStates()
        {
            string currentStates = "{";
            for (int i = 0; i < this.CurrentStates.Count; ++i) currentStates += (i != 0 ? ", " : "") + this.CurrentStates[i].Name;
            currentStates += "}";
            return $"Aktualny stan (stany) = {currentStates}";
        }

        // Ilość wystąpień potrojeń cyfr/liczb jest obliczona na podstawie ilości wystąpień poszczególnych stanów akceptujących
        public void printAnalysisResults()
        {
            int tripleDigitsCounter = this.CurrentStates.Count(state => state == q15);
            int tirpleLetterCounter = this.CurrentStates.Count(state => state == q16);
            Console.WriteLine($"Ilość wystąpienia potrojenia cyfr = {tripleDigitsCounter}");
            Console.WriteLine($"Ilość wystąpienia potrojenia liter = {tirpleLetterCounter}");
        }

        public void printStatesHistory()
        {
            string statesHistory = "{";
            for (int i = 0; i < this.StatesHistory.Count; ++i)
            {
                List<State> stateHistory = this.StatesHistory[i];
                string states = "{";
                for (int j = 0; j < stateHistory.Count; ++j) states += (j != 0 ? ", " : "") + stateHistory[j].Name;
                states += "}";
                statesHistory += (i != 0 ? ", " : "") + states;
            }
            statesHistory += "}";
            Console.WriteLine($"Historia stanów = {statesHistory}");
            Console.WriteLine();
        }

        public void analyzeWord(string word)
        {
            this.CurrentStates = new List<State> { this.InitialState };
            this.StatesHistory = new List<List<State>> { new List<State>(this.CurrentStates) };
            Console.WriteLine($"Analizowane słowo = \"{word}\" {this.getCurrentStates()}");
            foreach (char symbol in word)
            {
                this.transition(symbol);
                Console.WriteLine($"Wczytany symbol = \'{symbol}\' {this.getCurrentStates()}");
            }
            this.printAnalysisResults();
            this.printStatesHistory();
        }

        // Funkcja przejścia automatu. Akceptuje pojedyńczy symbol (obecny stan/stany są zapisane w klasie).
        public void transition(char symbol)
        {
            foreach (State state in this.CurrentStates.ToList())
            {
                // Stan dla którego nie ma przypisanego następnego stanu/stanów jest usuwany.
                this.CurrentStates.Remove(state);
                if (this.TransitionTable.TryGetValue(state, out Dictionary<char, List<State>>? stateTransitions))
                {
                    if (stateTransitions.TryGetValue(symbol, out List<State>? nextStates))
                    {
                        // Z tabli przejść jest wyczytany następny stan/stany.
                        foreach (State nextState in nextStates) this.CurrentStates.Add(nextState);
                    }
                }
            }
            // Stan/stany są dodawne do historii stanów.
            this.StatesHistory.Add(new List<State>(this.CurrentStates));
        }
    }

    static void Main(string[] args)
    {
        Console.WriteLine();
        Console.WriteLine("Program analizujący potrójne występowanie symboli w wyrazach");
        Console.WriteLine();

        string fileText = System.IO.File.ReadAllText($@"{Environment.CurrentDirectory}\words.txt");
        string[] fileWords = fileText.Split('#');

        Console.WriteLine("Wczytano plik!");
        Console.WriteLine();

        NFA nfa = new NFA(states, alphabet, initialState, acceptingStates, transitionTable);

        foreach (string word in fileWords) nfa.analyzeWord(word);

        Console.WriteLine("Automat zakończył działanie!");
        Console.WriteLine();
    }
}
