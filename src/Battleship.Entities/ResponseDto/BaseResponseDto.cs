using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Battleship.Entities.ResponseDto
{
    public class BaseResponseDto
    {
        public string Message { get; set; }

        public bool Success { get; set; }

        public ArgumentException Exception { get; set; }
    }
}
