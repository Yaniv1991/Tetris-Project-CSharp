using System;

namespace TetrisProject
{
    interface IUserInteractor
    {
        string AcceptUsername();
        void PrintScores(PlayerData data);
    }

    class ConsoleInteractor : IUserInteractor
    {
        private static readonly ConsoleInteractor single = new ConsoleInteractor();
        public void PrintScores(PlayerData data)
        {
            for (int i = data.HighScores.Length - 1; i >= 0; i--)
            {
                Console.WriteLine("Name : {0} Score : {1} Start : {2} End : {3}",
                    data.HighScores[i].PlayerUsername, data.HighScores[i].PlayerScore,
                    data.HighScores[i].Start, data.HighScores[i].End);
            }
        }

        public string AcceptUsername()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Welcome to the Tetris Game, Designed & Authored by Hanna Ayoub & Yaniv Chen.\n");
            Console.ResetColor();

            string PlayerUsername;
            while (true)
            {
                Console.Write("Please insert your name ==> ");
                PlayerUsername = Console.ReadLine().Trim();

                if (PlayerUsername.Length < 1)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("No name has been inserted, Please try again putting your name.");
                    Console.ResetColor();
                    continue;
                }

                Console.WriteLine($"\nHello {PlayerUsername[0].ToString().ToUpper() + PlayerUsername.Substring(1)}, Have fun playing this Tetris Game.\n");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("------------------------------------------------------------------\n");
                Console.ResetColor();
                return PlayerUsername;
            }
        }

        public static ConsoleInteractor Single()
        {
            return single;
        }

        static ConsoleInteractor()
        {
                
        }
    }
}
