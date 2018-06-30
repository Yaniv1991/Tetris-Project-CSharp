using System;

namespace TetrisProject
{
    class LineBrick : IShape
    {
        public int X { get; set; }
        public int Y { get; set; }
        public ConsoleColor Color { get => ConsoleColor.Cyan; }

        public bool[,] Positioning { get; private set; }

        public bool[,] NextPositioning { get; private set; }

        public LineBrick()
        {
            Positioning = new bool[,]
                        {{true,true,true,true },
                            {false, false ,false, false },
                            { false ,false, false, false},
                            {false, false ,false ,false}};
            NextPositioning = new bool[,]
                       {
                            {true,false,false,false},
                            {true, false ,false, false},
                            {true,false, false, false},
                            {true, false ,false ,false}
                           };
        }

        public void Move(Direction direction)
        {
            switch (direction)
            {
                case Direction.Rotate:
                    bool[,] temp = new bool[4, 4];
                    temp = Positioning;
                    Positioning = NextPositioning;
                    NextPositioning = temp;
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
    }
}
