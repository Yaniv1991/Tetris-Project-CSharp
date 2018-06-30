using System;
using System.Collections.Generic;

namespace TetrisProject
{
    interface IBoardPrinter
    {
        void Print();
    }

    /// <summary>
    /// I'll need to implement a print method that utilizes Console.Cursor instead of this method
    /// and i'll make 2 arrays - one of BrickType and one of ConsoleColors instead of using a switch for doing useless stuff
    /// </summary>
    class ConsoleBoardPrinter : IBoardPrinter
    {
        private int numberOfColumns;
        private int numberOfRows;

        private Board instance;

        public ConsoleBoardPrinter(Board board,int numOfRows,int numOfColumns)
        {
            instance = board;
            numberOfColumns = numOfColumns;
            numberOfRows = numOfRows;
        }

        public void Print()
        {
            Console.Clear();

            for (int i = 0; i < 2; i++)
            {
                Console.WriteLine();
            }

            for (int i = -7; i < numberOfColumns; i++)
            {
                if (i < 0)
                {
                    Console.Write(" ");
                }
                else
                    Console.Write("_");
            }
            Console.WriteLine();


            for (int i = 0; i < numberOfRows; i++)
            {
                for (int j = -7; j <= numberOfColumns; j++)
                {
                    if (j == -1 || j == numberOfColumns)
                        Console.Write("|");
                    else
                    {
                        if (j >= 0 && instance.GameBoard[i, j] != BrickType.Empty)
                        {
                            {
                                switch (instance.GameBoard[i, j])
                                {
                                    case BrickType.MovingBlock:
                                        Console.ForegroundColor = instance.CurrentBlock.Color;
                                        break;
                                    case BrickType.Cyan:
                                        Console.ForegroundColor = ConsoleColor.Cyan;
                                        break;
                                    case BrickType.Yellow:
                                        Console.ForegroundColor = ConsoleColor.Yellow;
                                        break;
                                    case BrickType.Red:
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        break;
                                    case BrickType.Green:
                                        Console.ForegroundColor = ConsoleColor.Green;
                                        break;
                                    case BrickType.Orange:
                                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                                        break;
                                    case BrickType.Purple:
                                        Console.ForegroundColor = ConsoleColor.Magenta;
                                        break;
                                    case BrickType.DarkPurple:
                                        Console.ForegroundColor = ConsoleColor.DarkMagenta;
                                        break;

                                }
                                Console.Write("*");
                                Console.ResetColor();
                            }
                        }

                        else
                        {
                            Console.Write(" ");
                        }
                    }

                    if (i == 3 && j == numberOfColumns)
                    {
                        Console.Write("  Level {0}", instance.Game.Level);
                    }
                    if (i == 4 && j == numberOfColumns)
                    {
                        Console.Write("  Score : {0}", instance.Game.Score);
                    }
                }
                Console.WriteLine();
            }

            for (int i = -7; i < numberOfColumns; i++)
            {
                if (i < 0)
                {
                    Console.Write(" ");
                }
                else
                    Console.Write("_");
            }
        }
    }

    /// <summary>
    /// I'll change the SpawnBlock to one that utilizes a delegate array
    /// and make a NextBlock Ishape for fun. the BoardPrinter will print it, of course
    /// and maybe i'll add a raycast to show where the block will eventually land, but i guess its the job of the BoardPrinter
    /// </summary>
    class Board
    {
        Random rnd = new Random();


        public Game Game { get; }
        public IShape CurrentBlock { get => currentBlock;}
        public BrickType[,] GameBoard { get; private set;}

        ConsoleBoardPrinter consoleBoardPrinter;

        IShape currentBlock;
        
        int numberOfColumns;
        int numberOfRows;


        //public int rowsDestoryed = 0;

        public Board(int numOfRows, int numOfColumns, Game game)
        {
            Game = game;
            numberOfRows = numOfRows;
            numberOfColumns = numOfColumns;
            GameBoard = new BrickType[numberOfRows, numberOfColumns];
            consoleBoardPrinter = new ConsoleBoardPrinter(this,numOfRows,numOfColumns);
        }

        public void MoveBlockDown()
        {
            if (currentBlock != null)
            {
                currentBlock.Move(Direction.Down);
            }
        }

        public void Print()
        {
            consoleBoardPrinter.Print();
        }

