namespace CatAndMouse
{
    enum State
    {
        Winner,
        Looser,
        Playing,
        NotInGame
    }
    class Player
    {
        public string name;
        public int location;
        public State state = State.NotInGame;
        public int distanceTraveled = 0;

        public Player(string name)
        {
            this.name = name;
            this.location = -1;
        }


        public void Move(int steps, int boardSize = 10000)
        {
            if (state == State.NotInGame)
            {
                location = steps;
                state = State.Playing;
                return;
            }

            int dir = steps > 0 ? 1 : -1;
            int count = steps > 0 ? steps : -steps;

            for (int i = 0; i < count; i++)
            {
                location += dir;
                if (location > boardSize)
                {
                    location = 1;
                }
                if (location < 1)
                {
                    location = boardSize;
                }
                distanceTraveled++;
            }
        }
    }
}