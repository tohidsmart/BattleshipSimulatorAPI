using Battleship.Entities.ResponseDto;
using System;

namespace Battleship.Command
{
    public interface ICommand
    {
        void Execute(out BaseResponseDto baseResponseDto);
    }
}
