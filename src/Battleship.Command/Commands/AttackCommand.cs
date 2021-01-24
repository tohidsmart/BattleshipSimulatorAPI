using Battleship.Command.Receiver;
using Battleship.Entities.RequestDto;
using Battleship.Entities.ResponseDto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Battleship.Command.Commands
{
    public class AttackCommand : ICommand
    {
        private readonly ISimulator simulator;
        private readonly AttackRequestDto attackRequestDto;

        public AttackCommand(ISimulator simulator, AttackRequestDto requestDto)
        {
            this.simulator = simulator;
            this.attackRequestDto = requestDto;
        }

        public void Execute(out BaseResponseDto responseDto)
        {
            responseDto = new BaseResponseDto();
            try
            {
                responseDto.Message = simulator.Attack(attackRequestDto).ToString();
                responseDto.Success = true;
            }
            catch (ArgumentException exception)
            {
                responseDto.Exception = exception;
                responseDto.Success = false;
            }

        }
    }
}