        public void Update()
        {
            if (currentBlock == null)
            {
                currentBlock = SpawnBlock();
                currentBlock.X = (numberOfColumns / 2) -1;
            }

            ClearPreviousMovingBlockPosition();
            FindMovingBlockPosition();

            if (!CanMove(Direction.Down))
            {
                MakeBlockStatic();
                currentBlock = null;
                CheckForCompleteLines();
                CheckIfGameLost();
            }
        }

        private void CheckIfGameLost()
        {
            for (int j = 0; j < numberOfColumns; j++)
            {
                if (GameBoard[0, j] != BrickType.Empty && GameBoard[0, j] != BrickType.MovingBlock)
                {
                    Game.End();
                }
            }
        }

        private void CheckForCompleteLines()
        {
            List<int> rowsToClear = new List<int>();
            int numberOfCompleteLines = 0;
            int numberOfBlocksInRow = 0;

            for (int i = 0; i < numberOfRows; i++)
            {
                numberOfBlocksInRow = 0;
                for (int j = 0; j < numberOfColumns; j++)
                {
                    if (GameBoard[i, j] != BrickType.Empty)
                    {
                        numberOfBlocksInRow++;
                    }
                    if (numberOfBlocksInRow == numberOfColumns)
                    {
                        numberOfCompleteLines++;
                        rowsToClear.Add(i);
                    }
                }
            }
            ClearRows(rowsToClear);
            Game.AddScore(numberOfCompleteLines);
        }

        private void ClearRows(List<int> rowsToClear)
        {
            foreach (var row in rowsToClear)
            {
                for (int j = 0; j < numberOfColumns; j++)
                {
                    GameBoard[row, j] = BrickType.Empty;
                }

                for (int i = row; i > 0; i--)
                {
                    for (int j = 0; j < numberOfColumns; j++)
                    {
                        if (GameBoard[i - 1, j] != BrickType.Empty)
                        {
                            BrickType brick = GameBoard[i - 1, j];
                            GameBoard[i - 1, j] = BrickType.Empty;
                            GameBoard[i, j] = brick;
                        }
                    }
                }
            }

            //Good!!!
        }

        private void ClearPreviousMovingBlockPosition()
        {
            for (int i = 0; i < numberOfRows; i++)
            {
                for (int j = 0; j < numberOfColumns; j++)
                {
                    if (GameBoard[i, j] == BrickType.MovingBlock)
                    {
                        GameBoard[i, j] = BrickType.Empty;
                    }
                }
            }
        }

