using System;
using System.Collections.Generic;
using System.Text;

namespace Battleship.Entities.RequestDto
{
    public class AddBattleshipRequestDto
    {
        public int XCoordinate { get; set; }
        public int YCoordinate { get; set; }

        public Direction Direction { get; set; }
        public int Size { get; set; }
    }
}
