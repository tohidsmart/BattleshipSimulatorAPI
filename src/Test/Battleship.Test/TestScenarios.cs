using Battleship.Command;
using Battleship.Command.Commands;
using Battleship.Command.Invoker;
using Battleship.Command.Receiver;
using Battleship.Entities;
using Battleship.Entities.RequestDto;
using Battleship.Entities.ResponseDto;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BattleShip.Test
{
    [TestClass]
    public class TestScenarios
    {
        public ICommand Command { get; set; }
       
        //Receiver
        public ISimulator Simulator { get; set; }

        //Invoker
        public IPlayer Player { get; set; }

        public TestScenarios()
        {
            
            Simulator = new BattleshipSimulator();
            Player = new SinglePlayer();
        }


        // <summary>
        /// Users initiates the attack before creating the board
        /// The response success is false
        /// The shown message is to create the board first
        /// </summary>
        [TestMethod]
        public void Test1_RunAttack_BeforeCreateBoard()
        {
            //Arrange 
            var attackDto = new AttackRequestDto { XCoordinate = 1, YCoordinate = 1 };

            Player.Play(new AttackCommand(Simulator,attackDto), out BaseResponseDto responseDto);

            Assert.IsFalse(responseDto.Success);
            Assert.IsNotNull(responseDto.Exception);
            Assert.IsTrue( responseDto.Exception.Message.Contains("Board is not created yet.Run Create Board API to initialize the Board"));

        }


        // <summary>
        /// Users call the Created board for first time
        /// /// The response success is false
        /// The shown message is to create the board first
        /// </summary>
        [TestMethod]
        public void Test2_RunCreateBoard()
        {
           
            Player.Play(new CreateBoardCommand(Simulator),out BaseResponseDto responseDto);
            

            Assert.IsTrue(responseDto.Success);
            Assert.AreEqual("Success", responseDto.Message);
            Assert.IsNull(responseDto.Exception);

        }



        /// <summary>
        /// The user calls the Create Board Command
        /// The user calls the Add Add BattleShip Command
        /// Battleship is placed
        /// </summary>
        [TestMethod]
        public void Test3_AddBattleShipToBoard()
        {
            //Arrange
            Player.Play(new CreateBoardCommand(Simulator), out _);
            var addBattleshipDto = new AddBattleshipRequestDto { Size = 3, XCoordinate = 2, YCoordinate = 2, Direction = Direction.Horizontal };
            
            //Act
            Player.Play(new AddBattleshipCommand(Simulator, addBattleshipDto), out BaseResponseDto responseDto);
            
            Assert.IsTrue(responseDto.Success);
            Assert.IsTrue(responseDto.Message.Contains("Battleship placed successfully"));
        }

        /// <summary>
        /// The user calls the Create Board Command
        /// The user calls the Add BattleShip Command with wrong parameters
        /// The API returns error message
        /// 
        /// </summary>
        [TestMethod]
        public void Test4_AddBattleshipWithWrongHorizontalParameters()
        {
            //Arrange
            Player.Play(new CreateBoardCommand(Simulator), out _);
            var wrongBattleshipDto = new AddBattleshipRequestDto { Size = 3, XCoordinate = 9, YCoordinate = 1, Direction = Direction.Horizontal };

            //Act
            Player.Play(new AddBattleshipCommand(Simulator, wrongBattleshipDto), out BaseResponseDto responseDto);


            Assert.IsFalse(responseDto.Success);
            Assert.IsNotNull(responseDto.Exception);
            Assert.IsTrue(responseDto.Exception.Message.Contains($"Battleship size is greater than the board size in {wrongBattleshipDto.Direction} direction"));

        }


        /// <summary>
        /// The user calls the Create Board Command
        /// The user calls the Add BattleShip Command with wrong vertical parameters
        /// The API returns error message
        /// 
        /// </summary>
        [TestMethod]
        public void Test5_AddBattleshipWithWrongVerticalParameters()
        {
            //Arrange
            Player.Play(new CreateBoardCommand(Simulator), out _);
            var wrongBattleshipDto = new AddBattleshipRequestDto { Size = 3, XCoordinate = 2, YCoordinate = 9, Direction = Direction.Vertical};

            //Act
            Player.Play(new AddBattleshipCommand(Simulator, wrongBattleshipDto), out BaseResponseDto responseDto);


            Assert.IsFalse(responseDto.Success);
            Assert.IsNotNull(responseDto.Exception);
            Assert.IsTrue(responseDto.Exception.Message.Contains($"Battleship size is greater than the board size in {wrongBattleshipDto.Direction} direction"));

        }


        /// <summary>
        /// The user calls the Create Board Command
        /// The user calls the Add BattleShip Commands with correct Horizontal parameters
        /// The API returns success message
        /// 
        /// </summary>
        [TestMethod]
        public void Test6_AddBattleshipWithCorrectHorizontalParameters()
        {
            //Arrange
            Player.Play(new CreateBoardCommand(Simulator), out _);
            var correctHorizontalBattleshipDto = new AddBattleshipRequestDto { Size = 5, XCoordinate = 6, YCoordinate = 1, Direction = Direction.Horizontal};

            //Act
            Player.Play(new AddBattleshipCommand(Simulator, correctHorizontalBattleshipDto), out BaseResponseDto responseDto);


            Assert.IsTrue(responseDto.Success);
            Assert.IsNull(responseDto.Exception);
            Assert.IsTrue(responseDto.Message.Contains($"Battleship placed successfully"));

        }

        /// <summary>
        /// The user calls the Create Board command
        /// The user calls the Add BattleShip Command with correct vertical parameters
        /// The API returns success message 
        /// 
        /// </summary>
        [TestMethod]
        public void Test7_AddBattleshipWithCorrectVerticalParameters()
        {
            //Arrange
            Player.Play(new CreateBoardCommand(Simulator), out _);
            var correctVerticalBattleshipDto = new AddBattleshipRequestDto { Size = 5, XCoordinate = 1, YCoordinate = 6, Direction = Direction.Vertical };

            //Act
            Player.Play(new AddBattleshipCommand(Simulator, correctVerticalBattleshipDto), out BaseResponseDto responseDto);


            Assert.IsTrue(responseDto.Success);
            Assert.IsNull(responseDto.Exception);
            Assert.IsTrue(responseDto.Message.Contains($"Battleship placed successfully"));

        }


        /// <summary>
        /// The user calls the Create Board
        /// The user calls the Add BattleShipCommand with  vertical parameters
        /// The user initiates an attack
        /// The API returns success message : Hit
        /// 
        /// </summary>
        [TestMethod]
        public void Test8_AddBattleship_StrikeAttack_AttackHit()
        {
            //Arrange
            Player.Play(new CreateBoardCommand(Simulator), out _);
            var verticalBattleshipDto = new AddBattleshipRequestDto { Size = 5, XCoordinate = 1, YCoordinate = 6, Direction = Direction.Vertical };
            var attackDto = new AttackRequestDto { XCoordinate = 1, YCoordinate = 7 };

            //Act
            Player.Play(new AddBattleshipCommand(Simulator, verticalBattleshipDto), out _);
            Player.Play(new AttackCommand(Simulator, attackDto), out BaseResponseDto responseDto);


            Assert.IsTrue(responseDto.Success);
            Assert.IsNull(responseDto.Exception);
            Assert.IsTrue(responseDto.Message.Contains($"Hit"));

        }


        /// <summary>
        /// The user calls the Create Board command
        /// The user calls the Add BattleShipCommand with  vertical parameters
        /// The user initiates an attack
        /// The API return Miss message : Miss
        /// 
        /// </summary>
        [TestMethod]
        public void Test9_AddBattleship_StrikeAttack_AttackMiss()
        {
            //Arrange
            Player.Play(new CreateBoardCommand(Simulator), out _);
            var verticalBattleshipDto = new AddBattleshipRequestDto { Size = 5, XCoordinate = 1, YCoordinate = 6, Direction = Direction.Vertical };
            var attackDto = new AttackRequestDto { XCoordinate = 1, YCoordinate = 5 };

            //Act
            Player.Play(new AddBattleshipCommand(Simulator, verticalBattleshipDto), out _);
            Player.Play(new AttackCommand(Simulator, attackDto), out BaseResponseDto responseDto);


            Assert.IsTrue(responseDto.Success);
            Assert.IsNull(responseDto.Exception);
            Assert.IsTrue(responseDto.Message.Contains($"Miss"));

        }


        /// <summary>
        /// The user calls the Create Board Command
        /// The user calls the Add BattleShip Command with vertical parameters
        /// The user initiates an attack
        /// The API returns success message : Miss
        /// 
        /// </summary>
        [TestMethod]
        public void Test91_AddBattleship_StrikeAttack_AttackMiss()
        {
            //Arrange
            Player.Play(new CreateBoardCommand(Simulator), out _);
            var verticalBattleshipDto = new AddBattleshipRequestDto { Size = 4, XCoordinate = 1, YCoordinate = 6, Direction = Direction.Vertical };
            var attackDto = new AttackRequestDto { XCoordinate = 1, YCoordinate = 10 };

            //Act
            Player.Play(new AddBattleshipCommand(Simulator, verticalBattleshipDto), out _);
            Player.Play(new AttackCommand(Simulator, attackDto), out BaseResponseDto responseDto);


            Assert.IsTrue(responseDto.Success);
            Assert.IsNull(responseDto.Exception);
            Assert.IsTrue(responseDto.Message.Contains($"Miss"));

        }

    }
}
