﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results
{
    public class SuccessDataResult<T> : DataResult<T>, ISuccess
    {
        public SuccessDataResult(T data, string message)
            : base(data: data, success: true, message: message)
        {

        }

        public SuccessDataResult(T data)
            : base(data: data, success: true)
        {

        }

        public SuccessDataResult(string message)
            : base(default, true, message)
        {
        }

        public SuccessDataResult()
            : base(default, true)
        {
        }
    }
}