        private void FindMovingBlockPosition()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (currentBlock.Positioning[i, j])
                    {
                        GameBoard[currentBlock.Y + i, currentBlock.X + j] = BrickType.MovingBlock;
                    }
                }
            }

        }

        private void MakeBlockStatic()
        {
            for (int i = 0; i < numberOfRows; i++)
            {
                for (int j = 0; j < numberOfColumns; j++)
                {
                    if (GameBoard[i, j] == BrickType.MovingBlock)
                    {
                        switch (currentBlock.Color)
                        {
                            case ConsoleColor.Magenta:
                                GameBoard[i, j] = BrickType.Purple;
                                break;

                            case ConsoleColor.Cyan:
                                GameBoard[i, j] = BrickType.Cyan;
                                break;

                            case ConsoleColor.Yellow:
                                GameBoard[i, j] = BrickType.Yellow;
                                break;

                            case ConsoleColor.Red:
                                GameBoard[i, j] = BrickType.Red;
                                break;

                            case ConsoleColor.Green:
                                GameBoard[i, j] = BrickType.Green;
                                break;

                            case ConsoleColor.DarkYellow:
                                GameBoard[i, j] = BrickType.Orange;
                                break;
                            case ConsoleColor.DarkMagenta:
                                GameBoard[i, j] = BrickType.DarkPurple;
                                break;
                        }
                        //board[i, j] = BrickType.StaticBlock;
                    }
                }
            }
        }

        IShape SpawnBlock()
        {
            while (true)
            {
                int s = rnd.Next(0, 7);
                switch (s)
                {
                    case 0:
                        return new LineBrick();
                    case 1:
                        return new RectangleBrick();
                    case 2:
                        return new LBrick();
                    case 3:
                        return new ReverseLBrick();
                    case 4:
                        return new JaggedBrick();
                    case 5:
                        return new ReverseJaggedBrick();
                    case 6:
                        return new TBrick();
                }
            }

        }

        public void PlayerMovement()
        {
            while (Game.stillPlayingTheGame)
            {
                switch (Console.ReadKey().Key)
                {
                    case (ConsoleKey.RightArrow):
                        if (CanMove(Direction.Right))
                            currentBlock.Move(Direction.Right);
                        break;

                    case (ConsoleKey.LeftArrow):

                        if (CanMove(Direction.Left))
                            currentBlock.Move(Direction.Left);
                        break;

                    case (ConsoleKey.UpArrow):
                        if (CanMove(Direction.Rotate))
                            currentBlock.Move(Direction.Rotate);
                        break;
                    case (ConsoleKey.DownArrow):
                        if (CanMove(Direction.Down))
                            currentBlock.Move(Direction.Down);
                        break;
                    case (ConsoleKey.Spacebar):
                        while (CanMove(Direction.Down))
                        {
                            currentBlock.Move(Direction.Down);
                            Update();
                        }

                        break;
                }
                Update();
            }
        }

        private bool CanMove(Direction direction)
        {
            if (currentBlock == null)
                return false;

            int maxY = Max(currentBlock.Y, true);
            int maxX = Max(currentBlock.X, false);
            int minY = Min(currentBlock.Y, true);
            int minX = Min(currentBlock.X, false);

            switch (direction)
            {
                case Direction.Down:
                    if (Max(currentBlock.Y, true) == numberOfRows - 1)
                    {
                        return false;
                    }
                    for (int i = minY; i <= maxY; i++)
                    {
                        for (int j = minX; j <= maxX; j++)
                        {
                            if (GameBoard[i, j] == BrickType.MovingBlock &&  GameBoard[i + 1, j] != BrickType.MovingBlock && GameBoard[i + 1, j] != BrickType.Empty)
                            {
                                return false;
                            }
                        }
                    }
                    break;

                case Direction.Left:
                    if (minX == 0)
                        return false;
                    for (int i = minY; i <= maxY; i++)
                    {
                        if (GameBoard[i, minX] == BrickType.MovingBlock && GameBoard[i, minX - 1] != BrickType.Empty)
                        {
                            return false;
                        }
                    }
                    break;

                case Direction.Right:
                    if (maxX == numberOfColumns - 1)
                        return false;
                    for (int i = minY; i <= maxY; i++)
                    {
                        if (GameBoard[i, maxX] == BrickType.MovingBlock && GameBoard[i, maxX + 1] != BrickType.Empty)
                        {
                            return false;
                        }
                    }
                    break;

                case Direction.Rotate:
                    return CanRotate();
            }

            return true;
        }

        private bool CanRotate()
        {
            if (Max(currentBlock.Y, true, true) > numberOfRows - 1 ||
                Max(currentBlock.X, false, true) > numberOfColumns - 1 ||
                Min(currentBlock.Y, true, true) < 0 ||
                Min(currentBlock.X, false, true) < 0)
            {
                return false;
            }

            for (int n = 0; n < 4; n++)
            {
                for (int n2 = 0; n2 < 4; n2++)
                {
                    if (currentBlock.NextPositioning[n, n2] &&
                        GameBoard[currentBlock.Y + n, currentBlock.X + n2] != BrickType.MovingBlock &&
                        GameBoard[currentBlock.Y + n, currentBlock.X + n2] != BrickType.Empty)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private int Max(int property, bool CheckingTheYAxis, bool CheckingNextPositioning = false)
        {
            int result = 0;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (CheckingTheYAxis)
                    {
                        if (CheckingNextPositioning ? currentBlock.NextPositioning[i, j] : currentBlock.Positioning[i, j])
                        {
                            result = property + i;
                        }
                    }
                    else
                    {
                        if (CheckingNextPositioning ? currentBlock.NextPositioning[j, i] : currentBlock.Positioning[j, i])
                        {
                            result = property + i;
                        }
                    }
                }
            }
            return result;

        }
        private int Min(int property, bool CheckingTheYAxis, bool CheckingNextPositioning = false)
        {
            int result = 0;
            for (int i = 3; i >= 0; i--)
            {
                for (int j = 3; j >= 0; j--)
                {
                    if (CheckingTheYAxis)
                    {
                        if (CheckingNextPositioning ? currentBlock.NextPositioning[i, j] : currentBlock.Positioning[i, j])
                        {
                            result = property + i;
                        }
                    }
                    else
                    {
                        if (CheckingNextPositioning ? currentBlock.NextPositioning[j, i] : currentBlock.Positioning[j, i])
                        {
                            result = property + i;
                        }
                    }
                }
            }
            return result;
        }
    }

}

