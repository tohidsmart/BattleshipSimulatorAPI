using Battleship.Entities.ResponseDto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Battleship.Command.Invoker
{
    public class SinglePlayer : IPlayer
    {

        public void Play(ICommand command, out BaseResponseDto responseDto)
        {
            command.Execute(out responseDto);
        }
    }
}
