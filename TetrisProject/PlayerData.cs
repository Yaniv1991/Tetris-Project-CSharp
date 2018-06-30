using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisProject
{
    class PlayerData
    {
        public string Path { get; private set; }

        public Player[] HighScores { get; private set; }

        public  int LowestScore
        {
            get
            {
                int result = HighScores[0].PlayerScore;
                for (int i = 1; i < HighScores.Length; i++)
                {
                    if (result > HighScores[i].PlayerScore)
                    {
                        result = HighScores[i].PlayerScore;
                    }
                }
                return result;
            }
        }

        public int HighestScore
        {
            get
            {
                int result = HighScores[0].PlayerScore;
                for (int i = 1; i < HighScores.Length; i++)
                {
                    if (result < HighScores[i].PlayerScore)
                    {
                        result = HighScores[i].PlayerScore;
                    }
                }
                return result;
            }
        }

        public void AddNewScore(Player player)
        {
            if (player.PlayerScore < LowestScore)
            {
                return;
            }

            for (int i = HighScores.Length-1; i >= 0; i--)
            {
                if (player.PlayerScore > HighScores[i].PlayerScore)
                {
                    for (int j = 1; j <= i; j++) // This function just shifts everyone else down one increment
                    {
                        HighScores[j - 1] = HighScores[j]; 
                    }
                    HighScores[i] = player;
                    break;
                }
            }

        }

        public void WriteToFile()
        {
            try
            {
                string dirPath = @"C:\Tetris";
                if (!Directory.Exists(dirPath))
                {
                    Directory.CreateDirectory(dirPath);
                }
                using (StreamWriter sw = new StreamWriter(Path, false))
                {
                    for (int i = 0; i < HighScores.Length; i++)
                    {
                        sw.Write("{0},{1};", HighScores[i].PlayerUsername, HighScores[i].PlayerScore);
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Could not save the highscores!!");
            }
            
        }

        public PlayerData(string path)
        {
            Path = path;
            MakeHighScoreArray();
            
        }

        private void MakeHighScoreArray()
        {
            Player[] result = new Player[5];

            if (!File.Exists(Path))
            {
                for (int i = 0; i < result.Length; i++)
                {
                    result[i] = new Player("", (i + 1) * 1000);
                }
            }
            else
            {
                string arrayAsString = File.ReadAllText(Path);
                string[] splitString = arrayAsString.Split(';');
                for (int i = 0; i < result.Length; i++)
                {
                    result[i] = new Player(splitString[i].Split(',')[0], int.Parse(splitString[i].Split(',')[1]));
                }
            }
            HighScores = result;
        }
    }
    
}
