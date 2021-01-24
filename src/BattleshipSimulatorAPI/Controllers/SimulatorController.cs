using Battleship.Command.Commands;
using Battleship.Command.Invoker;
using Battleship.Command.Receiver;
using Battleship.Entities.RequestDto;
using Battleship.Entities.ResponseDto;
using Microsoft.AspNetCore.Mvc;

namespace BattleshipSimulatorAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class SimulatorController : ControllerBase
    {
        private readonly IPlayer player;
        private readonly ISimulator simulator;

        public SimulatorController(IPlayer player, ISimulator simulator)
        {
            this.player = player;
            this.simulator = simulator;
        }

        [HttpPost]
        [Route("board")]
        public IActionResult CreateBoard()
        {
            player.Play(new CreateBoardCommand(simulator), out BaseResponseDto responseDto);
            if (responseDto.Success)
                return Created("", responseDto?.Message);
            return BadRequest(responseDto.Exception?.Message);
        }

        [HttpPost]
        [Route("board/attack")]
        public IActionResult Attack([FromBody] AttackRequestDto requestDto)
        {
            player.Play(new AttackCommand(simulator, requestDto), out BaseResponseDto responseDto);
            if (responseDto.Success)
                return Ok(responseDto?.Message);
            return BadRequest(responseDto?.Exception?.Message);
        }

        [HttpPost]
        [Route("board/battleship")]
        public IActionResult PlaceBattleship([FromBody] AddBattleshipRequestDto requestDto)
        {
            player.Play(new AddBattleshipCommand(simulator, requestDto), out BaseResponseDto responseDto);
            if (responseDto.Success)
                return Ok(responseDto?.Message);
            return BadRequest(responseDto?.Exception?.Message);
        }


    }
}
