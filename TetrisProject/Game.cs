using System;
using System.Threading;

namespace TetrisProject
{

    class Game
    {
        public bool stillPlayingTheGame = true;
        public int Level { get; private set; }
        public int Score { get; private set; }

        int levelUpCounter = 12;
        int levelUpLimiter = 12;
        const int maxLevel = 13;

        Player player;
        IUserInteractor userInteractor;
        public void Start()
        {
            player = new Player
            {
                Start = DateTime.Now
            };
            Board board = new Board(25, 10, this);
            player.ReadName();

            Thread AcceptMovement = new Thread(board.PlayerMovement);
            AcceptMovement.Start();

            while (stillPlayingTheGame)
            {
                board.Print();
                Thread.Sleep(800 - (Level * 50));

                board.MoveBlockDown();
                board.Update();
            }

            Console.Clear();
            Console.WriteLine("Game over!!");
            EndSequence();
        }

        private void EndSequence()
        {
            player.End = DateTime.Now;

            player.PlayerScore = Score;
            //player.PlayerScore = 6800;
            PlayerData data = new PlayerData(@"C:\Tetris\Tetris.txt");
            data.AddNewScore(player);
            data.WriteToFile();
            Console.Clear();
            userInteractor.PrintScores(data);

        }

        public void End()
        {
            stillPlayingTheGame = false;
        }

        public void AddScore(int LinesCleared)
        {
            switch (LinesCleared)
            {
                case 1:
                    Score += 100 * Level;
                    break;
                case 2:
                    Score += 300 * Level;
                    break;
                case 3:
                    Score += 500 * Level;
                    break;
                case 4:
                    Score += 800 * Level;
                    break;
            }
            levelUpCounter -= LinesCleared;
            if(levelUpCounter <= 0 && Level< maxLevel)
            {
                levelUpCounter = levelUpLimiter;
                Level++;
            }
        }


        public Game()
        {
            Level = 1;
            userInteractor = ConsoleInteractor.Single();
        }
    }
}
