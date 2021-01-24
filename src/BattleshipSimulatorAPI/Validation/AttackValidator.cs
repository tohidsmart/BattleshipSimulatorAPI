using Battleship.Entities.RequestDto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BattleshipSimulatorAPI.Validation
{
    public class AttackValidator :AbstractValidator<AttackRequestDto>
    {
        public AttackValidator()
        {
            RuleFor(request => request.XCoordinate).LessThanOrEqualTo(10).WithMessage("X coordinate value should be less or Equal to 10");
            RuleFor(request => request.XCoordinate).GreaterThanOrEqualTo(1).WithMessage("X coordinate value should be greater or equal to 1");
            
            RuleFor(request => request.YCoordinate).LessThanOrEqualTo(10).WithMessage("Y coordinate value should be less or Equal to 10");
            RuleFor(request => request.YCoordinate).GreaterThanOrEqualTo(1).WithMessage("coordinate value should be greater or equal to 1");
        }
    }
}
