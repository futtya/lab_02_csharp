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
        while (state != GameState.End)
        {
        }
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