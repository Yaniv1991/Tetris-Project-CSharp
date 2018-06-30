using System;

namespace TetrisProject
{
    class Player
    {
        public string PlayerUsername { get; private set; }
        public int PlayerScore { get; set; }

        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public void ReadName()
        {
            ConsoleInteractor.Single().AcceptUsername();
        }

        public Player()
        {

        }

        public Player(string name, int score)
        {
            PlayerUsername = name;
            PlayerScore = score;
        }

    }
}
