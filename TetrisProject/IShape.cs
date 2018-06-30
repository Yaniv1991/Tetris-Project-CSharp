using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisProject
{
    interface IShape
    {
        bool[,] Positioning { get;}
        bool[,] NextPositioning { get; }

        int X { get; set; }
        int Y { get; set; }
        ConsoleColor Color { get; }

        void Move(Direction direction);
    }
}
