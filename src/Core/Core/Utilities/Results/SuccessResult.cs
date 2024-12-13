using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results
{
    public class SuccessResult : Result, ISuccess
    {
        public SuccessResult(string message)
            : base(success: true, message: message)
        {

        }

        public SuccessResult()
            : base(success: true)
        {

        }
    }
}
