using Battleship.Entities;
using Battleship.Entities.RequestDto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Battleship.Command.Receiver
{
    public interface ISimulator
    {
        bool CreateBoard();
        bool AddBattleship(AddBattleshipRequestDto requestDto);

        AttackResult Attack(AttackRequestDto requestDto);
    }
}
