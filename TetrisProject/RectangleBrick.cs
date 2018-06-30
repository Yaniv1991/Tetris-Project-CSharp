using System;

namespace TetrisProject
{
    class RectangleBrick : IShape
    {
        public bool[,] Positioning { get; private set; }

        public bool[,] NextPositioning { get => Positioning; }

        
        public int X { get; set; }
        public int Y { get; set; }

        public ConsoleColor Color => ConsoleColor.Yellow;

        public void Move(Direction direction)
        {
            switch (direction)
            {
                case Direction.Rotate:
                    
                    break;

                case Direction.Right:
                    X++;
                    break;

                case Direction.Left:
                    X--;
                    break;

                case Direction.Down:
                    Y++;
                    break;
            }
        }

        public RectangleBrick()
        {
            Positioning = new bool[,]
            {
                {true,true,false,false },
                {true,true,false,false },
                {false,false,false,false },
                {false,false,false,false } };
        }
    }
}
