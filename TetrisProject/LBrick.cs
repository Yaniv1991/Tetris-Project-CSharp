using System;

namespace TetrisProject
{
    class LBrick : IShape
    {
        public bool[,] Positioning { get; private set; }

        public bool[,] NextPositioning { get; private set; }

        public int X { get; set; }
        public int Y { get; set; }

        int rotation;

        public ConsoleColor Color => ConsoleColor.DarkYellow;


        public void Move(Direction direction)
        {
            switch (direction)
            {
                case Direction.Rotate:
                    Rotate();
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

        void Rotate()
        {
            Positioning = NextPositioning;
            switch (rotation)
            {
                // *
                // *
                // * *
                case 0:
                    NextPositioning = new bool[,]
                        {{true, false, false, false},
                        {true, false, false, false},
                        {true, true, false, false},
                        {false, false, false, false}};
                    break;
                // * * *
                // *
                case 1:
                    NextPositioning = new bool[,]
                        {{true, true, true, false},
                        {true, false, false, false},
                        {false, false, false, false},
                        {false, false, false, false}};
                    break;
                // * *
                //   *
                //   *
                case 2:
                    NextPositioning = new bool[,]
                        {{true, true, false, false},
                        {false, true, false, false},
                        {false, true, false, false},
                        {false, false, false, false}};
                    break;
                //     *
                // * * *
                case 3:
                    NextPositioning = new bool[,]
                        {{false, false, true, false },
                        {true, true, true, false},
                        {false, false, false, false},
                        {false, false, false, false}};
                    break;
            }
            rotation++;
            if (rotation > 3)
            {
                rotation = 0;
            }
        }

        public LBrick()
        {
            rotation = 0;
            Positioning = new bool[,]
                        {{false, false, true, false},
                        {true, true, true, false},
                        {false, false, false, false},
                        {false, false, false, false}};
            NextPositioning = new bool[,]
                        {{true, false, false, false},
                        {true, false, false, false},
                        {true, true, false, false},
                        {false, false, false, false}};

        }
    } // Done

    class ReverseLBrick : IShape // Done
    {
        public bool[,] Positioning { get; private set; }

        public bool[,] NextPositioning { get; private set; }

        public int X { get; set; }
        public int Y { get; set; }

        int rotation;

        public ConsoleColor Color => ConsoleColor.Magenta;


        public void Move(Direction direction)
        {
            switch (direction)
            {
                case Direction.Rotate:
                    Rotate();
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

        void Rotate()
        {
            Positioning = NextPositioning;
            switch (rotation)
            {
                //   *
                //   *
                // * *
                case 2:
                    NextPositioning = new bool[,]
                        {{false,true, false,  false},
                        {false,true,false, false},
                        {true, true,false, false},
                        {false, false, false, false}};
                    break;
                // *  
                // * * *
                case 3:
                    NextPositioning = new bool[,]
                        {{true,false, false, false},
                        {true, true, true,false},
                        {false, false, false, false},
                        {false, false, false, false}};
                    break;
                // * *
                // *
                // *
                case 0:
                    NextPositioning = new bool[,]
                         {{ true, true,false, false},
                         { true,false, false, false},
                         { true,false, false, false},
                         {false, false, false, false}};
                    break;
                // * * *
                //     *
                case 1:
                    NextPositioning = new bool[,]
                        {{ true, true, true,false},
                        {false, false, true,false },
                        {false, false, false, false},
                        {false, false, false, false}};
                    break;
            }
            rotation++;
            if (rotation > 3)
            {
                rotation = 0;
            }
        }

        public ReverseLBrick()
        {
            //   *
            //   *
            // * *

            rotation = 0;
            Positioning = new bool[,]
                       {{false, false, true, false},
                        {false, false, true, false},
                        {false, true, true, false},
                        {false, false, false, false}};
            // *  
            // * * *
            NextPositioning = new bool[,]
                        {{false, true, false, false},
                        {false, true, true, true},
                        {false, false, false, false},
                        {false, false, false, false}};
        }
    }

    class JaggedBrick : IShape // Done
    {
        public bool[,] Positioning { get; private set; }

        public bool[,] NextPositioning { get; private set; }

        public int X { get; set; }
        public int Y { get; set; }

        int rotation;

        public ConsoleColor Color => ConsoleColor.Red;

        public void Move(Direction direction)
        {
            switch (direction)
            {
                case Direction.Rotate:
                    Rotate();
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

        void Rotate()
        {
            Positioning = NextPositioning;
            switch (rotation)
            {
                //  * *
                //* *
                case 0:
                    NextPositioning = new bool[,]
                       {{false, true, true, false},
                        {true, true, false, false},
                        {false, false, false, false},
                        {false, false, false, false}};
                    break;
                // *
                // * *
                //   *
                case 1:
                    NextPositioning = new bool[,]
                       {{false, true, false, false},
                        {false, true, true, false},
                        {false, false, true, false},
                        {false, false, false, false}};
                    break;
                //  * *
                //* *
                case 2:
                    NextPositioning = new bool[,]
                       {{false, true, true, false},
                        {true, true, false, false},
                        {false, false, false, false},
                        {false, false ,false, false}};
                    break;
                // *
                // * *
                //   *
                case 3:
                    NextPositioning = new bool[,]
                       {{false, true ,false, false},
                        {false,true,true,false},
                            { false ,false, true, false},
                            {false, false ,false ,false}};
                    break;
            }
            rotation++;
            if (rotation > 3)
            {
                rotation = 0;
            }
        }

        public JaggedBrick()
        {
            rotation = 0;
            Positioning = new bool[,]
                       {{false, true, true, false},
                        {true, true, false, false},
                        {false, false, false, false},
                        {false, false, false, false}};
            NextPositioning = new bool[,]
                       {{false, true, false, false},
                       {false, true, true, false},
                       {false, false, true, false},
                       {false, false, false, false}};

        }
    }

    class ReverseJaggedBrick : IShape // Done
    {
        public bool[,] Positioning { get; private set; }

        public bool[,] NextPositioning { get; private set; }

        public int X { get; set; }
        public int Y { get; set; }

        int rotation;

        public ConsoleColor Color => ConsoleColor.Green;



        public void Move(Direction direction)
        {
            switch (direction)
            {
                case Direction.Rotate:
                    Rotate();
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

        void Rotate()
        {
            Positioning = NextPositioning;
            switch (rotation)
            {
                // * *
                //   * *
                case 0:
                    NextPositioning = new bool[,]
                        {{false, true, true, false},
                        {false, false, true, true},
                        {false, false, false, false},
                        {false, false ,false, false}};
                    break;
                //   *
                // * *
                // *
                case 1:
                    NextPositioning = new bool[,]
                        {{false, false, true, false},
                        {false, true, true, false},
                        {false, true, false, false},
                        {false, false ,false, false}};
                    break;
                // * *
                //   * *
                case 2:
                    NextPositioning = new bool[,]
                         {{false, true, true, false},
                        {false, false, true, true},
                        {false, false, false, false},
                        {false, false ,false, false}};
                    break;
                //   *
                // * *
                // *
                case 3:
                    NextPositioning = new bool[,]
                        {{false, false, true, false},
                        {false, true, true, false},
                        {false, true, false, false},
                        {false, false ,false, false}};
                    break;
            }
            rotation++;
            if (rotation > 3)
            {
                rotation = 0;
            }
        }

        public ReverseJaggedBrick()
        {
            rotation = 0;
            Positioning = new bool[,]
                        {{false, true, true, false},
                        {false, false, true, true},
                        {false, false, false, false},
                        {false, false ,false, false}};
            NextPositioning = new bool[,]
                        {{false, false, true, false},
                        {false, true, true, false},
                        {false, true, false, false},
                        {false, false ,false, false}};

        }
    }

    class TBrick : IShape // Done
    {
        public bool[,] Positioning { get; private set; }

        public bool[,] NextPositioning { get; private set; }

        public int X { get; set; }
        public int Y { get; set; }

        int rotation;

        public ConsoleColor Color => ConsoleColor.DarkMagenta;

        public void Move(Direction direction)
        {
            switch (direction)
            {
                case Direction.Rotate:
                    Rotate();
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

        void Rotate()
        {
            Positioning = NextPositioning;
            switch (rotation)
            {

                //   *
                // * * *

                // *  
                // * *
                // *

                // * * *
                //   *

                //   *
                // * *
                //   *


                case 0:
                    NextPositioning = new bool[,]
                        {{true, true, true, false},
                        {false, true, false, false},
                        {false, false, false, false},
                        {false, false ,false, false}};
                    break;

                case 1:
                    NextPositioning = new bool[,]
                        {{false,true,false,false},
                         {true,true,false,false},
                        {false,true,false,false},
                        {false,false,false,false}};
                    break;

                case 2:
                    NextPositioning = new bool[,]
                        {{false, true, false, false},
                        {true,true, true,false},
                        {false, false, false, false},
                        {false, false ,false, false}};
                    break;

                case 3:
                    NextPositioning = new bool[,]
                        {{true, false, false, false},
                        {true,true, false,false},
                        {true, false, false, false},
                        {false, false ,false, false}};
                    break;
            }
            rotation++;
            if (rotation > 3)
            {
                rotation = 0;
            }
        }

        public TBrick()
        {
            rotation = 0;
            Positioning = new bool[,]
                        {{false, true, false, false},
                        {true,true, true,false},
                        {false, false, false, false},
                        {false, false ,false, false}};
            NextPositioning = new bool[,]
                        {{true, false, false, false},
                        {true, true, false, false},
                        {true, false, false, false},
                        {false, false ,false, false}};

        }
    }
}

