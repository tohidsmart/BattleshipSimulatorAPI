using Battleship.Entities.ResponseDto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Battleship.Command.Invoker
{
    public interface IPlayer
    {
        void Play(ICommand command, out BaseResponseDto responseDto);
    }
}
