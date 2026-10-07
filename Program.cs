class Program
{
    static void Main(string[] args)
    {
        //через статические поля класса Game можно задать путь к
        //входным и выходным файлам
        Game.InputFile = "1.ChaseData.txt";
        Game.OutFile = "1.PursuitLog.txt";

        Game game = new Game(16);
        game.Run(); //запуск игры и вывод результатов
    }
}