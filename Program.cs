
namespace CatAndMouse
{
    class Program
    {
        static void Main(string[] args)
        {
            Game.InputFile = "1.ChaseData.txt";
            Game.OutFile = "1.PursuitLog.txt";
            Game game1 = new Game(16);
            game1.Run();

            Game.InputFile = "2.ChaseData.txt";
            Game.OutFile = "2.PursuitLog.txt";
            Game game2 = new Game(20);
            game2.Run();

            Game.InputFile = "3.ChaseData.txt";
            Game.OutFile = "3.PursuitLog.txt";
            Game game3 = new Game(27);
            game3.Run();
        }
    }
}