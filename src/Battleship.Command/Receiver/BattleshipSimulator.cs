using Battleship.Entities;
using Battleship.Entities.RequestDto;
using System;

namespace Battleship.Command.Receiver
{
    public class BattleshipSimulator : ISimulator
    {
        private readonly Board Board;
        private const int IncludeFirstSquare = 1;

        public BattleshipSimulator()
        {
            this.Board = new Board();
        }

        public bool AddBattleship(AddBattleshipRequestDto requestDto)
        {
            if (!Board.Initialized)
                throw new ArgumentException("Board is not created yet.Run Create Board API to initialize the Board", "Place Battleship");

            requestDto.YCoordinate--;
            requestDto.XCoordinate--;

            if (requestDto.Direction == Direction.Horizontal)
            {
                if (requestDto.Size >  Board.Squares.GetLength(0) - requestDto.XCoordinate)
                    throw new ArgumentException($"Battleship size is greater than the board size in {Direction.Horizontal} direction", "Place BattleShip");

                for (int j = 0; j < requestDto.Size; j++)
                {
                    if (Board.Squares[requestDto.XCoordinate + j, requestDto.YCoordinate].IsOccupied())
                    {
                        throw new ArgumentException("One or more square(s) are already occupied in the position", "Place Battleship");
                    }

                }
                int k = 0;
                while (k < requestDto.Size)
                {
                    Board.Squares[requestDto.XCoordinate + k, requestDto.YCoordinate].Occupy();
                    k++;
                }
                return true;
            }

            if (requestDto.Direction == Direction.Vertical)
            {
                if (requestDto.Size  > Board.Squares.GetLength(1) - requestDto.YCoordinate)
                    throw new ArgumentException($"Battleship size is greater than the board size in {Direction.Vertical} direction", "Place BattleShip");

                for (int i = 0; i < requestDto.Size; i++)
                {
                    if (Board.Squares[requestDto.XCoordinate, requestDto.YCoordinate + i].IsOccupied())
                    {
                        throw new ArgumentException($"One or more square(s) are already occupied in the sequence", "Place BattleShip");
                    }
                }
                int k = 0;
                while (k < requestDto.Size)
                {
                    Board.Squares[requestDto.XCoordinate, requestDto.YCoordinate + k].Occupy();
                    k++;
                }
                return true;
            }

            return false;
        }

        public AttackResult Attack(AttackRequestDto requestDto)
        {
            if (!Board.Initialized)
                throw new ArgumentException("Board is not created yet.Run Create Board API to initialize the Board", "Initialize Board");

            requestDto.XCoordinate--;
            requestDto.YCoordinate--;
            if (Board.Squares[requestDto.XCoordinate, requestDto.YCoordinate].IsOccupied())
            {
                Board.Squares[requestDto.XCoordinate, requestDto.YCoordinate].Vacant();
                return AttackResult.Hit;
            }

            return AttackResult.Miss;
        }

        public bool CreateBoard()
        {
            if (Board.Initialized)
                throw new ArgumentException("Board is already created. Run Attack or Add Battleship APIs", "Board Initialized");

            for (int i = 0; i < Board.Squares.GetLength(0); i++)
            {

                for (int j = 0; j < Board.Squares.GetLength(1); j++)
                {
                    Board.Squares[i, j] = new Square(i, j);
                }

            }
            Board.Initialized = true;
            return Board.Initialized;
        }


    }
}
