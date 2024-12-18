﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results
{
    public class Result : IResult
    {
        public string Message { get; }
        public bool Success { get; }

        public Result(bool success, string message)
        {
            this.Message = message;
            this.Success = success;
        }

        public Result(bool success)
        {
            this.Success = success;
        }
    }
}
