using Battleship.Command.Receiver;
using Battleship.Entities.RequestDto;
using Battleship.Entities.ResponseDto;
using System;

namespace Battleship.Command.Commands
{
   public class AddBattleshipCommand : ICommand
    {
        private readonly ISimulator simulator;
        private readonly AddBattleshipRequestDto requestDto;

        public AddBattleshipCommand(ISimulator simulator,AddBattleshipRequestDto requestDto)
        {
            this.simulator = simulator;
            this.requestDto = requestDto;
        }
        public void Execute(out BaseResponseDto responseDto )
        {
            responseDto = new BaseResponseDto();
            try
            {
                responseDto.Success = simulator.AddBattleship(requestDto);
                responseDto.Message = responseDto.Success ? "Battleship placed successfully" : "Something went wrong";
            }
            catch (ArgumentException exception)
            {
                responseDto.Success = false;
                responseDto.Exception = exception;
            }
            
        }
    }
}
