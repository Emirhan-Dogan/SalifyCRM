using Core.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results
{
    public class ErrorDataResult<T> : DataResult<T>, IError
    {
        public ErrorDataResult(T data, string message)
            : base(data: data, success: false, message: message)
        {

        }

        public ErrorDataResult(T data)
            : base(data: data, success: false)
        {

        }

        public ErrorDataResult(string message)
            : base(default, false, message)
        {
        }

        public ErrorDataResult()
            : base(default, false)
        {
        }

        public List<ErrorDetail>? Errors { get; set; } = new List<ErrorDetail>();

        public ErrorDataResult<T> AddErrorDetail(ErrorDetail errorDetail)
        {
            this.Errors.Add(errorDetail);
            return this;
        }
    }
}
