using Core.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results
{
    public class ErrorResult : Result, IError
    {
        public ErrorResult(string message) 
            : base(success: false, message: message)
        {

        }

        public ErrorResult() 
            : base(success: false)
        {

        }

        public List<ErrorDetail> Errors { get; set; } = new List<ErrorDetail>();

        public ErrorResult AddErrorDetail(ErrorDetail errorDetail)
        {
            this.Errors.Add(errorDetail);
            return this;
        }
    }
}
