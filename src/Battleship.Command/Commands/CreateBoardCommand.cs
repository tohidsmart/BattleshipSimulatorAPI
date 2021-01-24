using Battleship.Command.Receiver;
using Battleship.Entities.ResponseDto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Battleship.Command.Commands
{
    public class CreateBoardCommand : ICommand

    {
        private readonly ISimulator simulator;

        public CreateBoardCommand(ISimulator simulator)
        {
            this.simulator = simulator;
        }

        public void Execute(out BaseResponseDto responseDto)
        {
            responseDto = new BaseResponseDto();
            try
            {
                responseDto.Success = simulator.CreateBoard();
                responseDto.Message= "Success";
            }
            catch (ArgumentException exception)
            {
                responseDto.Exception = exception;
                responseDto.Success = false;
            }



        }
    }
}
