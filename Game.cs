using CatAndMouse;

enum GameState
{
    Start,
    End
}

class Game
{
    public int size;
    public Player cat;
    public Player mouse;
    public GameState state;
    public static string InputFile = "1.ChaseData.txt";
    public static string OutFile = "1.PursuitLog.txt";

    public Game(int size)
    {
        this.size = size;
        cat = new Player("Cat");
        mouse = new Player("Mouse");
        state = GameState.Start;
    }

    public void Run()
    {
        if (!File.Exists(InputFile)) return;

        string[] lines = File.ReadAllLines(InputFile);
        if (lines.Length == 0) return;

        if (int.TryParse(lines[0].Trim(), out int parsedSize) && parsedSize > 0)
        {
            size = parsedSize;
        }

        using (StreamWriter writer = new StreamWriter(OutFile))
        {
            writer.WriteLine("Cat and Mouse");
            writer.WriteLine();
            writer.WriteLine("Cat  Mouse  Distance");
            writer.WriteLine("-------------------");

            int i = 1;
            while (state != GameState.End && i < lines.Length)
            {
                string line = lines[i].Trim();
                i++;

                if (string.IsNullOrWhiteSpace(line)) continue;

                string[] parts = line.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                char command = parts[0][0];

                if (command == 'P')
                {
                    DoPrintCommand(writer);
                }
                else if (command == 'M' || command == 'C')
                {
                    if (parts.Length > 1 && int.TryParse(parts[1], out int steps))
                    {
                        DoMoveCommand(command, steps);

                        if (cat.state == State.Playing && mouse.state == State.Playing && cat.location == mouse.location)
                        {
                            cat.state = State.Winner;
                            mouse.state = State.Looser;
                            state = GameState.End; 
                        }
                    }
                }
            }

            writer.WriteLine("-------------------");
            writer.WriteLine();
            writer.WriteLine();
            writer.WriteLine("Distance traveled:   Mouse    Cat");
            writer.WriteLine($"                       {mouse.distanceTraveled,2}     {cat.distanceTraveled,2}");
            writer.WriteLine();

            if (mouse.state == State.Looser)
            {
                writer.WriteLine($"Mouse caught at: {cat.location,2}");
            }
            else
            {
                writer.WriteLine("Mouse evaded Cat");
            }
        }

        state = GameState.End;
    }
    private void DoMoveCommand(char command, int steps)
    {
        switch (command)
        {
            case 'M': mouse.Move(steps, size); break;
            case 'C': cat.Move(steps, size); break;
        }
    }

    private int GetDistance()
    {
        if (cat.state == State.NotInGame || mouse.state == State.NotInGame)
            return -1;

        int pos = cat.location;
        int dist = 0;

        while (pos != mouse.location)
        {
            pos++;
            if (pos > size) pos = 1;
            dist++;
        }

        if (dist > size / 2)
        {
            dist = size - dist;
        }

        return dist;
    }

    private void DoPrintCommand(StreamWriter writer)
    {
        string catStr = cat.state == State.NotInGame ? "??" : cat.location.ToString();
        string mouseStr = mouse.state == State.NotInGame ? "??" : mouse.location.ToString();

        if (cat.state == State.NotInGame || mouse.state == State.NotInGame)
        {
            writer.WriteLine($"{catStr,3}    {mouseStr,3}");
        }
        else
        {
            int dist = GetDistance();
            writer.WriteLine($"{catStr,3}   {mouseStr,3}        {dist,2}");
        }
    }
}