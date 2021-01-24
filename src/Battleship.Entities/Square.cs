using System;
using System.Collections.Generic;
using System.Linq;

namespace Battleship.Entities
{
    public class Square
    {
        public static readonly List<int> XCoordinates = Enumerable.Range(1, 10).ToList();
        public static readonly List<int> YCoordinates = Enumerable.Range(1, 10).ToList();
        public int XCoordinate { get; set; }
        public int YCoordinate { get; set; }

        public bool Occupied { get; set; }

        public Square(int x, int y)
        {

            XCoordinate = XCoordinates[x];
            YCoordinate = YCoordinates[y];
        }

        public void Occupy()
        {
            Occupied = true;
        }

        public bool IsOccupied()
        {
            return Occupied;
        }

        public void Vacant()
        {
            Occupied = false;
        }


    }
}