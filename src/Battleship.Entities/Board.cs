using System;
using System.Collections.Generic;
using System.Text;

namespace Battleship.Entities
{
    public class Board
    {
        public Square[,] Squares { get; }
        public bool Initialized { get; set; }
        public Board()
        {
            Squares = new Square[10, 10];
            
        }

    }
}
