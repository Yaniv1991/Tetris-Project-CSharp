using System;

namespace TetrisProject
{

    class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            Game game = new Game();
            game.Start();

            Console.WriteLine("Press enter to quit the game");
            Console.Read();
        }
    }
}
